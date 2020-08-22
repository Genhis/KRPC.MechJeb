import krpc
import inspect
import sys
from krpcmjtest import *

# Function definitions
indent = 0
def prettyPrint(message, end = "\n"):
	print("    " * indent + message, end = end)

# Initialize kRPC
conn = krpc.connect("KRPC.MechJeb tests")
sc = conn.space_center
mj = conn.mech_jeb

# Create modules
modules = [
	#AirplaneAutopilotTest("AirplaneAutopilot"),
	AscentAutopilotTest("AscentAutopilot"),
	DockingAutopilotTest("DockingAutopilot"),
	LandingAutopilotTest("LandingAutopilot"),
	RendezvousAutopilotTest("RendezvousAutopilot"),

	SmartAssTest("SmartASS"),
	SmartRcsTest("SmartRCS"),
	TranslatronTest("Translatron"),
]

# Test modules
for module in modules:
	prettyPrint("Testing module " + module.type)
	indent += 1
	
	module.setInstance(sc, mj)
	members = inspect.getmembers(module)
	for name, method in members:
		if Annotations.hasAnnotation(method, Annotations.BeforeClass):
			prettyPrint("Calling BeforeClass")
			getattr(module, name)()

	for name, method in members:
		if Annotations.hasAnnotation(method, Annotations.Test):
			prettyPrint("%-40s" % ("Calling " + name + "()"), " ")
			errors = []

			value = Annotations.getTestType(method)
			method = getattr(module, name)
			values = []
			if value == InputType.NONE:
				values.append(-1)
				try:
					method()
				except:
					errors.append(str(sys.exc_info()[0]))
			else:
				if value == InputType.BOOLEAN:
					values = [True, False]
				else:
					if value == InputType.FLOAT:
						for i in range(1, 4):
							values.append((i ** 3 + 0.25 ** i) * 100)
					# value == InputType.INTEGER:
					for i in range(1, 4):
						values.append(i ** 3 * 100)

				for value in values:
					try:
						method(value)
					except:
						errors.append(str(sys.exc_info()[0]))

			failed = len(errors) > 0
			print(str(len(values) - len(errors)) + "/" + str(len(values)) + "   " + ("FAILED" if failed else "SUCCEEDED"))
			if failed:
				indent += 1
				for error in errors:
					prettyPrint(error)
				indent -= 1

	indent -= 1

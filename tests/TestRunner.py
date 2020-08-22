import krpc
import inspect
import sys
from krpcmjtest import *

# Function definitions
indent = 0
def prettyPrint(message, end = "\n"):
	print("    " * indent + message, end = end)

def printErrors(errors):
	global indent
	
	indent += 1
	for error in errors:
		prettyPrint(error)
	indent -= 1

def runTests(sc, parentInstance, modules):
	global indent

	for module in modules:
		prettyPrint("Testing module " + module.name)
		indent += 1
	
		try:
			globalErrors = module.setInstance(sc, parentInstance)
			members = inspect.getmembers(module)
			for name, method in members:
				if Annotations.hasAnnotation(method, Annotations.BeforeClass):
					prettyPrint("Setting up environment...")
					getattr(module, name)()

			for name, method in members:
				if Annotations.hasAnnotation(method, Annotations.Test):
					prettyPrint("%-40s" % ("Calling " + name + "()"), " ")
					if name in globalErrors:
						print("      NOT RUN")
						printErrors([str(globalErrors[name])])

					errors = []
					values = []
					def catchExceptions(callable):
						try:
							callable()
						except (Exception, RuntimeError) as ex:
							errors.append(str(ex))

					value = Annotations.getTestType(method)
					method = getattr(module, name)
					if value == InputType.NONE:
						values.append(-1)
						catchExceptions(method)
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
							catchExceptions(lambda: method(value))

					failed = len(errors) > 0
					print(str(len(values) - len(errors)) + "/" + str(len(values)) + "   " + ("FAILED" if failed else "SUCCEEDED"))
					if failed:
						printErrors(errors)
		except (Exception, RuntimeError) as ex:
			printErrors([ex])

		# Check for sub-modules
		if hasattr(module, "submodules"):
			runTests(sc, module.instance, module.submodules)

		indent -= 1

# Initialize kRPC
conn = krpc.connect("KRPC.MechJeb tests")
sc = conn.space_center
mj = conn.mech_jeb

# Create modules
modules = [
	#AirplaneAutopilotTest(),
	AscentAutopilotTest(),
	DockingAutopilotTest(),
	LandingAutopilotTest(),
	RendezvousAutopilotTest(),

	SmartAssTest(),
	SmartRcsTest(),
	TranslatronTest(),
]

# Test modules
runTests(sc, mj, modules)

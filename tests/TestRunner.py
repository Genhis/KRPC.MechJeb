import krpc
import inspect
from krpcmjtest import *

# Function definitions
indent = 0
def prettyPrint(message):
	print(" " * indent * 2 + message)

# Initialize kRPC
conn = krpc.connect("KRPC.MechJeb tests")
sc = conn.space_center
mj = conn.mech_jeb

# Create modules
modules = [
	AscentAutopilotTest("AscentAutopilot"),
	DockingAutopilotTest("DockingAutopilot")
]

# Test modules
for module in modules:
	prettyPrint("Testing module " + module.type)
	indent += 1
	
	module.setInstance(conn, sc, mj)
	members = inspect.getmembers(module, inspect.ismethod)
	for name, method in members:
		if hasAnnotation(method, Annotations.BeforeClass):
			prettyPrint("Calling BeforeClass")
			getattr(module, name)()

	for name, method in members:
		if hasAnnotation(method, Annotations.Test):
			value = getTestType(method)
			method = getattr(module, name)
			if value == InputType.NONE:
				method()
			elif value == InputType.BOOLEAN:
				method(True)
				method(False)
			else:
				if value == InputType.FLOAT:
					for i in range(1, 4):
						method((i ** 3 + 0.25 ** i) * 100)
				# value == InputType.INTEGER:
				for i in range(1, 4):
					method(i ** 3 * 100)
	indent -= 1

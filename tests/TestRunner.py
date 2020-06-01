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
	AscentAutopilotTest("AscentAutopilot", mj.ascent_autopilot),
	DockingAutopilotTest("DockingAutopilot", mj.docking_autopilot)
]

# Test modules
for module in modules:
	prettyPrint("Testing module " + module.type)
	indent += 1

	members = inspect.getmembers(type(module), inspect.ismethod)
	for name, method in members:
		if hasAnnotation(method, Annotations.BeforeClass):
			prettyPrint("Calling BeforeClass")
			getattr(module, name)()

	for name, method in members:
		if hasAnnotation(method, Annotations.Test):
			value = getTestType(method)
			method = getattr(module, name)
			if value == InputType.BOOLEAN:
				method(True)
				method(False)
			elif value == InputType.FLOAT:
				for i in range(1, 4):
					method((i ** 3 + 0.25 ** i) * 100)
			elif value == InputType.INTEGER:
				for i in range(1, 4):
					method(i ** 3 * 100)
	indent -= 1

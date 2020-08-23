import krpc
import inspect

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

def runTests(spaceCenter, parentInstance, modules):
	global indent

	for module in modules:
		prettyPrint("Testing module " + module.name)
		indent += 1
	
		try:
			globalErrors = module.setInstance(spaceCenter, parentInstance)
			members = inspect.getmembers(module, inspect.ismethod)
			for name, method in members:
				if Annotations.hasAnnotation(method, Annotations.BeforeClass):
					prettyPrint("Setting up environment...")
					getattr(module, name)()

			for name, method in members:
				if Annotations.hasAnnotation(method, Annotations.Test):
					prettyPrint("%-40s" % ("Calling " + name + "()"), " ")

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

						
			for name, error in globalErrors.items():
				prettyPrint("%-40s" % ("Calling " + name + "()"), " ")
				if isinstance(error, MissingTestException):
					print("      MISSING")
				else:
					print("      NOT RUN")
					printErrors([str(error)])

			# Check for sub-modules
			if hasattr(module, "submodules"):
				runTests(sc, module.instance, module.submodules)
		except (Exception, RuntimeError) as ex:
			prettyPrint("Testing FAILED: " + str(ex))


		indent -= 1

# Initialize kRPC
conn = krpc.connect("KRPC.MechJeb tests")

# Create modules
modules = [
	AirplaneAutopilotTest(),
	AscentAutopilotTest(),
	DockingAutopilotTest(),
	LandingAutopilotTest(),
	RendezvousAutopilotTest(),

	SmartAssTest(),
	SmartRcsTest(),
	TranslatronTest(),
]

# Test modules
runTests(conn.space_center, conn.mech_jeb, modules)

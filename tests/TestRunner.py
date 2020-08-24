from colorama import init as initColoredOutput, Fore, Style
import krpc
import inspect
import re

from krpcmjtest import *

# Function definitions
indent = 0
indentMultiplier = 4
def prettyPrint(message, end = "\n"):
	print(" " * indentMultiplier * indent + message, end = end)

def printErrors(errors):
	global indent
	
	indent += 1
	for error in errors:
		lines = (type(error).__name__ + ": " + str(error)).split("\n")
		removeFrom = 1
		for i, line in enumerate(lines):
			if "KRPC." in line:
				removeFrom = i + 1

		if removeFrom < len(lines):
			del lines[removeFrom:]

		for line in lines:
			prettyPrint(Fore.BLACK + printErrors.p1.sub("", line).replace(" (", "("))
	indent -= 1
printErrors.p1 = re.compile(r"\[0x.*")

def getTypeMessage(type, failed):
	if type == InputType.READ_ONLY:
		return Fore.CYAN + "READ-ONLY" + Fore.RESET
	if type == InputType.MISSING:
		return Fore.YELLOW + "MISSING" + Fore.RESET
	if type == InputType.NOT_RUN:
		return Fore.RED + "NOT RUN" + Fore.RESET

	return (Fore.RED + "FAILED" if failed else Fore.GREEN + "SUCCEEDED") + Fore.RESET

summary = {
	False: 0,
	True: 0,
	InputType.READ_ONLY: 0,
	InputType.MISSING: 0,
	InputType.NOT_RUN: 0,
}
failedModules = []
def runTests(spaceCenter, parentInstance, modules):
	global indent

	for module in modules:
		print()
		prettyPrint("Testing module " + Fore.YELLOW + module.name + Fore.RESET + ":")
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
					prettyPrint(("Calling %-" + str(40 - indentMultiplier * indent) + "s") % (name + "()"), " ")

					errors = []
					values = []
					def catchExceptions(callable):
						try:
							callable()
						except (Exception, RuntimeError) as ex:
							errors.append(ex)

					t = Annotations.getTestType(method)
					empty = t == InputType.READ_ONLY or t == InputType.MISSING or t == InputType.NOT_RUN
					method = getattr(module, name)
					failed = False
					if not empty:
						if t == InputType.NONE:
							values.append(-1)
							catchExceptions(method)
						else:
							if t == InputType.BOOLEAN:
								values = [True, False]
							else:
								if t == InputType.FLOAT:
									for i in range(1, 4):
										values.append(i ** 3 * 100 + 0.25 ** i)
								# t == InputType.INTEGER:
								for i in range(1, 4):
									values.append(i ** 3 * 100)

							for value in values:
								catchExceptions(lambda: method(value))

						failed = len(errors) > 0
						summary[failed] += 1
					else:
						summary[t] += 1

					print(("   " if empty else str(len(values) - len(errors)) + "/" + str(len(values))) + "   %-19s" % (getTypeMessage(t, failed)) + ("" if Annotations.hasAnnotation(method, Annotations.Generated) else "   MANUAL"))
					if failed:
						printErrors(errors)
					elif t == InputType.NOT_RUN:
						printErrors([globalErrors[name]])

			# Check for sub-modules
			if hasattr(module, "submodules"):
				runTests(spaceCenter, module.instance, module.submodules)
		except (Exception, RuntimeError) as ex:
			prettyPrint(Fore.RED + "*** Testing FAILED *** " + Fore.YELLOW + type(ex).__name__ + ": " + str(ex))
			failedModules.append(module)

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

	NodeExecutorTest(),
	RcsControllerTest(),
	StagingControllerTest(),
]

# Test modules
initColoredOutput(True)
runTests(conn.space_center, conn.mech_jeb, modules)

# Print summary
def printSummary(message, value):
	if value != 0:
		print(message + ": " + str(value))

print()
printSummary(getTypeMessage(InputType.NONE, False), summary[False])
printSummary(getTypeMessage(InputType.READ_ONLY, False), summary[InputType.READ_ONLY])
printSummary(getTypeMessage(InputType.MISSING, False), summary[InputType.MISSING])
printSummary(getTypeMessage(InputType.NONE, True), summary[True])
printSummary(getTypeMessage(InputType.NOT_RUN, False), summary[InputType.NOT_RUN])

# TODO: print failed modules

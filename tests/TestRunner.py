from colorama import init as initColoredOutput, Fore, Style
import krpc
import inspect
import itertools
import re

from modules import *

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
	if type == GeneratedTestType.READ_ONLY:
		return Fore.CYAN + "READ-ONLY" + Fore.RESET
	if type == GeneratedTestType.MISSING:
		return Fore.YELLOW + "MISSING" + Fore.RESET
	if type == GeneratedTestType.NOT_RUN:
		return Fore.RED + "NOT RUN" + Fore.RESET

	return (Fore.RED + "FAILED" if failed else Fore.GREEN + "SUCCEEDED") + Fore.RESET

summary = {
	False: 0,
	True: 0,
	GeneratedTestType.READ_ONLY: 0,
	GeneratedTestType.MISSING: 0,
	GeneratedTestType.NOT_RUN: 0,
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
					method = getattr(module, name)
					values = list(itertools.product(*Annotations.ParameterizedTest.getParameters(method))) if Annotations.hasAnnotation(method, Annotations.ParameterizedTest) else []
					def catchExceptions(callable):
						try:
							callable()
						except (Exception, RuntimeError) as ex:
							errors.append(ex)

					generated = Annotations.hasAnnotation(method, Annotations.GeneratedTest)
					t = Annotations.GeneratedTest.getType(method) if generated else GeneratedTestType.NORMAL
					failed = False
					if t is GeneratedTestType.NORMAL:
						if len(values) == 0:
							values.append(-1)
							catchExceptions(method)
						else:
							for value in values:
								catchExceptions(lambda: method(*value))

						failed = len(errors) > 0
						summary[failed] += 1
					else:
						summary[t] += 1

					print(("   " if t is not GeneratedTestType.NORMAL else str(len(values) - len(errors)) + "/" + str(len(values))) + "   %-19s" % (getTypeMessage(t, failed)) + ("" if generated else "   MANUAL"))
					if failed:
						printErrors(errors)
					elif t == GeneratedTestType.NOT_RUN:
						printErrors([globalErrors[name]])

			# Check for sub-modules
			if hasattr(module, "submodules"):
				runTests(spaceCenter, module.instance, module.submodules)
		except (Exception, RuntimeError) as ex:
			prettyPrint(Fore.RED + "*** Testing FAILED *** " + Fore.YELLOW + type(ex).__name__ + ": " + str(ex))
			failedModules.append(module)
			raise ex

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

	ManeuverPlannerTest(),
	SmartAssTest(),
	SmartRcsTest(),
	TranslatronTest(),

	NodeExecutorTest(),
	RcsControllerTest(),
	StagingControllerTest(),
	TargetControllerTest(),
	ThrustControllerTest(),
]

# Test modules
initColoredOutput(True)
runTests(conn.space_center, conn.mech_jeb, modules)

# Print summary
def printSummary(message, value):
	if value != 0:
		print(message + ": " + str(value))

print()
printSummary(getTypeMessage(GeneratedTestType.NORMAL, False), summary[False])
printSummary(getTypeMessage(GeneratedTestType.READ_ONLY, False), summary[GeneratedTestType.READ_ONLY])
printSummary(getTypeMessage(GeneratedTestType.MISSING, False), summary[GeneratedTestType.MISSING])
printSummary(getTypeMessage(GeneratedTestType.NORMAL, True), summary[True])
printSummary(getTypeMessage(GeneratedTestType.NOT_RUN, False), summary[GeneratedTestType.NOT_RUN])

# TODO: print failed modules

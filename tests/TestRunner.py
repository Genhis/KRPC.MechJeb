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
		# Extract parameters if the error is from a parameterized method
		parameters = ""
		if type(error) is tuple:
			if len(error[1]) > 0:
				parameters = "\n  * Method parameters: " + str(error[1]).replace(",)", ")").replace("'", "\"")
			error = error[0]

		lines = (type(error).__name__ + ": " + str(error) + parameters).split("\n")
		if type(error) is not krpc.error.RPCError:
			# If the exception is not a remote type, print it as-is
			for line in lines:
				prettyPrint(Fore.BLACK + line)
		else:
			# Remove unnecessary information from remote exceptions
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
def runTests(spaceCenter, mechJeb, parentInstance, modules):
	global indent

	for module in modules:
		print()
		prettyPrint("Testing module " + Fore.YELLOW + (module.name if module.name == module.className else module.className + "[" + module.name + "]") + Fore.RESET + ":")
		indent += 1
	
		try:
			globalErrors = module.setInstance(spaceCenter, mechJeb, parentInstance)
			members = inspect.getmembers(module, inspect.ismethod)
			for name, method in members:
				if Annotations.hasAnnotation(method, Annotations.BeforeClass):
					prettyPrint("Setting up environment...")
					getattr(module, name)()

			for name, method in members:
				if Annotations.hasAnnotation(method, Annotations.Test):
					prettyPrint(("Calling %-" + str(44 - indentMultiplier * indent) + "s") % (name + "()"), " ")

					errors = []
					values = []
					method = getattr(module, name)

					def catchExceptions(callable, parameters = ()):
						try:
							callable()
						except (Exception, RuntimeError) as ex:
							errors.append((ex, parameters))
							
					failed = False
					generated = Annotations.hasAnnotation(method, Annotations.GeneratedTest)
					t = Annotations.GeneratedTest.getType(method) if generated else GeneratedTestType.NORMAL
					def runNormal():
						nonlocal failed, values, t

						if Annotations.hasAnnotation(method, Annotations.ParameterizedTest):
							for param in Annotations.ParameterizedTest.getParameters(method):
								v = param(module) if callable(param) else param
								if len(v) == 0:
									t = GeneratedTestType.NOT_RUN
									globalErrors[name] = TestGeneratorException("Not enough parameters")
									return

								values.append(v)
							values = list(itertools.product(*values))

						if len(values) == 0:
							values.append(-1)
							catchExceptions(method)
						else:
							for value in values:
								catchExceptions(lambda: method(*value), value)

						failed = len(errors) > 0
						summary[failed] += 1

					if t is GeneratedTestType.NORMAL:
						runNormal()
					else:
						summary[t] += 1

					print(("         " if t is not GeneratedTestType.NORMAL else "%3d / %-3d" % (len(values) - len(errors), len(values))) + "   %-19s" % (getTypeMessage(t, failed)) + ("" if generated else "   MANUAL"))
					if failed:
						printErrors(errors)
					elif t == GeneratedTestType.NOT_RUN:
						printErrors([globalErrors[name]])

			# Check for sub-modules
			if hasattr(module, "submodules"):
				runTests(spaceCenter, mechJeb, module.instance, module.submodules)
		except (Exception, RuntimeError) as ex:
			prettyPrint(Fore.RED + "*** Testing FAILED *** " + Fore.YELLOW + type(ex).__name__ + ": " + str(ex))
			failedModules.append(module)
			raise ex

		indent -= 1

# Initialize kRPC
conn = krpc.connect("KRPC.MechJeb tests")
mj = conn.mech_jeb

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

	AntennaControllerTest(),
	NodeExecutorTest(),
	RcsControllerTest(),
	SolarPanelControllerTest(),
	StagingControllerTest(),
	TargetControllerTest(),
	ThrustControllerTest(),
]

# Test modules
initColoredOutput(True)
runTests(conn.space_center, mj, mj, modules)

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

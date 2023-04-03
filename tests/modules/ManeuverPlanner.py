from .ComputerModule import ModuleTest
from .Maneuver import *

class ManeuverPlannerTest(ModuleTest):
	def __init__(self):
		super().__init__("maneuver_planner", "ManeuverPlanner")
		self.submodules = [
			OperationApoapsisTest(),
			OperationCircularizeTest(),
			OperationCourseCorrectionTest(),
			OperationEllipticizeTest(),
			OperationInclinationTest(),
			OperationInterplanetaryTransferTest(),
			OperationKillRelVelTest(),
			OperationLambertTest(),
			OperationLanTest(),
			OperationLongitudeTest(),
			OperationMoonReturnTest(),
			OperationPeriapsisTest(),
			OperationPlaneTest(),
			OperationResonantOrbitTest(),
			OperationSemiMajorTest(),
			OperationTransferTest(),
		]

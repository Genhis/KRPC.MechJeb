from .Annotations import Test
from .ComputerModule import ComputerModuleTest

class TargetControllerTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("target_controller", "TargetController")

	@Test
	def target_orbit(self):
		self.assertFail("NullReferenceException: Object reference not set to an instance of an object\n  KRPC.SpaceCenter.Services.Orbit.GetHashCode () (at <f3e476445b794afca74822afa2c12a0e>:0)")

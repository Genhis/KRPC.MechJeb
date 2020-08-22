from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class LandingAutopilotTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

	@Test(InputType.NONE)
	def land_at_position_target(self):
		self.assertFail()

	@Test(InputType.NONE)
	def land_untargeted(self):
		self.assertFail()

	@Test(InputType.NONE)
	def stop_landing(self):
		self.assertFail()

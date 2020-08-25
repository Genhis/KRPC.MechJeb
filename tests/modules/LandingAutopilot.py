from .Annotations import InputType, Test
from .ComputerModule import AutopilotModuleTest

class LandingAutopilotTest(AutopilotModuleTest):
	def __init__(self):
		super().__init__("landing_autopilot", "LandingAutopilot")

	@Test(InputType.NONE)
	def land_at_position_target(self):
		self.assertFail()

	@Test(InputType.NONE)
	def land_untargeted(self):
		self.assertFail()

	@Test(InputType.NONE)
	def stop_landing(self):
		self.assertFail()

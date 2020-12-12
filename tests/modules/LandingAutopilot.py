from .Annotations import Test
from .ComputerModule import AutopilotModuleTest

class LandingAutopilotTest(AutopilotModuleTest):
	def __init__(self):
		super().__init__("landing_autopilot", "LandingAutopilot")

	@Test
	def land_at_position_target(self):
		self.assertFail()

	@Test
	def land_untargeted(self):
		self.assertFail()

	@Test
	def stop_landing(self):
		self.assertFail()

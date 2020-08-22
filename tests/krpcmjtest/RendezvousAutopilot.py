from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class RendezvousAutopilotTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("rendezvous_autopilot", "RendezvousAutopilot")

	@Test(InputType.NONE)
	def status(self):
		self.assertFail()

from .Annotations import Test
from .ComputerModule import ComputerModuleTest

class RendezvousAutopilotTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("rendezvous_autopilot", "RendezvousAutopilot")

	@Test
	def status(self):
		self.assertFail()

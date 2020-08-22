from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class RendezvousAutopilotTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

	@Test(InputType.NONE)
	def status(self):
		self.assertFail()

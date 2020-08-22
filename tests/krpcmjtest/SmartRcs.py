from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class SmartRcsTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

	@Test(InputType.NONE)
	def mode(self):
		self.assertFail()

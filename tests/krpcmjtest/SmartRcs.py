from .Annotations import InputType, Test
from .ComputerModule import DisplayModuleTest

class SmartRcsTest(DisplayModuleTest):
	def __init__(self):
		super().__init__("smart_rcs", "SmartRcs")

	@Test(InputType.NONE)
	def mode(self):
		self.assertFail()

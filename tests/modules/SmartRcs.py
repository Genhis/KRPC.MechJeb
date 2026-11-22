from .Annotations import Test
from .ComputerModule import DisplayModuleTest

class SmartRcsTest(DisplayModuleTest):
	def __init__(self):
		super().__init__("smart_rcs", "SmartRcs")

	@Test
	def mode(self):
		self.assertFail()

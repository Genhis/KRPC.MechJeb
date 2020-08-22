from .Annotations import InputType, Test
from .ComputerModule import DisplayModuleTest

class SmartAssTest(DisplayModuleTest):
	def __init__(self):
		super().__init__("smart_ass", "SmartAss")

	@Test(InputType.NONE)
	def interface_mode(self):
		self.assertFail()

	@Test(InputType.NONE)
	def autopilot_mode(self):
		self.assertFail()

	@Test(InputType.NONE)
	def advanced_reference(self):
		self.assertFail()

	@Test(InputType.NONE)
	def advanced_direction(self):
		self.assertFail()

	@Test(InputType.NONE)
	def update(self):
		self.assertFail()

from .Annotations import Test
from .ComputerModule import DisplayModuleTest

class SmartAssTest(DisplayModuleTest):
	def __init__(self):
		super().__init__("smart_ass", "SmartAss")

	@Test
	def interface_mode(self):
		self.assertFail()

	@Test
	def autopilot_mode(self):
		self.assertFail()

	@Test
	def advanced_reference(self):
		self.assertFail()

	@Test
	def advanced_direction(self):
		self.assertFail()

	@Test
	def update(self):
		self.assertFail()

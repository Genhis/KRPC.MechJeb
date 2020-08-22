from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class SmartAssTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

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

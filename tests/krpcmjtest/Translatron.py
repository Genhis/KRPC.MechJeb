from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class TranslatronTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

	@Test(InputType.NONE)
	def mode(self):
		self.assertFail()

	@Test(InputType.NONE)
	def panic_switch(self):
		self.assertFail()

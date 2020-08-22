from .Annotations import InputType, Test
from .ComputerModule import DisplayModuleTest

class TranslatronTest(DisplayModuleTest):
	def __init__(self):
		super().__init__("translatron", "Translatron")

	@Test(InputType.NONE)
	def mode(self):
		self.assertFail()

	@Test(InputType.NONE)
	def panic_switch(self):
		self.assertFail()

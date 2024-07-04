from .Annotations import Test
from .ComputerModule import DisplayModuleTest

class TranslatronTest(DisplayModuleTest):
	def __init__(self):
		super().__init__("translatron", "Translatron")

	@Test
	def mode(self):
		self.assertFail()

	@Test
	def panic_switch(self):
		self.assertFail()

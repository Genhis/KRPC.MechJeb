from .Annotations import InputType, Test
from .TestCase import TestCase

class ComputerModuleTest(TestCase):
	def __init__(self, conn, type, instance):
		super().__init__(type)
		self.conn = conn
		self.instance = instance

	@Test(InputType.BOOLEAN)
	def testEnabled(self, value):
		self.instance.enabled = value
		self.assertEquals(value, self.instance.enabled)

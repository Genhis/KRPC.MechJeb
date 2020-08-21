from .Annotations import InputType, Test
from .TestCase import TestCase
from .Util import toSnakeCase

class ComputerModuleTest(TestCase):
	def __init__(self, type):
		super().__init__(type)

	def setInstance(self, conn, spacecenter, mechjeb):
		self.conn = conn
		self.sc = spacecenter
		self.instance = getattr(mechjeb, toSnakeCase(self.type))

	@Test(InputType.BOOLEAN)
	def testEnabled(self, value):
		self.instance.enabled = value
		self.assertEquals(value, self.instance.enabled)

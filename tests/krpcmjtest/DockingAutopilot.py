from .Annotations import InputType, BeforeClass, Test
from .ComputerModule import ComputerModuleTest

class DockingAutopilotTest(ComputerModuleTest):
	def __init__(self, conn, type, instance):
		super().__init__(conn, type, instance)

	@BeforeClass
	def beforeClass(self):
		#self.conn.space_center.
		pass

	@Test(InputType.FLOAT)
	def speedLimit(self, value):
		self.instance.speed_limit = value
		self.assertEquals(value, self.instance.speed_limit)

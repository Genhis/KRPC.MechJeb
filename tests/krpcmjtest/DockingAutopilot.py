from .Annotations import InputType, BeforeClass, Test
from .ComputerModule import ComputerModuleTest

class DockingAutopilotTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

	#@BeforeClass
	def beforeClass(self):
		#self.conn.space_center.
		pass

	@Test(InputType.NONE)
	def status(self):
		self.assertFail()

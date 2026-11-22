from .Annotations import BeforeClass, Test
from .ComputerModule import ComputerModuleTest

class DockingAutopilotTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("docking_autopilot", "DockingAutopilot")

	#@BeforeClass
	def beforeClass(self):
		#self.conn.space_center.
		pass
	
	@Test
	def status(self):
		self.assertFail()

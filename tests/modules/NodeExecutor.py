from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class NodeExecutorTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("node_executor", "NodeExecutor")

	@Test(InputType.NONE)
	def abort(self):
		self.assertFail()

	@Test(InputType.NONE)
	def execute_all_nodes(self):
		self.assertFail()

	@Test(InputType.NONE)
	def execute_one_node(self):
		self.assertFail()

from .Annotations import Test
from .ComputerModule import ComputerModuleTest

class NodeExecutorTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("node_executor", "NodeExecutor")

	@Test
	def abort(self):
		self.assertFail()

	@Test
	def execute_all_nodes(self):
		self.assertFail()

	@Test
	def execute_one_node(self):
		self.assertFail()

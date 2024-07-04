from .Annotations import BeforeClass
from .ComputerModule import ComputerModuleTest

class AscentGtTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("ascent_path_gt", "AscentGt")

	@BeforeClass
	def setUp(self):
		self.parent.ascent_path_index = 1

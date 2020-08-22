from .Annotations import InputType, BeforeClass, Test
from .ComputerModule import ComputerModuleTest

class AscentClassicTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("ascent_path_classic", "AscentClassic")

	@BeforeClass
	def setUp(self):
		self.parent.ascent_path_index = 0

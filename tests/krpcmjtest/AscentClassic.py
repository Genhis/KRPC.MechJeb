from .Annotations import InputType, BeforeClass, Test
from .AscentAutopilot import AscentBaseTest

class AscentClassicTest(AscentBaseTest):
	def __init__(self, type):
		super().__init__(type)

	@BeforeClass
	def setUp(self):
		self.instance.ascent_path_index = 0

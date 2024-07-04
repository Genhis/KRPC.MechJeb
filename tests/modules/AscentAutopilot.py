from .Annotations import ParameterizedTest, Test
from .AscentClassic import AscentClassicTest
from .AscentGt import AscentGtTest
from .ComputerModule import ComputerModuleTest

class AscentAutopilotTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("ascent_autopilot", "AscentAutopilot")
		self.submodules = [
			AscentClassicTest(),
			AscentGtTest(),
		]

	@Test
	def status(self):
		self.assertFail()

	@ParameterizedTest([0, 1, 2])
	def ascent_path_index_valid(self, value):
		self.instance.ascent_path_index = value;
		self.assertEquals(value, self.instance.ascent_path_index)

	@ParameterizedTest([-1, 3])
	def ascent_path_index_invalid(self, value):
		currentIndex = self.instance.ascent_path_index;
		self.instance.ascent_path_index = value;
		self.assertEquals(currentIndex, self.instance.ascent_path_index)

	@Test
	def ascent_path_classic(self):
		self.assertType("AscentClassic", "ascent_path_classic")

	@Test
	def ascent_path_gt(self):
		self.assertType("AscentGT", "ascent_path_gt")

	@Test
	def ascent_path_pvg(self):
		self.assertType("AscentPVG", "ascent_path_pvg")

	@Test
	def launch_mode(self):
		self.assertFail()

	@Test
	def abort_timed_launch(self):
		self.assertFail()

	@Test
	def launch_to_rendezvous(self):
		self.assertFail()

	@Test
	def launch_to_target_plane(self):
		self.assertFail()

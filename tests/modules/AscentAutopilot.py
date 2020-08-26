from .Annotations import Test
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

	@Test
	def ascent_path_index(self):
		self.instance.ascent_path_index = 0;
		self.assertEquals(0, self.instance.ascent_path_index)

		self.instance.ascent_path_index = 1;
		self.assertEquals(1, self.instance.ascent_path_index)

		self.instance.ascent_path_index = 2;
		self.assertEquals(2, self.instance.ascent_path_index)

	@Test
	def ascent_path_index_invalid(self):
		self.instance.ascent_path_index = -1;
		self.assertInRange(0, 2, self.instance.ascent_path_index)

		self.instance.ascent_path_index = 3;
		self.assertInRange(0, 2, self.instance.ascent_path_index)

	@Test
	def ascent_path_classic(self):
		self.assertIsObject(self.instance.ascent_path_classic)

	@Test
	def ascent_path_gt(self):
		self.assertIsObject(self.instance.ascent_path_gt)

	@Test
	def ascent_path_pvg(self):
		self.assertIsObject(self.instance.ascent_path_pvg)

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

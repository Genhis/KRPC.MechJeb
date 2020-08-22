from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class AscentAutopilotTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

	@Test(InputType.NONE)
	def status(self):
		self.assertFail()

	@Test(InputType.NONE)
	def ascent_path_index(self):
		self.instance.ascent_path_index = 0;
		self.assertEquals(0, self.instance.ascent_path_index)

		self.instance.ascent_path_index = 1;
		self.assertEquals(1, self.instance.ascent_path_index)

		self.instance.ascent_path_index = 2;
		self.assertEquals(2, self.instance.ascent_path_index)
		
	@Test(InputType.NONE)
	def ascent_path_index_invalid(self):
		self.instance.ascent_path_index = -1;
		self.assertInRange(0, 2, self.instance.ascent_path_index)
		
		self.instance.ascent_path_index = 3;
		self.assertInRange(0, 2, self.instance.ascent_path_index)

	@Test(InputType.NONE)
	def ascent_path_classic(self):
		self.assertIsObject(self.instance.ascent_path_classic)

	@Test(InputType.NONE)
	def ascent_path_gt(self):
		self.assertIsObject(self.instance.ascent_path_gt)

	@Test(InputType.NONE)
	def ascent_path_pvg(self):
		self.assertIsObject(self.instance.ascent_path_pvg)

	@Test(InputType.NONE)
	def launch_mode(self):
		self.assertFail()

	@Test(InputType.NONE)
	def abort_timed_launch(self):
		self.assertFail()

	@Test(InputType.NONE)
	def launch_to_rendezvous(self):
		self.assertFail()

	@Test(InputType.NONE)
	def launch_to_target_plane(self):
		self.assertFail()
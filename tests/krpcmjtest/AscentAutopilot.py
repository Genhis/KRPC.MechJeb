from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class AscentAutopilotTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

	@Test(InputType.NONE)
	def status(self):
		self.assertFail()

	@Test(InputType.NONE)
	def ascentPathIndexValid(self):
		self.instance.ascent_path_index = 0;
		self.assertEquals(0, self.instance.ascent_path_index)

		self.instance.ascent_path_index = 1;
		self.assertEquals(1, self.instance.ascent_path_index)

		self.instance.ascent_path_index = 2;
		self.assertEquals(2, self.instance.ascent_path_index)
		
	@Test(InputType.NONE)
	def ascentPathIndexInvalid(self):
		self.instance.ascent_path_index = -1;
		self.assertInRange(0, 2, self.instance.ascent_path_index)
		
		self.instance.ascent_path_index = 3;
		self.assertInRange(0, 2, self.instance.ascent_path_index)

	@Test(InputType.NONE)
	def ascentPathClassic(self):
		self.assertIsObject(self.instance.ascent_path_classic)

	@Test(InputType.NONE)
	def ascentPathGT(self):
		self.assertIsObject(self.instance.ascent_path_gt)

	@Test(InputType.NONE)
	def ascentPathPVG(self):
		self.assertIsObject(self.instance.ascent_path_pvg)

	@Test(InputType.FLOAT)
	def desiredOrbitAltitude(self, value):
		self.instance.desired_orbit_altitude = value
		self.assertEquals(value, self.instance.desired_orbit_altitude)

	@Test(InputType.FLOAT)
	def desiredInclination(self, value):
		self.instance.desired_inclination = value
		self.assertEquals(value, self.instance.desired_inclination)

	@Test(InputType.BOOLEAN)
	def correctiveSteering(self, value):
		self.instance.corrective_steering = value
		self.assertEquals(value, self.instance.corrective_steering)

	@Test(InputType.FLOAT)
	def correctiveSteeringGain(self, value):
		self.instance.corrective_steering_gain = value
		self.assertEquals(value, self.instance.corrective_steering_gain)

	@Test(InputType.BOOLEAN)
	def forceRoll(self, value):
		self.instance.force_roll = value
		self.assertEquals(value, self.instance.force_roll)

	@Test(InputType.FLOAT)
	def verticalRoll(self, value):
		self.instance.vertical_roll = value
		self.assertEquals(value, self.instance.vertical_roll)

	@Test(InputType.FLOAT)
	def turnRoll(self, value):
		self.instance.turn_roll = value
		self.assertEquals(value, self.instance.turn_roll)

	@Test(InputType.BOOLEAN)
	def autodeploySolarPanels(self, value):
		self.instance.autodeploy_solar_panels = value
		self.assertEquals(value, self.instance.autodeploy_solar_panels)

	@Test(InputType.BOOLEAN)
	def AutoDeployAntennas(self, value):
		self.instance.auto_deploy_antennas = value
		self.assertEquals(value, self.instance.auto_deploy_antennas)

	@Test(InputType.BOOLEAN)
	def skipCircularization(self, value):
		self.instance.skip_circularization = value
		self.assertEquals(value, self.instance.skip_circularization)

	@Test(InputType.BOOLEAN)
	def correctiveSteering(self, value):
		self.instance.corrective_steering = value
		self.assertEquals(value, self.instance.corrective_steering)

	@Test(InputType.FLOAT)
	def correctiveSteeringGain(self, value):
		self.instance.corrective_steering_gain = value
		self.assertEquals(value, self.instance.corrective_steering_gain)

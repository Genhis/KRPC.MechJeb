from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class AscentAutopilotTest(ComputerModuleTest):
	def __init__(self, conn, type, instance):
		super().__init__(conn, type, instance)

	@Test(InputType.FLOAT)
	def desiredOrbitAltitude(self, value):
		self.instance.desired_orbit_altitude = value
		self.assertEquals(value, self.instance.desired_orbit_altitude)


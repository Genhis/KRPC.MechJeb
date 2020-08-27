from ..Annotations import ParameterizedTest, Test
from .Operation import TimedOperationTest, ManeuverTest

class OperationCircularizeTest(TimedOperationTest):
	def __init__(self):
		super().__init__("operation_circularize", "OperationCircularize")
		self.submodules[0].validTimeReferences = ["altitude", "apoapsis", "periapsis", "x_from_now"]

	@Test
	def error_message(self):
		# No error message is expected here
		self.assertEquals("", self.instance.error_message)

	def make_nodes(self, timeReference):
		self.instance.time_selector.time_reference = getattr(self.mechJeb.TimeReference, timeReference)
		nodes = self.instance.make_nodes()
		
		self.error_message()
		self.assertEquals(1, len(nodes), "len(nodes)")

		newOrbit = nodes[0].orbit
		self.assertEquals(newOrbit.apoapsis_altitude, newOrbit.periapsis_altitude, "circularize")
		self.testOrbit(newOrbit, ["inclination"])
		self.assertEquals(None, newOrbit.next_orbit, "next_orbit")

		return nodes[0]
	
	@ManeuverTest
	@ParameterizedTest([115000, 150000, 180000])
	def make_nodes_altitude(self, altitude):
		self.instance.time_selector.circularize_altitude = altitude
		self.testTimeReference(self.make_nodes("altitude"), "altitude", circularizeAltitude = altitude)

	@ManeuverTest
	def make_nodes_apoapsis(self):
		node = self.make_nodes("apoapsis")
		self.assertEquals(self.oldOrbit.apoapsis_altitude, node.orbit.periapsis_altitude, "time_check")
		self.testTimeReference(node, "apoapsis")

	@ManeuverTest
	def make_nodes_periapsis(self):
		node = self.make_nodes("periapsis")
		self.assertEquals(self.oldOrbit.periapsis_altitude, node.orbit.apoapsis_altitude, "time_check")
		self.testTimeReference(node, "periapsis")
		
	@ManeuverTest
	@ParameterizedTest([314, 587.4, 34.751])
	def make_nodes_timed(self, leadTime):
		self.instance.time_selector.lead_time = leadTime
		self.testTimeReference(self.make_nodes("x_from_now"), "x_from_now", leadTime)

	@ManeuverTest
	@ParameterizedTest([80000, 112450, 187550, 240000])
	def make_nodes_altitude_invalid(self, altitude):
		self.instance.time_selector.circularize_altitude = altitude
		self.instance.time_selector.time_reference = self.mechJeb.TimeReference.altitude
		try:
			self.instance.make_nodes()
			self.assertFail("Expected OperationException")
		except self.mechJeb.OperationException as ex:
			if not str(ex).startswith("Warning: can't circularize at this altitude, since current orbit does not reach it."):
				raise ex

from ..Annotations import ParameterizedTest, Test
from .Operation import TimedOperationTest, ManeuverTest

class OperationPeriapsisTest(TimedOperationTest):
	def __init__(self):
		super().__init__("operation_periapsis", "OperationPeriapsis")
		self.submodules[0].validTimeReferences = ["apoapsis", "periapsis", "x_from_now"]

	@Test
	def error_message(self):
		# No error message is expected here
		self.assertEquals("", self.instance.error_message)

	def make_nodes(self, periapsis, timeReference):
		self.instance.new_periapsis = periapsis
		self.instance.time_selector.time_reference = getattr(self.mechJeb.TimeReference, timeReference)
		nodes = self.instance.make_nodes()

		self.error_message()
		self.assertEquals(1, len(nodes), "len(nodes)")

		newOrbit = nodes[0].orbit
		self.assertEquals(periapsis, newOrbit.periapsis_altitude, "periapsis")
		self.testOrbit(newOrbit, ["inclination"])
		self.assertEquals(None, newOrbit.next_orbit, "next_orbit")

		return nodes[0]

	@ManeuverTest
	@ParameterizedTest([80000, 75341, 70821.649], ["apoapsis", "periapsis"])
	def make_nodes_valid(self, periapsis, timeReference):
		self.testTimeReference(self.make_nodes(periapsis, timeReference), timeReference)

	@ManeuverTest
	@ParameterizedTest([80000, 75341, 70821.649], [314, 587.4, 34.751])
	def make_nodes_timed(self, periapsis, leadTime):
		self.instance.time_selector.lead_time = leadTime
		self.testTimeReference(self.make_nodes(periapsis, "x_from_now"), "x_from_now", leadTime)

	@ManeuverTest
	@ParameterizedTest([280000, 375000, 935821.649], lambda self: self.submodules[0].validTimeReferences)
	def make_nodes_invalid(self, periapsis, timeReference):
		self.instance.new_periapsis = periapsis
		self.instance.time_selector.time_reference = getattr(self.mechJeb.TimeReference, timeReference)
		try:
			self.instance.make_nodes()
			self.assertFail("Expected OperationException for values (" + str(apoapsis) + ", " + timeReference + ")")
		except self.mechJeb.OperationException as ex:
			if not str(ex).startswith("new periapsis cannot be higher than the altitude of the burn"):
				raise ex

from ..Annotations import ParameterizedTest, Test
from .Operation import TimedOperationTest, ManeuverTest

class OperationApoapsisTest(TimedOperationTest):
	def __init__(self):
		super().__init__("operation_apoapsis", "OperationApoapsis")
		self.submodules[0].validTimeReferences = ["apoapsis", "eq_ascending", "eq_descending", "periapsis", "x_from_now"]

	@Test
	def error_message(self):
		# No error message is expected here
		self.assertEquals("", self.instance.error_message)

	def make_nodes(self, apoapsis, timeReference):
		self.instance.new_apoapsis = apoapsis
		self.instance.time_selector.time_reference = getattr(self.mechJeb.TimeReference, timeReference)
		nodes = self.instance.make_nodes()

		self.error_message()
		self.assertEquals(1, len(nodes), "len(nodes)")

		newOrbit = nodes[0].orbit
		self.assertEquals(apoapsis, newOrbit.apoapsis_altitude, "apoapsis")
		self.testOrbit(newOrbit, ["inclination"])
		self.assertEquals(None, newOrbit.next_orbit, "next_orbit")

		return nodes[0]

	@ManeuverTest
	@ParameterizedTest([280000, 375000, 935821.649], ["apoapsis", "eq_ascending", "eq_descending", "periapsis"])
	def make_nodes_valid(self, apoapsis, timeReference):
		self.testTimeReference(self.make_nodes(apoapsis, timeReference), timeReference)

	@ManeuverTest
	@ParameterizedTest([280000, 375000, 935821.649], [314, 587.4, 34.751])
	def make_nodes_timed(self, apoapsis, leadTime):
		self.instance.time_selector.lead_time = leadTime
		self.testTimeReference(self.make_nodes(apoapsis, "x_from_now"), "x_from_now", leadTime)

	@ManeuverTest
	@ParameterizedTest([80000, 75341, 70821.649], lambda self: self.submodules[0].validTimeReferences)
	def make_nodes_invalid(self, apoapsis, timeReference):
		self.instance.new_apoapsis = apoapsis
		self.instance.time_selector.time_reference = getattr(self.mechJeb.TimeReference, timeReference)
		try:
			self.instance.make_nodes()
			self.assertFail("Expected OperationException")
		except self.mechJeb.OperationException as ex:
			if not str(ex).startswith("new apoapsis cannot be lower than the altitude of the burn"):
				raise ex

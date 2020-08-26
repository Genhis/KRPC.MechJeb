from ..Annotations import ParameterizedTest
from .Operation import TimedOperationTest, ManeuverTest

class OperationApoapsisTest(TimedOperationTest):
	def __init__(self):
		super().__init__("operation_apoapsis", "OperationApoapsis")
		self.submodules[0].validTimeReferences = ["apoapsis", "eq_ascending", "eq_descending", "periapsis", "x_from_now"]

	@ParameterizedTest([280000, 375000, 935821.649], lambda self: self.submodules[0].validTimeReferences)
	@ManeuverTest
	def make_nodes_valid(self, apoapsis, timeReference):
		leadTime = 314

		self.instance.new_apoapsis = apoapsis
		self.instance.time_selector.time_reference = getattr(self.TimeReference, timeReference)
		self.instance.time_selector.lead_time = leadTime
		nodes = self.instance.make_nodes()

		self.assertEquals(1, len(nodes), "len(nodes)")

		newOrbit = nodes[0].orbit
		self.assertEquals(apoapsis, newOrbit.apoapsis_altitude, "apoapsis")
		self.testOrbit(newOrbit, ["inclination"])
		self.assertEquals(None, newOrbit.next_orbit, "next_orbit")
		self.testTimeReference(nodes[0], newOrbit, timeReference, leadTime)

	@ParameterizedTest([80000, 75341, 70821.649], lambda self: self.submodules[0].validTimeReferences)
	@ManeuverTest
	def make_nodes_invalid(self, apoapsis, timeReference):
		self.instance.new_apoapsis = apoapsis
		self.instance.time_selector.time_reference = getattr(self.TimeReference, timeReference)
		try:
			self.instance.make_nodes()
			self.assertFail("Expected OperationException for values (" + str(apoapsis) + ", " + timeReference + ")")
		except self.mechJeb.OperationException as ex:
			if not str(ex).startswith("new apoapsis cannot be lower than the altitude of the burn"):
				raise ex

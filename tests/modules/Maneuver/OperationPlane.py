from ..Annotations import BeforeClass, ParameterizedTest, Test
from .Operation import TimedOperationTest, ManeuverTest

class OperationPlaneTest(TimedOperationTest):
	def __init__(self):
		super().__init__("operation_plane", "OperationPlane")
		self.submodules[0].validTimeReferences = ["rel_ascending", "rel_descending", "rel_highest_ad", "rel_nearest_ad"]

	@Test
	def error_message(self):
		# No error message is expected here
		self.assertEquals("", self.instance.error_message)

	def make_nodes(self, target, timeReference):
		target = self.spaceCenter.bodies[target]
		self.spaceCenter.target_body = target
		self.instance.time_selector.time_reference = getattr(self.mechJeb.TimeReference, timeReference)
		nodes = self.instance.make_nodes()

		self.error_message()
		self.assertEquals(1, len(nodes), "len(nodes)")

		newOrbit = nodes[0].orbit
		self.assertEquals(target.orbit.inclination, newOrbit.inclination, "inclination")
		self.testOrbit(newOrbit, ["apoapsis", "periapsis"])
		self.assertEquals(None, newOrbit.next_orbit, "next_orbit")

		return nodes[0]

	@ManeuverTest
	@ParameterizedTest(["Minmus", "Mun"], lambda self: self.submodules[0].validTimeReferences)
	def make_nodes_valid(self, target, timeReference):
		self.testTimeReference(self.make_nodes(target, timeReference), timeReference)

	@ManeuverTest
	@ParameterizedTest(lambda self: self.submodules[0].validTimeReferences)
	def make_nodes_no_target(self, timeReference):
		self.spaceCenter.clear_target()
		self.testOperationException(timeReference, "must select a target to match planes with.")

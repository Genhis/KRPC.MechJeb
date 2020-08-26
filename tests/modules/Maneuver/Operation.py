import functools

from ..Annotations import GeneratedTestType, BeforeClass, GeneratedTest, Test
from ..TestCase import TestCase
from .TimeSelector import TimeSelectorTest

def ManeuverTest(func):
	@functools.wraps(func)
	def wrapper(*args, **kwargs):
		try:
			return func(*args, **kwargs)
		finally:
			args[0].activeVessel.control.remove_nodes()

	return wrapper

class OperationTest(TestCase):
	def __init__(self, variable, name):
		super().__init__(variable, name)

	def testOrbit(self, newOrbit, properties):
		for propertyName in properties:
			self.assertEquals(getattr(self.oldOrbit, propertyName), getattr(newOrbit, propertyName), propertyName)

	@BeforeClass
	def setUp(self):
		self.activeVessel = self.spaceCenter.active_vessel
		self.oldOrbit = self.activeVessel.orbit
		self.TimeReference = self.mechJeb.TimeReference

	@ManeuverTest
	@Test
	def make_node(self):
		self.assertObject(self.instance.make_node())

class TimedOperationTest(OperationTest):
	def __init__(self, variable, name):
		super().__init__(variable, name)
		self.submodules = [
			TimeSelectorTest()
		]

	def testTimeReference(self, node, newOrbit, timeReference, leadTime = None, circularizeAltitude = None):
		if timeReference == "apoapsis":
			self.assertEquals(self.oldOrbit.time_to_apoapsis, node.time_to, "time_to_apoapsis")
		if timeReference == "eq_ascending":
			pass # TODO: what would be a suitable test?
		if timeReference == "eq_descending":
			pass # TODO: what would be a suitable test?
		elif timeReference == "periapsis":
			self.assertEquals(self.oldOrbit.time_to_periapsis, node.time_to, "time_to_periapsis")
		elif timeReference == "x_from_now":
			self.assertEquals(leadTime, node.time_to, "time_to_periapsis")

	@GeneratedTest(GeneratedTestType.MISSING)
	def make_nodes_eq_ascending(self):
		pass

	@GeneratedTest(GeneratedTestType.MISSING)
	def make_nodes_eq_descending(self):
		pass

	@Test
	def time_selector(self):
		self.assertType("TimeSelector", "time_selector")

import functools

from ..Annotations import GeneratedTestType, BeforeClass, GeneratedTest, Test
from ..TestCase import TestCase
from .TimeSelector import TimeSelectorTest

def ManeuverTest(func):
	@functools.wraps(func)
	@Test
	def wrapper(*args, **kwargs):
		try:
			return func(*args, **kwargs)
		finally:
			args[0].activeVessel.control.remove_nodes()

	return wrapper

class OperationTest(TestCase):
	def __init__(self, variable, name, className = None):
		super().__init__(variable, name, className)

	def testOrbit(self, newOrbit, properties):
		for propertyName in properties:
			self.assertEquals(getattr(self.oldOrbit, propertyName), getattr(newOrbit, propertyName), propertyName)

	@BeforeClass
	def setUp(self):
		self.activeVessel = self.spaceCenter.active_vessel
		self.oldOrbit = self.activeVessel.orbit

class TimedOperationTest(OperationTest):
	def __init__(self, variable, name, className = None):
		super().__init__(variable, name, className)
		self.submodules = [
			TimeSelectorTest()
		]

	def testTimeReference(self, node, timeReference, leadTime = None, circularizeAltitude = None):
		if timeReference == "altitude":
			pass # TODO: Check if the node altitude equals to circularizeAltitude - how to get node altitude?
		elif timeReference == "apoapsis":
			self.assertEquals(self.oldOrbit.time_to_apoapsis, node.time_to, "time_to_apoapsis")
		elif timeReference == "eq_ascending":
			pass # TODO: what would be a suitable test?
		elif timeReference == "eq_descending":
			pass # TODO: what would be a suitable test?
		elif timeReference == "periapsis":
			self.assertEquals(self.oldOrbit.time_to_periapsis, node.time_to, "time_to_periapsis")
		elif timeReference == "rel_ascending":
			pass # TODO: what would be a suitable test?
		elif timeReference == "rel_descending":
			pass # TODO: what would be a suitable test?
		elif timeReference == "rel_highest_ad":
			pass # TODO: what would be a suitable test?
		elif timeReference == "rel_nearest_ad":
			pass # TODO: what would be a suitable test?
		elif timeReference == "x_from_now":
			self.assertEquals(leadTime, node.time_to, "time_to_x_from_now")
		else:
			self.assertFail("Time reference is not supported: " + timeReference)

	def testOperationException(self, timeReference, errorMessage):
		self.instance.time_selector.time_reference = getattr(self.mechJeb.TimeReference, timeReference)
		try:
			self.instance.make_nodes()
			self.assertFail("Expected OperationException")
		except self.mechJeb.OperationException as ex:
			if not str(ex).startswith(errorMessage):
				raise ex

	@GeneratedTest(GeneratedTestType.MISSING)
	def time_reference_altitude(self):
		pass

	@GeneratedTest(GeneratedTestType.MISSING)
	def time_reference_eq_ascending(self):
		pass

	@GeneratedTest(GeneratedTestType.MISSING)
	def time_reference_eq_descending(self):
		pass

	@GeneratedTest(GeneratedTestType.MISSING)
	def time_reference_rel_ascending(self):
		pass

	@GeneratedTest(GeneratedTestType.MISSING)
	def time_reference_rel_descending(self):
		pass

	@GeneratedTest(GeneratedTestType.MISSING)
	def time_reference_rel_highest_ad(self):
		pass

	@GeneratedTest(GeneratedTestType.MISSING)
	def time_reference_rel_nearest_ad(self):
		pass

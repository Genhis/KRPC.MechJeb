import inspect

from .Annotations import Generated, Test
from .Util import toSnakeCase

class AssertionException(Exception):
	def __init__(self, message):
		self.message = message

class TestCase:
	def __init__(self, type):
		self.type = type

	def setInstance(self, spacecenter, mechjeb):
		self.sc = spacecenter
		self.instance = getattr(mechjeb, toSnakeCase(self.type))

		def generateTest(type, name):
			@Generated
			@Test(type)
			def test(self, value):
				setattr(self.instance, name, value)
				self.assertEquals(value, getattr(self.instance, name))
			
			return test

		# Create default testing methods if they don't exist
		overriddenTests = list(name for name, method in inspect.getmembers(self, inspect.ismethod) if not name.startswith("_") and hasAnnotation(method, Test))
		# TODO: Check for missing tests
		for name, attribute in inspect.getmembers(self.instance, lambda o: not inspect.ismethod(o)):
			if not name.startswith("_") and name not in overriddenTests:
				t = toInputType(type(attribute))
				if t != InputType.NONE:
					setattr(self, name, generateTest(t, name).__get__(self, self.__class__))

	def assertFail(self):
		raise AssertionException(self._getExceptionPrefix() + "Not implemented")

	def assertEquals(self, expected, actual):
		if expected != actual:
			raise AssertionException(self._getExceptionPrefix() + "Expected " + str(expected) + " but got " + str(actual))
	
	def assertTrue(self, message, actual):
		if not actual:
			raise AssertionException(self._getExceptionPrefix() + message)
	
	def assertFalse(self, message, actual):
		self.assertTrue(message, not actual)

	def assertInRange(self, a, b, actual):
		if actual < a or actual > b:
			raise AssertionException(self._getExceptionPrefix() + "Excepted a value in range <" + str(a) + ", " + str(b) + "> but got " + str(actual))

	def assertIsObject(self, value):
		#print(type(value))
		pass

	def _getExceptionPrefix(self):
		return self.type + " " + inspect.stack()[2].function + "() failed: "

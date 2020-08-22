import inspect

from .Annotations import InputType, Generated, Test, hasAnnotation, toInputType

class AssertionException(Exception):
	def __init__(self, message):
		self.message = message

class TestCase:
	def __init__(self, variable, name):
		self.variable = variable
		self.name = name

	def setInstance(self, spacecenter, parent):
		self.sc = spacecenter
		self.parent = parent
		self.instance = getattr(parent, self.variable)

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
		errors = {}
		# Need to iterate through names to catch potential attribute exceptions
		for name in dir(self.instance):
			if not name.startswith("_") and name not in overriddenTests:
				try:
					t = toInputType(type(getattr(self.instance, name)))
					if t != InputType.NONE:
						setattr(self, name, generateTest(t, name).__get__(self, self.__class__))
				except (Exception, RuntimeError) as ex:
					errors[name] = ex

		return errors

	def assertFail(self):
		raise AssertionException("Not implemented")

	def assertEquals(self, expected, actual):
		if expected != actual:
			raise AssertionException("Expected " + str(expected) + " but got " + str(actual))
	
	def assertTrue(self, message, actual):
		if not actual:
			raise AssertionException(message)
	
	def assertFalse(self, message, actual):
		self.assertTrue(message, not actual)

	def assertInRange(self, a, b, actual):
		if actual < a or actual > b:
			raise AssertionException("Excepted a value in range <" + str(a) + ", " + str(b) + "> but got " + str(actual))

	def assertIsObject(self, value):
		#print(type(value))
		pass

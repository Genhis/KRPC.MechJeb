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

		def generateEmpty(type, name):
			@Generated
			@Test(type)
			def test(self):
				# Represents read-only or missing tests
				# Read-only value is tested when determining its write access below
				# This methos is here only to keep track of tests in TestRunner
				pass

			return test

		# Create default testing methods if they don't exist
		overriddenTests = list(name for name, method in inspect.getmembers(self, inspect.ismethod) if not name.startswith("_") and hasAnnotation(method, Test))
		# TODO: Check for missing tests
		errors = {}
		# Need to iterate through names to catch potential attribute exceptions
		for name in dir(self.instance):
			if not name.startswith("_") and name not in overriddenTests:
				try:
					attribute = getattr(self.instance, name)

					generator = generateEmpty
					t = toInputType(type(attribute))
					if t != InputType.NONE:
						# Check if the attribute is read-only - is there a better way?
						try:
							setattr(self.instance, name, attribute)
							generator = generateTest
						except AttributeError:
							t = InputType.READ_ONLY
					else:
						t = InputType.MISSING

					setattr(self, name, generator(t, name).__get__(self, self.__class__))
				except (Exception, RuntimeError) as ex:
					print("error")
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

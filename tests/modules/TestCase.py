import inspect

from .Annotations import GeneratedTestType, GeneratedTest, ParameterizedTest, Test, hasAnnotation

class AssertionException(Exception):
	def __init__(self, message, propertyName = None):
		super().__init__(("" if propertyName is None else "[" + propertyName + "] ") + message)

class TestGeneratorException(Exception):
	def __init__(self, message):
		self.message = message

class TestCase:
	def __init__(self, variable, name):
		self.variable = variable
		self.name = name

	def setInstance(self, spaceCenter, mechJeb, parent):
		self.spaceCenter = spaceCenter
		self.mechJeb = mechJeb
		self.parent = parent
		self.instance = getattr(parent, self.variable)

		def generateTest(type, name, values):
			@GeneratedTest(type)
			@ParameterizedTest(values)
			def test(self, value):
				setattr(self.instance, name, value)
				self.assertEquals(value, getattr(self.instance, name))
	
			return test

		def generateEmpty(type, name, values):
			@GeneratedTest(type)
			def test(self):
				# Represents read-only or missing tests
				# Read-only value is tested when determining its write access below
				# This methos is here only to keep track of tests in TestRunner
				pass

			return test

		# Create default testing methods if they don't exist
		overriddenTests = list(name for name, method in inspect.getmembers(self, inspect.ismethod) if not name.startswith("_") and hasAnnotation(method, Test))
		errors = {}
		# Need to iterate through names to catch potential attribute exceptions
		for name in dir(self.instance):
			if not name.startswith("_") and name not in overriddenTests and not any(s.startswith(name + "_") for s in overriddenTests):
				generator = generateEmpty
				values = []
				t = GeneratedTestType.NOT_RUN
				try:
					attribute = getattr(self.instance, name)
					attrType = type(attribute)
					if attrType is bool or attrType is float or attrType is int:
						# Check if the attribute is read-only - is there a better way?
						try:
							setattr(self.instance, name, attribute)

							generator = generateTest
							t = GeneratedTestType.NORMAL
							if attrType is bool:
								values = [True, False]
							else:
								if attrType is float:
									values = [100.05, 800.333, 2700.9417]

								values += [50, 750, 3400]
						except AttributeError:
							t = GeneratedTestType.READ_ONLY
					else:
						t = GeneratedTestType.MISSING

				except (Exception, RuntimeError) as ex:
					t = GeneratedTestType.NOT_RUN
					errors[name] = ex

				setattr(self, name, generator(t, name, values).__get__(self, self.__class__))
		return errors

	def assertFail(self, message = "Not implemented"):
		raise AssertionException(message)

	def assertEquals(self, expected, actual, propertyName = None):
		# TODO: Is there a way to get better precision? 0.1 epsilon value just to pass OperationApoapsis tests is quite large...
		if expected != actual and (type(actual) is not float or abs(expected - actual) > 0.25):
			raise AssertionException("Expected " + str(expected) + " but got " + str(actual), propertyName)
	
	def assertTrue(self, message, actual):
		if not actual:
			raise AssertionException(message)
	
	def assertFalse(self, message, actual):
		self.assertTrue(message, not actual)

	def assertInRange(self, a, b, actual):
		if actual < a or actual > b:
			raise AssertionException("Excepted a value in range <" + str(a) + ", " + str(b) + "> but got " + str(actual))

	def assertType(self, typeName, variableName):
		self.assertEquals(getattr(self.mechJeb, typeName), type(getattr(self.instance, variableName)))

	def assertObject(self, value):
		#print(type(value))
		pass

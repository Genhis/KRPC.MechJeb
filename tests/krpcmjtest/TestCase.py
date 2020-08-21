import krpc
import inspect

class AssertionException(Exception):
	def __init__(self, message):
		self.message = message

class TestCase:
	def __init__(self, type):
		self.type = type

	def assertFail(self):
		raise AssertionException(self._getExceptionPrefix() + "Not implemented")

	def assertEquals(self, expected, actual):
		if expected != actual:
			raise AssertionException(self._getExceptionPrefix() + "Expected " + str(expected) + " but got " + str(actual))
		else:
			print("OK")
	
	def assertTrue(self, message, actual):
		if not actual:
			raise AssertionException(self._getExceptionPrefix() + message)
		else:
			print("OK")
	
	def assertFalse(self, message, actual):
		self.assertTrue(message, not actual)

	def assertInRange(self, a, b, actual):
		if actual < a or actual > b:
			raise AssertionException(self._getExceptionPrefix() + "Excepted a value in range <" + str(a) + ", " + str(b) + "> but got " + str(actual))
		else:
			print("OK")

	def assertIsObject(self, value):
		print(type(value))

	def _getExceptionPrefix(self):
		return self.type + " " + inspect.stack()[2].function + "() failed: "

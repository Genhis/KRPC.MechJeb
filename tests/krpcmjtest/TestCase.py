import inspect

class AssertionException(Exception):
	def __init__(self, message):
		self.message = message

class TestCase:
	def __init__(self, type):
		self.type = type

	def assertEquals(self, expected, actual):
		if expected != actual:
			raise self._getException(expected, actual)
		else:
			print("OK")
	
	def assertTrue(self, name, actual):
		assertEquals(name, True, actual)
	
	def assertFalse(self, name, actual):
		assertEquals(name, False, actual)

	def _getException(self, expected, actual):
		return AssertionException(self.type + " " + inspect.stack()[2].function + "() failed: Expected " + str(expected) + " but got " + str(actual))

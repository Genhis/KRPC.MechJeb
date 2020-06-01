import inspect

class Test

class AssertionException(Exception):
	def __init__(self, message):
		self.message = message

def _getException(name, expected, actual):
	return AssertionException(name + " " + inspect.caller()[2].function + "() failed: Expected " + expected + " but got " + actual)

def assertEquals(name, expected, actual):
	if expected != actual:
		raise _getException(name, expected, actual)
	
def assertTrue(name, actual):
	assertEquals(name, True, actual)
	
def assertFalse(name, actual):
	assertEquals(name, False, actual)

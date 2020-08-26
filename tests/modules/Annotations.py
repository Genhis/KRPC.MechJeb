import functools
from enum import Enum, auto

class GeneratedTestType(Enum):
	NORMAL = auto()
	READ_ONLY = auto()
	MISSING = auto()
	NOT_RUN = auto()

__beforeClassAttr = "annotations-beforeclass"
__generatedTestAttr = "annotations-generated-test"
__parameterizedTestAttr = "annotations-parameterized-test"
__testAttr = "annotations-test"

def BeforeClass(func):
	@functools.wraps(func)
	def wrapper(*args, **kwargs):
		return func(*args, **kwargs)

	setattr(wrapper, __beforeClassAttr, True)
	return wrapper

def Test(func):
	@functools.wraps(func)
	def wrapper(*args, **kwargs):
		return func(*args, **kwargs)

	setattr(wrapper, __testAttr, True)
	return wrapper

def GeneratedTest(type):
	def decorator(func):
		@functools.wraps(func)
		@Test
		def wrapper(*args, **kwargs):
			return func(*args, **kwargs)

		setattr(wrapper, __generatedTestAttr, type)
		return wrapper
	return decorator
GeneratedTest.getType = lambda method: getattr(method, __generatedTestAttr)

def ParameterizedTest(*parameters):
	"""
	Accepts lists of parameters or lambda-based generators to create tuple pairs using itertools.product()
	"""

	def decorator(func):
		@functools.wraps(func)
		@Test
		def wrapper(*args, **kwargs):
			return func(*args, **kwargs)

		setattr(wrapper, __parameterizedTestAttr, parameters)
		return wrapper
	return decorator
ParameterizedTest.getParameters = lambda method: getattr(method, __parameterizedTestAttr)

__annotations = {
	BeforeClass: __beforeClassAttr,
	GeneratedTest: __generatedTestAttr,
	ParameterizedTest: __parameterizedTestAttr,
	Test: __testAttr
}
def hasAnnotation(method, annotation):
	return hasattr(method, __annotations[annotation])

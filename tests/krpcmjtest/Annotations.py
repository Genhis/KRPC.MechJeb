import functools
from enum import Enum

class InputType(Enum):
	NONE = 0
	BOOLEAN = 1
	FLOAT = 2
	INTEGER = 3

__beforeClassAttr = "annotations-beforeclass"
__testAttr = "annotations-test"

def BeforeClass(func):
	@functools.wraps(func)
	def wrappedBeforeClass(*args, **kwargs):
		return func(*args, **kwargs)

	setattr(wrappedBeforeClass, __beforeClassAttr, True)
	return wrappedBeforeClass

def Test(type):
	def decoratorTest(func):
		@functools.wraps(func)
		def wrappedTest(*args, **kwargs):
			return func(*args, **kwargs)

		setattr(wrappedTest, __testAttr, type)
		return wrappedTest
	return decoratorTest

def getTestType(method):
	return getattr(method, __testAttr)

__annotations = {
	BeforeClass: __beforeClassAttr,
	Test: __testAttr
}
def hasAnnotation(method, annotation):
	return hasattr(method, __annotations[annotation])

__inputType = {
	bool: InputType.BOOLEAN,
	float: InputType.FLOAT,
	int: InputType.INTEGER
}
def toInputType(type):
	return __inputType[type] if type in __inputType else InputType.NONE
import functools
from enum import Enum

class InputType(Enum):
	NONE = 0
	BOOLEAN = 1
	FLOAT = 2
	INTEGER = 3
	READ_ONLY = 4
	MISSING = 5

__beforeClassAttr = "annotations-beforeclass"
__generatedAttr = "annotations-generated"
__testAttr = "annotations-test"

def BeforeClass(func):
	@functools.wraps(func)
	def wrappedBeforeClass(*args, **kwargs):
		return func(*args, **kwargs)

	setattr(wrappedBeforeClass, __beforeClassAttr, True)
	return wrappedBeforeClass

def Generated(func):
	@functools.wraps(func)
	def wrappedGenerated(*args, **kwargs):
		return func(*args, **kwargs)

	setattr(wrappedGenerated, __generatedAttr, True)
	return wrappedGenerated

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
	Generated: __generatedAttr,
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

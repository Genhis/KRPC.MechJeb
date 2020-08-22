import inspect

from .Annotations import InputType, Test, hasAnnotation, toInputType
from .TestCase import TestCase, generateTest
from .Util import toSnakeCase

class ComputerModuleTest(TestCase):
	def __init__(self, type):
		super().__init__(type)

	def setInstance(self, spacecenter, mechjeb):
		self.sc = spacecenter
		self.instance = getattr(mechjeb, toSnakeCase(self.type))

		# Create default testing methods if they don't exist
		overriddenTests = list(name for name, method in inspect.getmembers(self, inspect.ismethod) if not name.startswith("_") and hasAnnotation(method, Test))
		# TODO: Check for missing tests
		for name, attribute in inspect.getmembers(self.instance, lambda o: not inspect.ismethod(o)):
			if not name.startswith("_") and name not in overriddenTests:
				t = toInputType(type(attribute))
				if t != InputType.NONE:
					setattr(self, name, generateTest(t, name).__get__(self, self.__class__))

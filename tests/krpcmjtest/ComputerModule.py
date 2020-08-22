import inspect

from .Annotations import InputType, Test, hasAnnotation, toInputType
from .TestCase import TestCase

class ModuleTest(TestCase):
	def __init__(self, type):
		super().__init__(type)

class ComputerModuleTest(ModuleTest):
	def __init__(self, type):
		super().__init__(type)

class AutopilotModuleTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

class DisplayModuleTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

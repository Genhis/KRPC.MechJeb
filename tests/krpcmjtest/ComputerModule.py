import inspect

from .Annotations import InputType, Test
from .TestCase import TestCase

class ModuleTest(TestCase):
	def __init__(self, variable, name):
		super().__init__(variable, name)

class ComputerModuleTest(ModuleTest):
	def __init__(self, variable, name):
		super().__init__(variable, name)

class AutopilotModuleTest(ComputerModuleTest):
	def __init__(self, variable, name):
		super().__init__(variable, name)

class DisplayModuleTest(ComputerModuleTest):
	def __init__(self, variable, name):
		super().__init__(variable, name)

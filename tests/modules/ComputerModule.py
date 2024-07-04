from .TestCase import TestCase

class ModuleTest(TestCase):
	def __init__(self, variable, name, className = None):
		super().__init__(variable, name, className)

class ComputerModuleTest(ModuleTest):
	def __init__(self, variable, name, className = None):
		super().__init__(variable, name, className)

class AutopilotModuleTest(ComputerModuleTest):
	def __init__(self, variable, name, className = None):
		super().__init__(variable, name, className)

class DisplayModuleTest(ComputerModuleTest):
	def __init__(self, variable, name, className = None):
		super().__init__(variable, name, className)

from ..Annotations import InputType, Test
from ..TestCase import TestCase
from .TimeSelector import TimeSelectorTest

class OperationTest(TestCase):
	def __init__(self, variable, name):
		super().__init__(variable, name)

class TimedOperationTest(OperationTest):
	def __init__(self, variable, name):
		super().__init__(variable, name)
		self.submodules = [
			TimeSelectorTest()
		]

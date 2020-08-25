from ..Annotations import InputType, Test
from .Operation import TimedOperationTest

class OperationCircularizeTest(TimedOperationTest):
	def __init__(self):
		super().__init__("operation_circularize", "OperationCircularize")

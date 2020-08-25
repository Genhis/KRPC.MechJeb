from ..Annotations import InputType, Test
from .Operation import TimedOperationTest

class OperationLambertTest(TimedOperationTest):
	def __init__(self):
		super().__init__("operation_lambert", "OperationLambert")

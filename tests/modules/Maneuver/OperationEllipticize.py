from ..Annotations import InputType, Test
from .Operation import TimedOperationTest

class OperationEllipticizeTest(TimedOperationTest):
	def __init__(self):
		super().__init__("operation_ellipticize", "OperationEllipticize")

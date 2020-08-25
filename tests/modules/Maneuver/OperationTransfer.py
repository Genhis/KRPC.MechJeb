from ..Annotations import InputType, Test
from .Operation import TimedOperationTest

class OperationTransferTest(TimedOperationTest):
	def __init__(self):
		super().__init__("operation_transfer", "OperationTransfer")

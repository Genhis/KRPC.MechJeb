from ..Annotations import ParameterizedTest
from ..TestCase import TestCase

class TimeSelectorTest(TestCase):
	def __init__(self):
		super().__init__("time_selector", "TimeSelector")
		self.validTimeReferences = []

	@ParameterizedTest(lambda self: self.validTimeReferences)
	def time_reference_valid(self, timeReference):
		timeReference = getattr(self.mechJeb.TimeReference, timeReference)
		self.instance.time_reference = timeReference
		self.assertEquals(timeReference, self.instance.time_reference)

	@ParameterizedTest(lambda self: list(value for value in dir(self.mechJeb.TimeReference) if not value.startswith("_") and value not in self.validTimeReferences))
	def time_reference_invalid(self, timeReference):
		try:
			self.instance.time_reference = getattr(self.mechJeb.TimeReference, timeReference)
			self.assertFail("Expected OperationException for values (" + timeReference + ")")
		except self.mechJeb.OperationException as ex:
			if not str(ex).startswith("This TimeReference is not allowed: "):
				raise ex

from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class AirplaneAutopilotTest(ComputerModuleTest):
	def __init__(self, type):
		super().__init__(type)

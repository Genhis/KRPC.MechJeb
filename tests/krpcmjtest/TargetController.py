from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class TargetControllerTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("target_controller", "TargetController")

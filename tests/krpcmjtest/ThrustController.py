from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class ThrustControllerTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("thrust_controller", "ThrustController")

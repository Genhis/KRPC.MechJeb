from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class RcsControllerTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("rcs_controller", "RcsController")

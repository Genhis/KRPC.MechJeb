from .Annotations import InputType, Test
from .ComputerModule import ComputerModuleTest

class StagingControllerTest(ComputerModuleTest):
	def __init__(self):
		super().__init__("staging_controller", "StagingController")

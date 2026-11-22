from .ComputerModule import ComputerModuleTest

class DeployableControllerTest(ComputerModuleTest):
	def __init__(self, variable, name):
		super().__init__(variable, name, "DeployableController")

class AntennaControllerTest(DeployableControllerTest):
	def __init__(self):
		super().__init__("antenna_controller", "AntennaController")

class SolarPanelControllerTest(DeployableControllerTest):
	def __init__(self):
		super().__init__("solar_panel_controller", "SolarPanelController")

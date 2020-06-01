from . import Assert

class ComputerModule:
	def __init__(self, type, instance):
		self.type = type
		self.instance = instance

	def testEnabled(self):
		self.instance.enabled = True
		Assert.assertTrue(self.type, self.instance.enabled)

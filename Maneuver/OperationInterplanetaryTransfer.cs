using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationInterplanetaryTransfer : Operation {
		private readonly FieldInfo waitForPhaseAngle;

		public OperationInterplanetaryTransfer() : base("OperationInterplanetaryTransfer") {
			this.waitForPhaseAngle = this.type.GetCheckedField("waitForPhaseAngle", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		[KRPCProperty]
		public bool WaitForPhaseAngle {
			get => (bool)this.waitForPhaseAngle.GetValue(this.instance);
			set => this.waitForPhaseAngle.SetValue(this.instance, value);
		}
	}
}

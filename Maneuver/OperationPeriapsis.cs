using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationPeriapsis : TimedOperation {
		private readonly object newPeA;

		public OperationPeriapsis() : base("OperationPeriapsis") {
			this.newPeA = this.type.GetCheckedField("newPeA").GetValue(this.instance);
		}

		[KRPCProperty]
		public double NewPeriapsis {
			get => EditableVariables.GetDouble(this.newPeA);
			set => EditableVariables.SetDouble(this.newPeA, value);
		}
	}
}

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationEllipticize : TimedOperation {
		private readonly object newApA;
		private readonly object newPeA;

		public OperationEllipticize() : base("OperationEllipticize") {
			this.newApA = this.type.GetCheckedField("newApA").GetValue(this.instance);
			this.newPeA = this.type.GetCheckedField("newPeA").GetValue(this.instance);
		}

		[KRPCProperty]
		public double NewApoapsis {
			get => EditableVariables.GetDouble(this.newApA);
			set => EditableVariables.SetDouble(this.newApA, value);
		}

		[KRPCProperty]
		public double NewPeriapsis {
			get => EditableVariables.GetDouble(this.newPeA);
			set => EditableVariables.SetDouble(this.newPeA, value);
		}
	}
}

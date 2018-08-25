using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationApoapsis : TimedOperation {
		private readonly object newApA;

		public OperationApoapsis() : base("OperationApoapsis") {
			this.newApA = this.type.GetField("newApA").GetValue(this.instance);
		}

		[KRPCProperty]
		public double NewApoapsis {
			get => EditableVariables.GetDouble(this.newApA);
			set => EditableVariables.SetDouble(this.newApA, value);
		}
	}
}

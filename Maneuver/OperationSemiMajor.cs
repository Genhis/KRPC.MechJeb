using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationSemiMajor : TimedOperation {
		private readonly object newSMA;

		public OperationSemiMajor() : base("OperationSemiMajor") {
			this.newSMA = this.type.GetField("newSMA").GetValue(this.instance);
		}

		[KRPCProperty]
		public double NewSemiMajorAxis {
			get => EditableVariables.GetDouble(this.newSMA);
			set => EditableVariables.SetDouble(this.newSMA, value);
		}
	}
}

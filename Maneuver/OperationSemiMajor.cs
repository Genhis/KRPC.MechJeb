using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationSemiMajor : TimedOperation {
		private readonly object newSMA;

		public OperationSemiMajor() : base("OperationSemiMajor") {
			this.newSMA = this.type.GetCheckedField("newSMA").GetValue(this.instance);
		}

		[KRPCProperty]
		public double NewSemiMajorAxis {
			get => EditableVariables.GetDouble(this.newSMA);
			set => EditableVariables.SetDouble(this.newSMA, value);
		}
	}
}

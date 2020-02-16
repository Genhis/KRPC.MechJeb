using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationInclination : TimedOperation {
		private readonly object newInc;

		public OperationInclination() : base("OperationInclination") {
			this.newInc = this.type.GetCheckedField("newInc").GetValue(this.instance);
		}

		[KRPCProperty]
		public double NewInclination {
			get => EditableVariables.GetDouble(this.newInc);
			set => EditableVariables.SetDouble(this.newInc, value);
		}
	}
}

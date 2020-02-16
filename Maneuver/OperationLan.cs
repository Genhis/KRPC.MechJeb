using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	/**
	 * <summary>Change longitude of ascending node</summary>
	 */
	[KRPCClass(Service = "MechJeb")]
	public class OperationLan : TimedOperation {
		private readonly object newLAN;

		public OperationLan() : base("OperationLan") {
			this.newLAN = this.type.GetCheckedField("newLAN").GetValue(this.instance);
		}

		[KRPCProperty]
		public double NewLAN {
			get => EditableVariables.GetDouble(this.newLAN);
			set => EditableVariables.SetDouble(this.newLAN, value);
		}
	}
}

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	/**
	 * <summary>Change surface longitude of apsis</summary>
	 */
	[KRPCClass(Service = "MechJeb")]
	public class OperationLongitude : TimedOperation {
		private readonly object newLAN;

		public OperationLongitude() : base("OperationLongitude") {
			this.newLAN = this.type.GetCheckedField("newLAN").GetValue(this.instance);
		}

		[KRPCProperty]
		public double NewSurfaceLongitude {
			get => EditableVariables.GetDouble(this.newLAN);
			set => EditableVariables.SetDouble(this.newLAN, value);
		}
	}
}

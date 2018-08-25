using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationMoonReturn : Operation {
		private readonly object moonReturnAltitude;

		public OperationMoonReturn() : base("OperationMoonReturn") {
			this.moonReturnAltitude = this.type.GetField("moonReturnAltitude").GetValue(this.instance);
		}

		[KRPCProperty]
		public double MoonReturnAltitude {
			get => EditableVariables.GetDouble(this.moonReturnAltitude);
			set => EditableVariables.SetDouble(this.moonReturnAltitude, value);
		}
	}
}

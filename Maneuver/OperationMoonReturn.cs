using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationMoonReturn : Operation {
		private readonly object moonReturnAltitude;

		public OperationMoonReturn() : base("OperationMoonReturn") {
			this.moonReturnAltitude = this.type.GetCheckedField("moonReturnAltitude").GetValue(this.instance);
		}

		/// <summary>
		/// Approximate return altitude from a moon (from an orbiting body to the parent body).
		/// </summary>
		[KRPCProperty]
		public double MoonReturnAltitude {
			get => EditableVariables.GetDouble(this.moonReturnAltitude);
			set => EditableVariables.SetDouble(this.moonReturnAltitude, value);
		}
	}
}

using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	/// <summary>
	/// Resonant orbit is useful for placing satellites to a constellation. This mode should be used starting from a orbit in the desired orbital plane. Important parameter to this mode is the desired orbital ratio, which is the ratio between period of your current orbit and the new orbit.
	/// To deploy satellites, set the denominator to number of satellites you want to have in the constellation. Setting the nominator to one less than denominator is the most efficient, but not necessary the fastest. To successfully deploy all satellites, make sure the numbers are incommensurable.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class OperationResonantOrbit : TimedOperation {
		private readonly object resonanceNumerator;
		private readonly object resonanceDenominator;

		public OperationResonantOrbit() : base("OperationResonantOrbit") {
			this.resonanceNumerator = this.type.GetField("resonanceNumerator").GetValue(this.instance);
			this.resonanceDenominator = this.type.GetField("resonanceDenominator").GetValue(this.instance);
		}

		[KRPCProperty]
		public int ResonanceNumerator {
			get => EditableVariables.GetInt(this.resonanceNumerator);
			set => EditableVariables.SetInt(this.resonanceNumerator, value);
		}

		[KRPCProperty]
		public int ResonanceDenominator {
			get => EditableVariables.GetInt(this.resonanceDenominator);
			set => EditableVariables.SetInt(this.resonanceDenominator, value);
		}
	}
}

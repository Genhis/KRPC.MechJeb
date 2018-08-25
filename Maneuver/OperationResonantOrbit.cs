using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
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

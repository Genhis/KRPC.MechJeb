using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class AscentGT : AscentBase {
		private readonly object turnStartAltitude;
		private readonly object turnStartVelocity;
		private readonly object turnStartPitch;
		private readonly object intermediateAltitude;
		private readonly object holdAPTime;

		public AscentGT() : base("AscentGT") {
			this.turnStartAltitude = this.type.GetField("turnStartAltitude").GetValue(this.instance);
			this.turnStartVelocity = this.type.GetField("turnStartVelocity").GetValue(this.instance);
			this.turnStartPitch = this.type.GetField("turnStartPitch").GetValue(this.instance);
			this.intermediateAltitude = this.type.GetField("intermediateAltitude").GetValue(this.instance);
			this.holdAPTime = this.type.GetField("holdAPTime").GetValue(this.instance);
		}

		[KRPCProperty]
		public double TurnStartAltitude {
			get => EditableVariables.GetDouble(this.turnStartAltitude);
			set => EditableVariables.SetDouble(this.turnStartAltitude, value);
		}

		[KRPCProperty]
		public double TurnStartVelocity {
			get => EditableVariables.GetDouble(this.turnStartVelocity);
			set => EditableVariables.SetDouble(this.turnStartVelocity, value);
		}

		[KRPCProperty]
		public double TurnStartPitch {
			get => EditableVariables.GetDouble(this.turnStartPitch);
			set => EditableVariables.SetDouble(this.turnStartPitch, value);
		}

		[KRPCProperty]
		public double IntermediateAltitude {
			get => EditableVariables.GetDouble(this.intermediateAltitude);
			set => EditableVariables.SetDouble(this.intermediateAltitude, value);
		}

		[KRPCProperty]
		public double HoldAPTime {
			get => EditableVariables.GetDouble(this.holdAPTime);
			set => EditableVariables.SetDouble(this.holdAPTime, value);
		}
	}
}

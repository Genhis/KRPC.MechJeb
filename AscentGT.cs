using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// This profile is similar to the gravity turn mod. It is a 3-burn to orbit style of launch that can get to orbit with about 2800 dV on stock Kerbin.
	/// If you want to have fun make a rocket that is basically a nose cone, a jumbo-64 a mainsail and some fairly big fins, have the pitch program flip it over aggressively (uncheck the AoA limiter, set the values to like 0.5 / 50 / 40 / 45 / 1) and let it rip.
	/// </summary>
	/// <remarks>
	/// It's not precisely the GT mod algorithm and it does not do any pitch-up during the intermediate burn right now, so it won't handle low TWR upper stages.
	/// </remarks>
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

		/// <summary>
		/// Altitude in km to pitch over and initiate the Gravity Turn (higher values for lower-TWR rockets).
		/// </summary>
		[KRPCProperty]
		public double TurnStartAltitude {
			get => EditableVariables.GetDouble(this.turnStartAltitude);
			set => EditableVariables.SetDouble(this.turnStartAltitude, value);
		}

		/// <summary>
		/// Velocity in m/s which triggers pitch over and initiates the Gravity Turn (higher values for lower-TWR rockets).
		/// </summary>
		[KRPCProperty]
		public double TurnStartVelocity {
			get => EditableVariables.GetDouble(this.turnStartVelocity);
			set => EditableVariables.SetDouble(this.turnStartVelocity, value);
		}

		/// <summary>
		/// Pitch that the pitch program immediately applies.
		/// </summary>
		[KRPCProperty]
		public double TurnStartPitch {
			get => EditableVariables.GetDouble(this.turnStartPitch);
			set => EditableVariables.SetDouble(this.turnStartPitch, value);
		}

		/// <summary>
		/// Intermediate apoapsis altitude to coast to and then raise the apoapsis up to the eventual final target. May be set to equal the final target in order to skip the intermediate phase.
		/// </summary>
		[KRPCProperty]
		public double IntermediateAltitude {
			get => EditableVariables.GetDouble(this.intermediateAltitude);
			set => EditableVariables.SetDouble(this.intermediateAltitude, value);
		}

		/// <summary>
		/// At the intermediate altitude with this much time-to-apoapsis left the engine will start burning prograde to lift the apoapsis.
		/// The engine will throttle down in order to burn closer to the apoapsis.
		/// This is very similar to the lead-time of a maneuver node in concept, but with throttling down in the case where the player has initiated the burn too early (the corollary is that if you see lots of throttling down at the start, you likely need less HoldAP time).
		/// </summary>
		[KRPCProperty]
		public double HoldAPTime {
			get => EditableVariables.GetDouble(this.holdAPTime);
			set => EditableVariables.SetDouble(this.holdAPTime, value);
		}
	}
}

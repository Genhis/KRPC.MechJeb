using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// The Primer Vector Guidance (RSS/RO) profile.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class AscentPVG : AscentBase {
		private readonly object pitchStartVelocity;
		private readonly object pitchRate;
		private readonly object desiredApoapsis;
		private readonly FieldInfo omitCoast;

		public AscentPVG() : base("AscentPVG") {
			this.pitchStartVelocity = this.type.GetField("pitchStartVelocity").GetValue(this.instance);
			this.pitchRate = this.type.GetField("pitchRate").GetValue(this.instance);
			this.desiredApoapsis = this.type.GetField("desiredApoapsis").GetValue(this.instance);
			this.omitCoast = this.type.GetField("omitCoast");
		}

		[KRPCProperty]
		public double PitchStartVelocity {
			get => EditableVariables.GetDouble(this.pitchStartVelocity);
			set => EditableVariables.SetDouble(this.pitchStartVelocity, value);
		}

		[KRPCProperty]
		public double PitchRate {
			get => EditableVariables.GetDouble(this.pitchRate);
			set => EditableVariables.SetDouble(this.pitchRate, value);
		}

		/// <summary>
		/// The target apoapsis in meters.
		/// </summary>
		[KRPCProperty]
		public double DesiredApoapsis {
			get => EditableVariables.GetDouble(this.desiredApoapsis);
			set => EditableVariables.SetDouble(this.desiredApoapsis, value);
		}

		/// <summary>
		/// The terminal guidance period in seconds.
		/// </summary>
		[KRPCProperty]
		public bool OmitCoast {
			get => (bool)this.omitCoast.GetValue(this.instance);
			set => this.omitCoast.SetValue(this.instance, value);
		}
	}
}

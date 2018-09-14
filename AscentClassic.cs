using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// The Classic Ascent Profile.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class AscentClassic : AscentBase {
		private readonly object turnStartAltitude;
		private readonly object turnStartVelocity;
		private readonly object turnEndAltitude;
		private readonly object turnEndAngle;
		private readonly object turnShapeExponent;
		private readonly FieldInfo autoPath;
		private readonly FieldInfo autoPathPerc;
		private readonly FieldInfo autoPathSpdFactor;
		private readonly PropertyInfo autoTurnStartAltitude;
		private readonly PropertyInfo autoTurnStartVelocity;
		private readonly PropertyInfo autoTurnEndAltitude;

		public AscentClassic() : base("AscentClassic") {
			this.turnStartAltitude = this.type.GetField("turnStartAltitude").GetValue(this.instance);
			this.turnStartVelocity = this.type.GetField("turnStartVelocity").GetValue(this.instance);
			this.turnEndAltitude = this.type.GetField("turnEndAltitude").GetValue(this.instance);
			this.turnEndAngle = this.type.GetField("turnEndAngle").GetValue(this.instance);
			this.turnShapeExponent = this.type.GetField("turnShapeExponent").GetValue(this.instance);
			this.autoPath = this.type.GetField("autoPath");
			this.autoPathPerc = this.type.GetField("autoPathPerc");
			this.autoPathSpdFactor = this.type.GetField("autoPathSpdFactor");
			this.autoTurnStartAltitude = this.type.GetProperty("autoTurnStartAltitude");
			this.autoTurnStartVelocity = this.type.GetProperty("autoTurnStartVelocity");
			this.autoTurnEndAltitude = this.type.GetProperty("autoTurnEndAltitude");
		}

		/// <summary>
		/// The turn starts when this altitude is reached.
		/// </summary>
		[KRPCProperty]
		public double TurnStartAltitude {
			get => EditableVariables.GetDouble(this.turnStartAltitude);
			set => EditableVariables.SetDouble(this.turnStartAltitude, value);
		}

		/// <summary>
		/// The turn starts when this velocity is reached.
		/// </summary>
		[KRPCProperty]
		public double TurnStartVelocity {
			get => EditableVariables.GetDouble(this.turnStartVelocity);
			set => EditableVariables.SetDouble(this.turnStartVelocity, value);
		}

		/// <summary>
		/// The turn ends when this altitude is reached.
		/// </summary>
		[KRPCProperty]
		public double TurnEndAltitude {
			get => EditableVariables.GetDouble(this.turnEndAltitude);
			set => EditableVariables.SetDouble(this.turnEndAltitude, value);
		}

		/// <summary>
		/// The final flight path angle.
		/// </summary>
		[KRPCProperty]
		public double TurnEndAngle {
			get => EditableVariables.GetDouble(this.turnEndAngle);
			set => EditableVariables.SetDouble(this.turnEndAngle, value);
		}

		/// <summary>
		/// A value between 0 - 1 describing how steep the turn is.
		/// </summary>
		[KRPCProperty]
		public double TurnShapeExponent {
			get => EditableVariables.GetDouble(this.turnShapeExponent);
			set => EditableVariables.SetDouble(this.turnShapeExponent, value);
		}

		/// <summary>
		/// Whether to enable automatic altitude turn.
		/// </summary>
		[KRPCProperty]
		public bool AutoPath {
			get => (bool)this.autoPath.GetValue(this.instance);
			set => this.autoPath.SetValue(this.instance, value);
		}

		/// <summary>
		/// A value between 0 and 1.
		/// </summary>
		[KRPCProperty]
		public float AutoPathPerc {
			get => (float)this.autoPathPerc.GetValue(this.instance);
			set => this.autoPathPerc.SetValue(this.instance, value);
		}

		/// <summary>
		/// A value between 0 and 1.
		/// </summary>
		[KRPCProperty]
		public float AutoPathSpeedFactor {
			get => (float)this.autoPathSpdFactor.GetValue(this.instance);
			set => this.autoPathSpdFactor.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double AutoTurnStartAltitude => EditableVariables.GetDouble(this.autoTurnStartAltitude);

		[KRPCProperty]
		public double AutoTurnStartVelocity => EditableVariables.GetDouble(this.autoTurnStartVelocity);

		[KRPCProperty]
		public double AutoTurnEndAltitude => EditableVariables.GetDouble(this.autoTurnEndAltitude);
	}
}

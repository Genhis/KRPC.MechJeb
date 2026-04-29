using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// The Classic Ascent Profile.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class AscentClassic : AscentBase {
		// In MechJeb 2.15.x, MechJebModuleAscentClassic was removed.
		// Settings now live in MechJebModuleAscentSettings (initialized by AscentAutopilot.InitType).

		// Fields and methods (looked up against MechJebModuleAscentSettings)
		private static FieldInfo turnStartAltitudeField;
		private static FieldInfo turnStartVelocityField;
		private static FieldInfo turnEndAltitudeField;
		private static FieldInfo turnEndAngleField;
		private static FieldInfo turnShapeExponentField;
		private static FieldInfo autoPath;
		private static FieldInfo autoTurnPerc;
		private static FieldInfo autoTurnSpdFactor;

		// Instance objects
		private object turnStartAltitude;
		private object turnStartVelocity;
		private object turnEndAltitude;
		private object turnEndAngle;
		private object turnShapeExponent;

		internal static new void InitType(Type type) {
			// type = MuMech.MechJebModuleAscentSettings (called by AscentAutopilot.InitType)
			turnStartAltitudeField = type.GetField("TurnStartAltitude");
			turnStartVelocityField = type.GetField("TurnStartVelocity");
			turnEndAltitudeField = type.GetField("TurnEndAltitude");
			turnEndAngleField = type.GetField("TurnEndAngle");
			turnShapeExponentField = type.GetField("TurnShapeExponent");
			autoPath = type.GetField("AutoPath");
			autoTurnPerc = type.GetField("AutoTurnPerc");
			autoTurnSpdFactor = type.GetField("AutoTurnSpdFactor");
		}

		protected internal override void InitInstance(object instance) {
			base.InitInstance(instance);

			this.turnStartAltitude = turnStartAltitudeField.GetInstanceValue(instance);
			this.turnStartVelocity = turnStartVelocityField.GetInstanceValue(instance);
			this.turnEndAltitude = turnEndAltitudeField.GetInstanceValue(instance);
			this.turnEndAngle = turnEndAngleField.GetInstanceValue(instance);
			this.turnShapeExponent = turnShapeExponentField.GetInstanceValue(instance);
		}

		/// <summary>
		/// The turn starts when this altitude is reached.
		/// </summary>
		[KRPCProperty]
		public double TurnStartAltitude {
			get => EditableDouble.Get(this.turnStartAltitude);
			set => EditableDouble.Set(this.turnStartAltitude, value);
		}

		/// <summary>
		/// The turn starts when this velocity is reached.
		/// </summary>
		[KRPCProperty]
		public double TurnStartVelocity {
			get => EditableDouble.Get(this.turnStartVelocity);
			set => EditableDouble.Set(this.turnStartVelocity, value);
		}

		/// <summary>
		/// The turn ends when this altitude is reached.
		/// </summary>
		[KRPCProperty]
		public double TurnEndAltitude {
			get => EditableDouble.Get(this.turnEndAltitude);
			set => EditableDouble.Set(this.turnEndAltitude, value);
		}

		/// <summary>
		/// The final flight path angle.
		/// </summary>
		[KRPCProperty]
		public double TurnEndAngle {
			get => EditableDouble.Get(this.turnEndAngle);
			set => EditableDouble.Set(this.turnEndAngle, value);
		}

		/// <summary>
		/// A value between 0 - 1 describing how steep the turn is.
		/// </summary>
		[KRPCProperty]
		public double TurnShapeExponent {
			get => EditableDouble.Get(this.turnShapeExponent);
			set => EditableDouble.Set(this.turnShapeExponent, value);
		}

		/// <summary>
		/// Whether to enable automatic altitude turn.
		/// </summary>
		[KRPCProperty]
		public bool AutoPath {
			get => (bool)autoPath.GetValue(this.instance);
			set => autoPath.SetValue(this.instance, value);
		}

		/// <summary>
		/// A value between 0 and 1.
		/// </summary>
		[KRPCProperty]
		public float AutoTurnPercent {
			get => (float)autoTurnPerc.GetValue(this.instance);
			set => autoTurnPerc.SetValue(this.instance, value);
		}

		/// <summary>
		/// A value between 0 and 1.
		/// </summary>
		[KRPCProperty]
		public float AutoTurnSpeedFactor {
			get => (float)autoTurnSpdFactor.GetValue(this.instance);
			set => autoTurnSpdFactor.SetValue(this.instance, value);
		}

		// AutoTurnStartAltitude, AutoTurnStartVelocity, AutoTurnEndAltitude were computed properties
		// in MechJebModuleAscentClassic; they no longer exist in MechJeb 2.15.x AscentSettings.
	}
}

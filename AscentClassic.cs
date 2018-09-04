using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
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
		public double TurnEndAltitude {
			get => EditableVariables.GetDouble(this.turnEndAltitude);
			set => EditableVariables.SetDouble(this.turnEndAltitude, value);
		}

		[KRPCProperty]
		public double TurnEndAngle {
			get => EditableVariables.GetDouble(this.turnEndAngle);
			set => EditableVariables.SetDouble(this.turnEndAngle, value);
		}

		[KRPCProperty]
		public double TurnShapeExponent {
			get => EditableVariables.GetDouble(this.turnShapeExponent);
			set => EditableVariables.SetDouble(this.turnShapeExponent, value);
		}

		[KRPCProperty]
		public bool AutoPath {
			get => (bool)this.autoPath.GetValue(this.instance);
			set => this.autoPath.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public float AutoPathPerc {
			get => (float)this.autoPathPerc.GetValue(this.instance);
			set => this.autoPathPerc.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public float AutoPathSpeedFactor {
			get => (float)this.autoPathSpdFactor.GetValue(this.instance);
			set => this.autoPathSpdFactor.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double AutoTurnStartAltitude {
			get => EditableVariables.GetDouble(this.autoTurnStartAltitude);
			set => EditableVariables.SetDouble(this.autoTurnStartAltitude, value);
		}

		[KRPCProperty]
		public double AutoTurnStartVelocity {
			get => EditableVariables.GetDouble(this.autoTurnStartVelocity);
			set => EditableVariables.SetDouble(this.autoTurnStartVelocity, value);
		}

		[KRPCProperty]
		public double AutoTurnEndAltitude {
			get => EditableVariables.GetDouble(this.autoTurnEndAltitude);
			set => EditableVariables.SetDouble(this.autoTurnEndAltitude, value);
		}

		[KRPCMethod]
		public override void DisableAll() {
			this.AutoPath = false;
		}
	}
}

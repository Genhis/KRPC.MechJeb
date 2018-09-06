using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class AirplaneAutopilot : KRPCComputerModule {
		private readonly FieldInfo headingHoldEnabled;
		private readonly FieldInfo altitudeHoldEnabled;
		private readonly FieldInfo vertSpeedHoldEnabled;
		private readonly FieldInfo rollHoldEnabled;
		private readonly FieldInfo speedHoldEnabled;

		private readonly FieldInfo altitudeTarget;
		private readonly FieldInfo headingTarget;
		private readonly FieldInfo rollTarget;
		private readonly FieldInfo speedTarget;
		private readonly FieldInfo vertSpeedTarget;
		private readonly FieldInfo vertSpeedMax;
		private readonly FieldInfo rollMax;

		private readonly object accKp;
		private readonly object accKi;
		private readonly object accKd;

		private readonly object verKp;
		private readonly object verKi;
		private readonly object verKd;

		private readonly object rolKp;
		private readonly object rolKi;
		private readonly object rolKd;

		private readonly object yawKp;
		private readonly object yawKi;
		private readonly object yawKd;

		private readonly object yawLimit;

		public AirplaneAutopilot() : base("AirplaneAutopilot") {
			this.headingHoldEnabled = this.type.GetField("HeadingHoldEnabled");
			this.altitudeHoldEnabled = this.type.GetField("AltitudeHoldEnabled");
			this.vertSpeedHoldEnabled = this.type.GetField("VertSpeedHoldEnabled");
			this.rollHoldEnabled = this.type.GetField("RollHoldEnabled");
			this.speedHoldEnabled = this.type.GetField("SpeedHoldEnabled");

			this.altitudeTarget = this.type.GetField("AltitudeTarget");
			this.headingTarget = this.type.GetField("HeadingTarget");
			this.rollTarget = this.type.GetField("RollTarget");
			this.speedTarget = this.type.GetField("SpeedTarget");
			this.vertSpeedTarget = this.type.GetField("VertSpeedTarget");
			this.vertSpeedMax = this.type.GetField("VertSpeedMax");
			this.rollMax = this.type.GetField("RollMax");

			this.accKp = this.type.GetField("AccKp").GetValue(this.instance);
			this.accKi = this.type.GetField("AccKi").GetValue(this.instance);
			this.accKd = this.type.GetField("AccKd").GetValue(this.instance);

			this.verKp = this.type.GetField("VerKp").GetValue(this.instance);
			this.verKi = this.type.GetField("VerKi").GetValue(this.instance);
			this.verKd = this.type.GetField("VerKd").GetValue(this.instance);

			this.rolKp = this.type.GetField("RolKp").GetValue(this.instance);
			this.rolKi = this.type.GetField("RolKi").GetValue(this.instance);
			this.rolKd = this.type.GetField("RolKd").GetValue(this.instance);

			this.yawKp = this.type.GetField("YawKp").GetValue(this.instance);
			this.yawKi = this.type.GetField("YawKi").GetValue(this.instance);
			this.yawKd = this.type.GetField("YawKd").GetValue(this.instance);

			this.yawLimit = this.type.GetField("YawLimit").GetValue(this.instance);
		}

		[KRPCProperty]
		public bool HeadingHoldEnabled {
			get => (bool)this.headingHoldEnabled.GetValue(this.instance);
			set => this.headingHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool AltitudeHoldEnabled {
			get => (bool)this.altitudeHoldEnabled.GetValue(this.instance);
			set => this.altitudeHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool VertSpeedHoldEnabled {
			get => (bool)this.vertSpeedHoldEnabled.GetValue(this.instance);
			set => this.vertSpeedHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool RollHoldEnabled {
			get => (bool)this.rollHoldEnabled.GetValue(this.instance);
			set => this.rollHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool SpeedHoldEnabled {
			get => (bool)this.speedHoldEnabled.GetValue(this.instance);
			set => this.speedHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double AltitudeTarget {
			get => (double)this.altitudeTarget.GetValue(this.instance);
			set => this.altitudeTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double HeadingTarget {
			get => (double)this.headingTarget.GetValue(this.instance);
			set => this.headingTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double RollTarget {
			get => (double)this.rollTarget.GetValue(this.instance);
			set => this.rollTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double SpeedTarget {
			get => (double)this.speedTarget.GetValue(this.instance);
			set => this.speedTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double VertSpeedTarget {
			get => (double)this.vertSpeedTarget.GetValue(this.instance);
			set => this.vertSpeedTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double VertSpeedMax {
			get => (double)this.vertSpeedMax.GetValue(this.instance);
			set => this.vertSpeedMax.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double RollMax {
			get => (double)this.rollMax.GetValue(this.instance);
			set => this.rollMax.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double AccKp {
			get => EditableVariables.GetDouble(this.accKp);
			set => EditableVariables.SetDouble(this.accKp, value);
		}

		[KRPCProperty]
		public double AccKi {
			get => EditableVariables.GetDouble(this.accKi);
			set => EditableVariables.SetDouble(this.accKi, value);
		}

		[KRPCProperty]
		public double AccKd {
			get => EditableVariables.GetDouble(this.accKd);
			set => EditableVariables.SetDouble(this.accKd, value);
		}

		[KRPCProperty]
		public double VerKp {
			get => EditableVariables.GetDouble(this.verKp);
			set => EditableVariables.SetDouble(this.verKp, value);
		}

		[KRPCProperty]
		public double VerKi {
			get => EditableVariables.GetDouble(this.verKi);
			set => EditableVariables.SetDouble(this.verKi, value);
		}

		[KRPCProperty]
		public double VerKd {
			get => EditableVariables.GetDouble(this.verKd);
			set => EditableVariables.SetDouble(this.verKd, value);
		}

		[KRPCProperty]
		public double RolKp {
			get => EditableVariables.GetDouble(this.rolKp);
			set => EditableVariables.SetDouble(this.rolKp, value);
		}

		[KRPCProperty]
		public double RolKi {
			get => EditableVariables.GetDouble(this.rolKi);
			set => EditableVariables.SetDouble(this.rolKi, value);
		}

		[KRPCProperty]
		public double RolKd {
			get => EditableVariables.GetDouble(this.rolKd);
			set => EditableVariables.SetDouble(this.rolKd, value);
		}

		[KRPCProperty]
		public double YawKp {
			get => EditableVariables.GetDouble(this.yawKp);
			set => EditableVariables.SetDouble(this.yawKp, value);
		}

		[KRPCProperty]
		public double YawKi {
			get => EditableVariables.GetDouble(this.yawKi);
			set => EditableVariables.SetDouble(this.yawKi, value);
		}

		[KRPCProperty]
		public double YawKd {
			get => EditableVariables.GetDouble(this.yawKd);
			set => EditableVariables.SetDouble(this.yawKd, value);
		}

		[KRPCProperty]
		public double YawLimit {
			get => EditableVariables.GetDouble(this.yawLimit);
			set => EditableVariables.SetDouble(this.yawLimit, value);
		}
	}
}

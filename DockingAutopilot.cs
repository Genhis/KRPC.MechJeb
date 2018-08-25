using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class DockingAutopilot : ComputerModule {
		private readonly FieldInfo status;
		private readonly object speedLimit;
		private readonly FieldInfo forceRoll;
		private readonly object roll;
		private readonly FieldInfo overrideSafeDistance;
		private readonly object overridenSafeDistance;
		private readonly FieldInfo overrideTargetSize;
		private readonly object overridenTargetSize;

		private readonly FieldInfo safeDistance;
		private readonly FieldInfo targetSize;

		public DockingAutopilot() : base("DockingAutopilot") {
			this.status = this.type.GetField("status");
			this.speedLimit = this.type.GetField("speedLimit").GetValue(this.instance);
			this.forceRoll = this.type.GetField("forceRol");
			this.roll = this.type.GetField("rol").GetValue(this.instance);
			this.overrideSafeDistance = this.type.GetField("overrideSafeDistance");
			this.overridenSafeDistance = this.type.GetField("overridenSafeDistance").GetValue(this.instance);
			this.overrideTargetSize = this.type.GetField("overrideTargetSize");
			this.overridenTargetSize = this.type.GetField("overridenTargetSize").GetValue(this.instance);
			this.safeDistance = this.type.GetField("safeDistance");
			this.targetSize = this.type.GetField("targetSize");
		}

		[KRPCProperty]
		public string Status => this.status.GetValue(this.instance).ToString();

		[KRPCProperty]
		public double SpeedLimit {
			get => EditableVariables.GetDouble(this.speedLimit);
			set => EditableVariables.SetDouble(this.speedLimit, value);
		}

		[KRPCProperty]
		public bool ForceRoll {
			get => (bool)this.forceRoll.GetValue(this.instance);
			set => this.forceRoll.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double Roll {
			get => EditableVariables.GetDouble(this.roll);
			set => EditableVariables.SetDouble(this.roll, value);
		}

		[KRPCProperty]
		public bool OverrideSafeDistance {
			get => (bool)this.overrideSafeDistance.GetValue(this.instance);
			set => this.overrideSafeDistance.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double OverridenSafeDistance {
			get => EditableVariables.GetDouble(this.overridenSafeDistance);
			set => EditableVariables.SetDouble(this.overridenSafeDistance, value);
		}

		[KRPCProperty]
		public bool OverrideTargetSize {
			get => (bool)this.overrideTargetSize.GetValue(this.instance);
			set => this.overrideTargetSize.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double OverridenTargetSize {
			get => EditableVariables.GetDouble(this.overridenTargetSize);
			set => EditableVariables.SetDouble(this.overridenTargetSize, value);
		}

		[KRPCProperty]
		public float SafeDistance => (float)this.safeDistance.GetValue(this.instance);

		[KRPCProperty]
		public float TargetSize => (float)this.targetSize.GetValue(this.instance);
	}
}

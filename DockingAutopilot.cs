using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class DockingAutopilot : KRPCComputerModule {
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
			this.status = this.type.GetCheckedField("status");
			this.speedLimit = this.type.GetCheckedField("speedLimit").GetValue(this.instance);
			this.forceRoll = this.type.GetCheckedField("forceRol");
			this.roll = this.type.GetCheckedField("rol").GetValue(this.instance);
			this.overrideSafeDistance = this.type.GetCheckedField("overrideSafeDistance");
			this.overridenSafeDistance = this.type.GetCheckedField("overridenSafeDistance").GetValue(this.instance);
			this.overrideTargetSize = this.type.GetCheckedField("overrideTargetSize");
			this.overridenTargetSize = this.type.GetCheckedField("overridenTargetSize").GetValue(this.instance);
			this.safeDistance = this.type.GetCheckedField("safeDistance");
			this.targetSize = this.type.GetCheckedField("targetSize");
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

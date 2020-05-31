using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {

	[KRPCClass(Service = "MechJeb")]
	public class AirplaneAutopilot : KRPCComputerModule {
		internal new const string MechJebType = "MuMech.MechJebModuleAirplaneAutopilot";

		// Fields and methods
		private static FieldInfo headingHoldEnabled;

		private static FieldInfo altitudeHoldEnabled;
		private static FieldInfo vertSpeedHoldEnabled;
		private static FieldInfo rollHoldEnabled;
		private static FieldInfo speedHoldEnabled;

		private static FieldInfo altitudeTarget;
		private static FieldInfo headingTarget;
		private static FieldInfo rollTarget;
		private static FieldInfo speedTarget;
		private static FieldInfo vertSpeedTarget;
		private static FieldInfo vertSpeedMax;
		private static FieldInfo rollMax;

		private static FieldInfo accKpField;
		private static FieldInfo accKiField;
		private static FieldInfo accKdField;

		private static FieldInfo verKpField;
		private static FieldInfo verKiField;
		private static FieldInfo verKdField;

		private static FieldInfo rolKpField;
		private static FieldInfo rolKiField;
		private static FieldInfo rolKdField;

		private static FieldInfo yawKpField;
		private static FieldInfo yawKiField;
		private static FieldInfo yawKdField;

		private static FieldInfo yawLimitField;

		// Instance objects
		private object accKp;

		private object accKi;
		private object accKd;

		private object verKp;
		private object verKi;
		private object verKd;

		private object rolKp;
		private object rolKi;
		private object rolKd;

		private object yawKp;
		private object yawKi;
		private object yawKd;

		private object yawLimit;

		internal static new void InitType(Type type) {
			headingHoldEnabled = type.GetCheckedField("HeadingHoldEnabled");
			altitudeHoldEnabled = type.GetCheckedField("AltitudeHoldEnabled");
			vertSpeedHoldEnabled = type.GetCheckedField("VertSpeedHoldEnabled");
			rollHoldEnabled = type.GetCheckedField("RollHoldEnabled");
			speedHoldEnabled = type.GetCheckedField("SpeedHoldEnabled");

			altitudeTarget = type.GetCheckedField("AltitudeTarget");
			headingTarget = type.GetCheckedField("HeadingTarget");
			rollTarget = type.GetCheckedField("RollTarget");
			speedTarget = type.GetCheckedField("SpeedTarget");
			vertSpeedTarget = type.GetCheckedField("VertSpeedTarget");
			vertSpeedMax = type.GetCheckedField("VertSpeedMax");
			rollMax = type.GetCheckedField("RollMax");

			accKpField = type.GetCheckedField("AccKp");
			accKiField = type.GetCheckedField("AccKi");
			accKdField = type.GetCheckedField("AccKd");

			verKpField = type.GetCheckedField("VerKp");
			verKiField = type.GetCheckedField("VerKi");
			verKdField = type.GetCheckedField("VerKd");

			rolKpField = type.GetCheckedField("RolKp");
			rolKiField = type.GetCheckedField("RolKi");
			rolKdField = type.GetCheckedField("RolKd");

			yawKpField = type.GetCheckedField("YawKp");
			yawKiField = type.GetCheckedField("YawKi");
			yawKdField = type.GetCheckedField("YawKd");

			yawLimitField = type.GetCheckedField("YawLimit");
		}

		protected internal override void InitInstance(object instance, object guiInstance) {
			base.InitInstance(instance, guiInstance);

			this.accKp = accKpField.GetInstanceValue(instance);
			this.accKi = accKiField.GetInstanceValue(instance);
			this.accKd = accKdField.GetInstanceValue(instance);

			this.verKp = verKpField.GetInstanceValue(instance);
			this.verKi = verKiField.GetInstanceValue(instance);
			this.verKd = verKdField.GetInstanceValue(instance);

			this.rolKp = rolKpField.GetInstanceValue(instance);
			this.rolKi = rolKiField.GetInstanceValue(instance);
			this.rolKd = rolKdField.GetInstanceValue(instance);

			this.yawKp = yawKpField.GetInstanceValue(instance);
			this.yawKi = yawKiField.GetInstanceValue(instance);
			this.yawKd = yawKdField.GetInstanceValue(instance);

			this.yawLimit = yawLimitField.GetInstanceValue(instance);
		}

		/// <summary>
		/// The visibility of the GUI window
		/// </summary>
		[KRPCProperty]
		public override bool Visible {
			get => base.Visible;
			set => base.Visible = value;
		}

		[KRPCProperty]
		public bool HeadingHoldEnabled {
			get => (bool)headingHoldEnabled.GetValue(this.instance);
			set => headingHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool AltitudeHoldEnabled {
			get => (bool)altitudeHoldEnabled.GetValue(this.instance);
			set => altitudeHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool VertSpeedHoldEnabled {
			get => (bool)vertSpeedHoldEnabled.GetValue(this.instance);
			set => vertSpeedHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool RollHoldEnabled {
			get => (bool)rollHoldEnabled.GetValue(this.instance);
			set => rollHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool SpeedHoldEnabled {
			get => (bool)speedHoldEnabled.GetValue(this.instance);
			set => speedHoldEnabled.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double AltitudeTarget {
			get => (double)altitudeTarget.GetValue(this.instance);
			set => altitudeTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double HeadingTarget {
			get => (double)headingTarget.GetValue(this.instance);
			set => headingTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double RollTarget {
			get => (double)rollTarget.GetValue(this.instance);
			set => rollTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double SpeedTarget {
			get => (double)speedTarget.GetValue(this.instance);
			set => speedTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double VertSpeedTarget {
			get => (double)vertSpeedTarget.GetValue(this.instance);
			set => vertSpeedTarget.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double VertSpeedMax {
			get => (double)vertSpeedMax.GetValue(this.instance);
			set => vertSpeedMax.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double RollMax {
			get => (double)rollMax.GetValue(this.instance);
			set => rollMax.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double AccKp {
			get => EditableDouble.Get(this.accKp);
			set => EditableDouble.Set(this.accKp, value);
		}

		[KRPCProperty]
		public double AccKi {
			get => EditableDouble.Get(this.accKi);
			set => EditableDouble.Set(this.accKi, value);
		}

		[KRPCProperty]
		public double AccKd {
			get => EditableDouble.Get(this.accKd);
			set => EditableDouble.Set(this.accKd, value);
		}

		[KRPCProperty]
		public double VerKp {
			get => EditableDouble.Get(this.verKp);
			set => EditableDouble.Set(this.verKp, value);
		}

		[KRPCProperty]
		public double VerKi {
			get => EditableDouble.Get(this.verKi);
			set => EditableDouble.Set(this.verKi, value);
		}

		[KRPCProperty]
		public double VerKd {
			get => EditableDouble.Get(this.verKd);
			set => EditableDouble.Set(this.verKd, value);
		}

		[KRPCProperty]
		public double RolKp {
			get => EditableDouble.Get(this.rolKp);
			set => EditableDouble.Set(this.rolKp, value);
		}

		[KRPCProperty]
		public double RolKi {
			get => EditableDouble.Get(this.rolKi);
			set => EditableDouble.Set(this.rolKi, value);
		}

		[KRPCProperty]
		public double RolKd {
			get => EditableDouble.Get(this.rolKd);
			set => EditableDouble.Set(this.rolKd, value);
		}

		[KRPCProperty]
		public double YawKp {
			get => EditableDouble.Get(this.yawKp);
			set => EditableDouble.Set(this.yawKp, value);
		}

		[KRPCProperty]
		public double YawKi {
			get => EditableDouble.Get(this.yawKi);
			set => EditableDouble.Set(this.yawKi, value);
		}

		[KRPCProperty]
		public double YawKd {
			get => EditableDouble.Get(this.yawKd);
			set => EditableDouble.Set(this.yawKd, value);
		}

		[KRPCProperty]
		public double YawLimit {
			get => EditableDouble.Get(this.yawLimit);
			set => EditableDouble.Set(this.yawLimit, value);
		}
	}
}

using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// The Primer Vector Guidance (RSS/RO) profile.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class AscentPVG : AscentBase {
		internal new const string MechJebType = "MuMech.MechJebModuleAscentPVG";

		// Fields and methods
		private static FieldInfo pitchStartVelocityField;
		private static FieldInfo pitchRateField;
		private static FieldInfo desiredApoapsisField;
		private static FieldInfo omitCoast;

		// Instance objects
		private object pitchStartVelocity;
		private object pitchRate;
		private object desiredApoapsis;

		internal static new void InitType(Type type) {
			pitchStartVelocityField = type.GetCheckedField("pitchStartVelocity");
			pitchRateField = type.GetCheckedField("pitchRate");
			desiredApoapsisField = type.GetCheckedField("desiredApoapsis");
			omitCoast = type.GetCheckedField("omitCoast");
		}

		protected internal override void InitInstance(object instance) {
			base.InitInstance(instance);

			this.pitchStartVelocity = pitchStartVelocityField.GetInstanceValue(instance);
			this.pitchRate = pitchRateField.GetInstanceValue(instance);
			this.desiredApoapsis = desiredApoapsisField.GetInstanceValue(instance);
		}

		[KRPCProperty]
		public double PitchStartVelocity {
			get => EditableDouble.Get(this.pitchStartVelocity);
			set => EditableDouble.Set(this.pitchStartVelocity, value);
		}

		[KRPCProperty]
		public double PitchRate {
			get => EditableDouble.Get(this.pitchRate);
			set => EditableDouble.Set(this.pitchRate, value);
		}

		/// <summary>
		/// The target apoapsis in meters.
		/// </summary>
		[KRPCProperty]
		public double DesiredApoapsis {
			get => EditableDouble.Get(this.desiredApoapsis);
			set => EditableDouble.Set(this.desiredApoapsis, value);
		}

		[KRPCProperty]
		public bool OmitCoast {
			get => (bool)omitCoast.GetValue(this.instance);
			set => omitCoast.SetValue(this.instance, value);
		}
	}
}

using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCEnum(Service = "MechJeb")]
	public enum TimeReference {
		Computed, XFromNow, Apoapsis, Periapsis, Altitude, EqAscending, EqDescending,
		RelAscending, RelDescending, ClosestApproach,
		EqHighestAd, EqNearestAd, RelHighestAd, RelNearestAd
	}

	[KRPCClass(Service = "MechJeb")]
	public class TimeSelector {
		internal const string MechJebType = "MuMech.TimeSelector";

		// Fields and methods
		private static FieldInfo allowedTimeRefField;
		private static FieldInfo currentTimeRef;
		private static FieldInfo leadTimeField;
		private static FieldInfo circularizeAltitudeField;

		// Instance objects
		internal object instance;

		private int[] allowedTimeRef; //MuMech.TimeReference enum
		private object leadTime;
		private object circularizeAltitude;

		internal static void InitType(Type type) {
			allowedTimeRefField = type.GetCheckedField("allowedTimeRef", BindingFlags.NonPublic | BindingFlags.Instance);
			currentTimeRef = type.GetCheckedField("currentTimeRef", BindingFlags.NonPublic | BindingFlags.Instance);
			leadTimeField = type.GetCheckedField("leadTime");
			circularizeAltitudeField = type.GetCheckedField("circularizeAltitude");
		}

		protected internal void InitInstance(object instance) {
			this.instance = instance;

			this.allowedTimeRef = (int[])allowedTimeRefField.GetInstanceValue(instance);
			this.leadTime = leadTimeField.GetInstanceValue(instance);
			this.circularizeAltitude = circularizeAltitudeField.GetInstanceValue(instance);
		}

		[KRPCProperty]
		public TimeReference TimeReference {
			get => (TimeReference)this.allowedTimeRef[(int)currentTimeRef.GetValue(this.instance)];
			set => currentTimeRef.SetValue(this.instance, this.GetTimeRefIndex(value));
		}

		private int GetTimeRefIndex(TimeReference timeRef) {
			for(int i = 0; i < this.allowedTimeRef.Length; i++)
				if(this.allowedTimeRef[i] == (int)timeRef)
					return i;
			throw new OperationException("This TimeReference is not allowed: " + timeRef);
		}

		/// <summary>
		/// To be used with <see cref="TimeReference.XFromNow" />.
		/// </summary>
		[KRPCProperty]
		public double LeadTime {
			get => EditableDouble.Get(this.leadTime);
			set => EditableDouble.Set(this.leadTime, value);
		}

		/// <summary>
		/// To be used with <see cref="TimeReference.Altitude" />.
		/// </summary>
		[KRPCProperty]
		public double CircularizeAltitude {
			get => EditableDouble.Get(this.circularizeAltitude);
			set => EditableDouble.Set(this.circularizeAltitude, value);
		}
	}
}

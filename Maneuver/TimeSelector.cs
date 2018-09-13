using System;
using System.Reflection;

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
		private static FieldInfo allowedTimeRefField;
		private static FieldInfo currentTimeRef;
		private static FieldInfo leadTimeField;
		private static FieldInfo circularizeAltitudeField;

		internal readonly object instance;
		private readonly int[] allowedTimeRef; //MuMech.TimeReference enum
		private readonly object leadTime;
		private readonly object circularizeAltitude;

		public TimeSelector(object instance) {
			this.instance = instance;
			this.allowedTimeRef = (int[])allowedTimeRefField.GetValue(instance);
			this.leadTime = leadTimeField.GetValue(instance);
			this.circularizeAltitude = circularizeAltitudeField.GetValue(instance);
		}

		internal static bool InitTypes(Type t) {
			switch(t.FullName) {
				case "MuMech.TimeSelector":
					allowedTimeRefField = t.GetField("allowedTimeRef", BindingFlags.NonPublic | BindingFlags.Instance);
					currentTimeRef = t.GetField("currentTimeRef", BindingFlags.NonPublic | BindingFlags.Instance);
					leadTimeField = t.GetField("leadTime");
					circularizeAltitudeField = t.GetField("circularizeAltitude");
					return true;
				default:
					return false;
			}
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
			get => EditableVariables.GetDouble(this.leadTime);
			set => EditableVariables.SetDouble(this.leadTime, value);
		}

		/// <summary>
		/// To be used with <see cref="TimeReference.Altitude" />.
		/// </summary>
		[KRPCProperty]
		public double CircularizeAltitude {
			get => EditableVariables.GetDouble(this.circularizeAltitude);
			set => EditableVariables.SetDouble(this.circularizeAltitude, value);
		}
	}
}

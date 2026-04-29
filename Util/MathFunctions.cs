using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;

namespace KRPC.MechJeb.Util {
	// MuMech.LaunchTiming was removed in MechJeb 2.15.x.
	// LaunchToRendezvous in AscentAutopilot will throw NotSupportedException if called.
	internal static class LaunchTiming {
		public static double TimeToPhaseAngle(double launchPhaseAngle) {
			throw new NotSupportedException("LaunchTiming.TimeToPhaseAngle is not available in MechJeb 2.15.x");
		}
	}

	internal static class MathFunctions {
		// Renamed from MechJebLib.Maths.Functions to MechJebLib.Functions.Astro in MechJeb 2.15.x
		internal const string MechJebType = "MechJebLib.Functions.Astro";

		// Fields and methods
		private static MethodInfo timeToPlane;

		internal static void InitType(Type type) {
			timeToPlane = type.GetCheckedMethod("TimeToPlane");
		}

		public static Tuple<double, double> MinimumTimeToPlane(double lan, double inclination) {
			double normal = TimeToPlane(lan, inclination);
			double inverted = TimeToPlane(lan, -inclination);
			return normal < inverted ? Tuple.Create(normal, inclination) : Tuple.Create(inverted, -inclination);
		}

		public static double TimeToPlane(double lan, double inclination) {
			return (double)timeToPlane.Invoke(null, new object[] { FlightGlobals.ActiveVessel.mainBody.rotationPeriod, MechJeb.vesselState.Latitude, MechJeb.vesselState.CelestialLongitude, lan, inclination });
		}
	}
}

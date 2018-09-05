using System;
using System.Reflection;

namespace KRPC.MechJeb.ExtensionMethods {
	public static class VesselExtensions {
		private static MethodInfo getMasterMechJeb;
		private static MethodInfo placeManeuverNode;
		private static MethodInfo removeAllManeuverNodes;

		public static bool InitTypes(Type t) {
			switch(t.FullName) {
				case "MuMech.VesselExtensions":
					getMasterMechJeb = t.GetMethod("GetMasterMechJeb", BindingFlags.Public | BindingFlags.Static);
					placeManeuverNode = t.GetMethod("PlaceManeuverNode", BindingFlags.Public | BindingFlags.Static);
					removeAllManeuverNodes = t.GetMethod("RemoveAllManeuverNodes", BindingFlags.Public | BindingFlags.Static);
					return true;
				default:
					return false;
			}
		}

		public static PartModule GetMasterMechJeb(this Vessel vessel) {
			return (PartModule)getMasterMechJeb.Invoke(null, new object[] { vessel });
		}

		public static ManeuverNode PlaceManeuverNode(this Vessel vessel, Orbit patch, Vector3d dV, double UT) {
			return (ManeuverNode)placeManeuverNode.Invoke(null, new object[] { vessel, patch, dV, UT });
		}

		public static void RemoveAllManeuverNodes(this Vessel vessel) {
			removeAllManeuverNodes.Invoke(null, new object[] { vessel });
		}
	}
}

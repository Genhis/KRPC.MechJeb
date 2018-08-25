using System;

using UnityEngine;

namespace KRPC.MechJeb {
	public static class Logger {
		public static void Info(string message) {
			Debug.Log("[KRPC.MechJeb] " + message);
		}

		public static void Warning(string message) {
			Debug.LogWarning("[KRPC.MechJeb] " + message);
		}

		public static void Warning(string message, Exception ex) {
			Warning(message);
			Debug.LogException(ex);
		}

		public static void Severe(string message) {
			Debug.LogError("[KRPC.MechJeb] " + message);
		}

		public static void Severe(string message, Exception ex) {
			Severe(message);
			Debug.LogException(ex);
		}
	}
}

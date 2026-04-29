using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// Stock-style GravityTurn profile.
	/// </summary>
	/// <remarks>
	/// This profile was removed in MechJeb 2.15.x. Properties will not be functional.
	/// </remarks>
	[KRPCClass(Service = "MechJeb")]
	public class AscentGT : AscentBase {
		// MuMech.MechJebModuleAscentGT was removed in MechJeb 2.15.x.
		// No MechJebType constant — AscentAutopilot.InitType handles initialization.

		internal static new void InitType(Type type) {
			// AscentGT no longer exists in MechJeb 2.15.x; no fields to initialize.
		}
	}
}

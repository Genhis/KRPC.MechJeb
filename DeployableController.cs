using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class DeployableController : ComputerModule {
		internal new const string MechJebType = "MuMech.MechJebModuleDeployableController";

		// Fields and methods
		private static FieldInfo autoDeploy;

		private static MethodInfo extendAll;
		private static MethodInfo retractAll;
		private static MethodInfo allRetracted;

		internal static new void InitType(Type type) {
			autoDeploy = type.GetCheckedField("autoDeploy");

			extendAll = type.GetCheckedMethod("ExtendAll");
			retractAll = type.GetCheckedMethod("RetractAll");
			allRetracted = type.GetCheckedMethod("AllRetracted");
		}

		[KRPCProperty]
		public bool AutoDeploy {
			get => (bool)autoDeploy.GetValue(this.instance);
			set => autoDeploy.SetValue(this.instance, value);
		}

		[KRPCMethod]
		public void ExtendAll() {
			extendAll.Invoke(this.instance, null);
		}

		[KRPCMethod]
		public void RetractAll() {
			retractAll.Invoke(this.instance, null);
		}

		[KRPCMethod]
		public bool AllRetracted() {
			return (bool)allRetracted.Invoke(this.instance, null);
		}
	}
}

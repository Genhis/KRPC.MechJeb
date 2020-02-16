using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class DeployableController : ComputerModule {
		private readonly FieldInfo autoDeploy;

		private readonly MethodInfo extendAll;
		private readonly MethodInfo retractAll;
		private readonly MethodInfo allRetracted;

		public DeployableController(string moduleType) : base(moduleType) {
			this.autoDeploy = this.type.GetCheckedField("autoDeploy");

			this.extendAll = this.type.GetCheckedMethod("ExtendAll");
			this.retractAll = this.type.GetCheckedMethod("RetractAll");
			this.allRetracted = this.type.GetCheckedMethod("AllRetracted");
		}

		[KRPCProperty]
		public bool AutoDeploy {
			get => (bool)this.autoDeploy.GetValue(this.instance);
			set => this.autoDeploy.SetValue(this.instance, value);
		}

		[KRPCMethod]
		public void ExtendAll() {
			this.extendAll.Invoke(this.instance, null);
		}

		[KRPCMethod]
		public void RetractAll() {
			this.retractAll.Invoke(this.instance, null);
		}

		[KRPCMethod]
		public bool AllRetracted() {
			return (bool)this.allRetracted.Invoke(this.instance, null);
		}
	}
}

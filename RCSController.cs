using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class RCSController : ComputerModule {
		private readonly FieldInfo rcsThrottle;
		private readonly FieldInfo rcsForRotation;

		public RCSController() : base("RCSController") {
			this.rcsThrottle = this.type.GetCheckedField("rcsThrottle");
			this.rcsForRotation = this.type.GetCheckedField("rcsForRotation");
		}

		[KRPCProperty]
		public bool RCSThrottle {
			get => (bool)this.rcsThrottle.GetValue(this.instance);
			set => this.rcsThrottle.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool RCSForRotation {
			get => (bool)this.rcsForRotation.GetValue(this.instance);
			set => this.rcsForRotation.SetValue(this.instance, value);
		}
	}
}

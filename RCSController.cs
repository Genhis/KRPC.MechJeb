using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class RCSController : ComputerModule {
		private readonly FieldInfo rcsThrottle;
		private readonly FieldInfo rcsForRotation;

		public RCSController() : base("RCSController") {
			this.rcsThrottle = this.type.GetField("rcsThrottle");
			this.rcsForRotation = this.type.GetField("rcsForRotation");
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

		[KRPCMethod]
		public override void DisableAll() {
			this.RCSForRotation = false;
			this.RCSThrottle = false;
		}
	}
}

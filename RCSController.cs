using System.Reflection;

using KRPC.Service.Attributes;
using KRPC.SpaceCenter.ExtensionMethods;

using Tuple3d = KRPC.Utils.Tuple<double, double, double>;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class RCSController : KRPCComputerModule {
		private readonly FieldInfo rcsThrottle;
		private readonly FieldInfo rcsForRotation;

		private readonly MethodInfo setTargetRelative;
		private readonly MethodInfo setTargetWorldVelocity;

		public RCSController() : base("RCSController") {
			this.rcsThrottle = this.type.GetField("rcsThrottle");
			this.rcsForRotation = this.type.GetField("rcsForRotation");

			this.setTargetRelative = this.type.GetMethod("SetTargetRelative");
			this.setTargetWorldVelocity = this.type.GetMethod("SetTargetWorldVelocity");
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
		public void SetTargetRelative(Tuple3d velocity) {
			this.setTargetRelative.Invoke(this.instance, new object[] { velocity.ToVector() });
		}

		[KRPCMethod]
		public void SetTargetWorldVelocity(Tuple3d velocity) {
			this.setTargetWorldVelocity.Invoke(this.instance, new object[] { velocity.ToVector() });
		}
	}
}

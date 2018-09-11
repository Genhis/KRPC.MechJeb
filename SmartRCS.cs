using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class SmartRCS : DisplayModule {
		private readonly FieldInfo target;
		private readonly FieldInfo autoDisableSmartRCS;

		private readonly MethodInfo engage;

		public SmartRCS() : base("SmartRcs") {
			this.target = this.type.GetField("target");
			this.autoDisableSmartRCS = this.type.GetField("autoDisableSmartRCS");

			this.engage = this.type.GetMethod("Engage");
		}

		[KRPCProperty]
		public bool AutoDisableSmartRCS {
			get => (bool)this.autoDisableSmartRCS.GetValue(this.instance);
			set => this.autoDisableSmartRCS.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public SmartRCSMode Mode {
			get => (SmartRCSMode)this.target.GetValue(this.instance);
			set {
				this.target.SetValue(this.instance, value);
				this.engage.Invoke(this.instance, null);
			}
		}

		[KRPCProperty]
		public RCSController RCSController => MechJeb.RCSController;
	}

	[KRPCEnum(Service = "MechJeb")]
	public enum SmartRCSMode {
		Off,
		ZeroRelativeVelocity,
		ZeroVelocity
	}
}

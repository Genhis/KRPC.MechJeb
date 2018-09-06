using System;
using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	public abstract class ComputerModule : SimpleModule {
		private static PropertyInfo enabled;
		private static FieldInfo usersField;

		private static MethodInfo usersAdd;
		private static MethodInfo usersRemove;

		private readonly object users;

		public ComputerModule(string moduleType) : base(moduleType) {
			this.users = usersField.GetValue(this.instance);
		}

		internal static bool InitTypes(Type t) {
			switch(t.FullName) {
				case "MuMech.AutopilotModule":
					AutopilotModule.status = t.GetProperty("status");
					return true;
				case "MuMech.ComputerModule":
					enabled = t.GetProperty("enabled");
					usersField = t.GetField("users");
					return true;
				case "MuMech.UserPool":
					usersAdd = t.GetMethod("Add");
					usersRemove = t.GetMethod("Remove");
					return true;
				default:
					return false;
			}
		}

		public virtual bool Enabled {
			get => (bool)enabled.GetValue(this.instance, null);
			set {
				if(value)
					usersAdd.Invoke(this.users, new object[] { this });
				else
					usersRemove.Invoke(this.users, new object[] { this });
			}
		}
	}

	public abstract class KRPCComputerModule : ComputerModule {
		public KRPCComputerModule(string moduleType) : base(moduleType) { }

		[KRPCProperty]
		public override bool Enabled {
			get => base.Enabled;
			set => base.Enabled = value;
		}
	}

	public abstract class AutopilotModule : KRPCComputerModule {
		internal static PropertyInfo status;

		public AutopilotModule(string moduleType) : base(moduleType) { }

		[KRPCProperty]
		public string Status => (string)status.GetValue(this.instance, null);
	}
}

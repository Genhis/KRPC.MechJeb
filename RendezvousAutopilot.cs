using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class RendezvousAutopilot : KRPCComputerModule {
		internal new const string MechJebType = "MuMech.MechJebModuleRendezvousAutopilot";

		// Fields and methods
		private static FieldInfo desiredDistanceField;
		private static FieldInfo maxPhasingOrbitsField;
		private static FieldInfo status;

		// Instance objects
		private object desiredDistance;
		private object maxPhasingOrbits;

		internal static new void InitType(Type type) {
			desiredDistanceField = type.GetCheckedField("desiredDistance");
			maxPhasingOrbitsField = type.GetCheckedField("maxPhasingOrbits");
			status = type.GetCheckedField("status");
		}

		protected internal override void InitInstance(object instance, object guiInstance) {
			base.InitInstance(instance, guiInstance);

			this.desiredDistance = desiredDistanceField.GetInstanceValue(instance);
			this.maxPhasingOrbits = maxPhasingOrbitsField.GetInstanceValue(instance);
		}

		/// <summary>
		/// The visibility of the GUI window
		/// </summary>
		[KRPCProperty]
		public override bool Visible {
			get => base.Visible;
			set => base.Visible = value;
		}

		[KRPCProperty]
		public double DesiredDistance {
			get => EditableDouble.Get(this.desiredDistance);
			set => EditableDouble.Set(this.desiredDistance, value);
		}

		[KRPCProperty]
		public double MaxPhasingOrbits {
			get => EditableDouble.Get(this.maxPhasingOrbits);
			set => EditableDouble.Set(this.maxPhasingOrbits, value);
		}

		[KRPCProperty]
		public string Status => status.GetValue(this.instance).ToString();
	}
}

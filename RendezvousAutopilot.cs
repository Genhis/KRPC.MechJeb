using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class RendezvousAutopilot : KRPCComputerModule {
		private readonly object desiredDistance;
		private readonly object maxPhasingOrbits;
		private readonly FieldInfo status;

		public RendezvousAutopilot() : base("RendezvousAutopilot") {
			this.desiredDistance = this.type.GetField("desiredDistance").GetValue(this.instance);
			this.maxPhasingOrbits = this.type.GetField("maxPhasingOrbits").GetValue(this.instance);
			this.status = this.type.GetField("status");
		}

		[KRPCProperty]
		public double DesiredDistance {
			get => EditableVariables.GetDouble(this.desiredDistance);
			set => EditableVariables.SetDouble(this.desiredDistance, value);
		}

		[KRPCProperty]
		public double MaxPhasingOrbits {
			get => EditableVariables.GetDouble(this.maxPhasingOrbits);
			set => EditableVariables.SetDouble(this.maxPhasingOrbits, value);
		}

		[KRPCProperty]
		public string Status => this.status.GetValue(this.instance).ToString();
	}
}

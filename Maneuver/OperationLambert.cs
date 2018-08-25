using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationLambert : TimedOperation {
		private readonly object interceptInterval;

		public OperationLambert() : base("OperationLambert") {
			this.interceptInterval = this.type.GetField("interceptInterval").GetValue(this.instance);
		}

		[KRPCProperty]
		public double InterceptInterval {
			get => EditableVariables.GetDouble(this.interceptInterval);
			set => EditableVariables.SetDouble(this.interceptInterval, value);
		}
	}
}

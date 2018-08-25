using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationKillRelVel : TimedOperation {
		public OperationKillRelVel() : base("OperationKillRelVel") { }
	}
}

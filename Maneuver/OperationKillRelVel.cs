using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	/// <summary>
	/// Kill relative velocity to a given target.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class OperationKillRelVel : TimedOperation {
		public OperationKillRelVel() : base("OperationKillRelVel") { }
	}
}

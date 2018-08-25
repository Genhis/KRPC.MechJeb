using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	/**
	 * <summary>Hohmann transfer to target</summary>
	 */
	[KRPCClass(Service = "MechJeb")]
	public class OperationTransfer : Operation {
		public OperationTransfer() : base("OperationGeneric") { }
	}
}

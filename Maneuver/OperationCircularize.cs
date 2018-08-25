using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationCircularize : TimedOperation {
		public OperationCircularize() : base("OperationCircularize") {}
	}
}

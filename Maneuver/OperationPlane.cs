using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationPlane : TimedOperation {
		public OperationPlane() : base("OperationPlane") { }
	}
}

using KRPC.MechJeb.ExtensionMethods;
using KRPC.MechJeb.Maneuver;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class ManeuverPlanner {
		private Operation[] operations = new Operation[17];

		//TODO: OperationAdvancedTransfer

		[KRPCProperty]
		public OperationApoapsis OperationApoapsis => this.GetOperation<OperationApoapsis>(1);

		[KRPCProperty]
		public OperationCircularize OperationCircularize => this.GetOperation<OperationCircularize>(2);

		[KRPCProperty]
		public OperationCourseCorrection OperationCourseCorrection => this.GetOperation<OperationCourseCorrection>(3);

		[KRPCProperty]
		public OperationEllipticize OperationEllipticize => this.GetOperation<OperationEllipticize>(4);

		[KRPCProperty]
		public OperationInclination OperationInclination => this.GetOperation<OperationInclination>(5);

		[KRPCProperty]
		public OperationInterplanetaryTransfer OperationInterplanetaryTransfer => this.GetOperation<OperationInterplanetaryTransfer>(6);

		[KRPCProperty]
		public OperationKillRelVel OperationKillRelVel => this.GetOperation<OperationKillRelVel>(7);

		[KRPCProperty]
		public OperationLambert OperationLambert => this.GetOperation<OperationLambert>(8);

		[KRPCProperty]
		public OperationLan OperationLan => this.GetOperation<OperationLan>(9);

		[KRPCProperty]
		public OperationLongitude OperationLongitude => this.GetOperation<OperationLongitude>(10);

		[KRPCProperty]
		public OperationMoonReturn OperationMoonReturn => this.GetOperation<OperationMoonReturn>(11);

		[KRPCProperty]
		public OperationPeriapsis OperationPeriapsis => this.GetOperation<OperationPeriapsis>(12);

		[KRPCProperty]
		public OperationPlane OperationPlane => this.GetOperation<OperationPlane>(13);

		[KRPCProperty]
		public OperationResonantOrbit OperationResonantOrbit => this.GetOperation<OperationResonantOrbit>(14);

		[KRPCProperty]
		public OperationSemiMajor OperationSemiMajor => this.GetOperation<OperationSemiMajor>(15);

		[KRPCProperty]
		public OperationTransfer OperationTransfer => this.GetOperation<OperationTransfer>(16);

		private T GetOperation<T>(int id) where T : Operation {
			if(this.operations[id] == null)
				this.operations[id] = typeof(T).CreateInstance<T>(null);
			return (T)this.operations[id];
		}
	}
}

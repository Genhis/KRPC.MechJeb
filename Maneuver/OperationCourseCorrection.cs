using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationCourseCorrection : Operation {
		private readonly object courseCorrectFinalPeA;
		private readonly object interceptDistance;

		public OperationCourseCorrection() : base("OperationCourseCorrection") {
			this.courseCorrectFinalPeA = this.type.GetField("courseCorrectFinalPeA").GetValue(this.instance);
			this.interceptDistance = this.type.GetField("interceptDistance").GetValue(this.instance);
		}

		[KRPCProperty]
		public double CourseCorrectFinalPeA {
			get => EditableVariables.GetDouble(this.courseCorrectFinalPeA);
			set => EditableVariables.SetDouble(this.courseCorrectFinalPeA, value);
		}

		[KRPCProperty]
		public double InterceptDistance {
			get => EditableVariables.GetDouble(this.interceptDistance);
			set => EditableVariables.SetDouble(this.interceptDistance, value);
		}
	}
}

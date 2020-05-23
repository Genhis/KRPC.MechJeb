using System;
using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	[KRPCClass(Service = "MechJeb")]
	public class OperationPlane : TimedOperation {
		internal new const string MechJebType = "MuMech.OperationPlane";

		// Fields and methods
		private static FieldInfo timeSelector;

		internal static new void InitType(Type type) {
			timeSelector = GetTimeSelectorField(type);
		}

		protected internal override void InitInstance(object instance) {
			base.InitInstance(instance);
			this.InitTimeSelector(timeSelector);
		}
	}
}

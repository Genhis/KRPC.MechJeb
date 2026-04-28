using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	/// <summary>
	/// Bi-impulsive (Hohmann) transfer to target.
	///
	/// This option is used to plan transfer to target in single sphere of influence. It is suitable for rendezvous with other vessels or moons.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class OperationTransfer : TimedOperation {
		internal new const string MechJebType = "MuMech.OperationGeneric";

		// Fields and methods
		private static FieldInfo captureField;
		private static FieldInfo planCaptureField;
		private static FieldInfo rendezvousField;
		private static FieldInfo coplanarField;
		private static FieldInfo lagTimeField;
		private static FieldInfo timeSelector;

		internal static new void InitType(Type type) {
			captureField = type.GetCheckedField("Capture");
			planCaptureField = type.GetCheckedField("PlanCapture");
			rendezvousField = type.GetCheckedField("Rendezvous");
			coplanarField = type.GetCheckedField("Coplanar");
			lagTimeField = type.GetCheckedField("LagTime");
			timeSelector = GetTimeSelectorField(type);
		}

		protected internal override void InitInstance(object instance) {
			base.InitInstance(instance);
			this.InitTimeSelector(timeSelector);
		}

		/// <summary>
		/// Plan a capture burn at the destination.
		/// </summary>
		[KRPCProperty]
		public bool Capture {
			get => (bool)captureField.GetValue(this.instance);
			set => captureField.SetValue(this.instance, value);
		}

		/// <summary>
		/// Plan a capture burn (alternative mode).
		/// </summary>
		[KRPCProperty]
		public bool PlanCapture {
			get => (bool)planCaptureField.GetValue(this.instance);
			set => planCaptureField.SetValue(this.instance, value);
		}

		/// <summary>
		/// Rendezvous mode (match velocity at closest approach).
		/// </summary>
		[KRPCProperty]
		public bool Rendezvous {
			get => (bool)rendezvousField.GetValue(this.instance);
			set => rendezvousField.SetValue(this.instance, value);
		}

		/// <summary>
		/// Coplanar transfer (ignore plane change).
		/// </summary>
		[KRPCProperty]
		public bool Coplanar {
			get => (bool)coplanarField.GetValue(this.instance);
			set => coplanarField.SetValue(this.instance, value);
		}

		/// <summary>
		/// Departure lag time in seconds.
		/// </summary>
		[KRPCProperty]
		public double LagTime {
			get => EditableDouble.Get(lagTimeField, this.instance);
			set => EditableDouble.Set(lagTimeField, this.instance, value);
		}
	}
}

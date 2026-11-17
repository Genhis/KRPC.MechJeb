using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	/// <summary>
	/// Bi-impulsive (Hohmann) transfer to target.
	/// 
	/// This option is used to plan transfer to target in single sphere of influence. It is suitable for rendezvous with other vessels or moons.
	/// Contrary to the name, the transfer is often uni-impulsive. You can select when you want the manevuer to happen or select optimum time.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class OperationTransfer : TimedOperation {
		internal new const string MechJebType = "MuMech.OperationGeneric";

		// Fields and methods
		private static FieldInfo capture;
		private static FieldInfo planCapture;
		private static FieldInfo rendezvous;
		private static FieldInfo lagTimeField;
		private static FieldInfo coplanar;
		private static FieldInfo timeSelector;

		// Instance objects
		private object lagTime;

		internal static new void InitType(Type type) {
			capture = type.GetCheckedField("Capture");
			planCapture = type.GetCheckedField("PlanCapture");
			rendezvous = type.GetCheckedField("Rendezvous");
			lagTimeField = type.GetCheckedField("LagTime");
			coplanar = type.GetCheckedField("Coplanar");
			timeSelector = GetTimeSelectorField(type);
		}

		protected internal override void InitInstance(object instance) {
			base.InitInstance(instance);

			this.lagTime = lagTimeField.GetInstanceValue(instance);
			this.InitTimeSelector(timeSelector);
		}

		/// <summary>
		/// Perform capture burn
		/// </summary>
		[KRPCProperty]
		public bool Capture {
			get => (bool)capture.GetValue(this.instance);
			set => capture.SetValue(this.instance, value);
		}

		/// <summary>
		/// Plan insertion burn
		/// </summary>
		[KRPCProperty]
		public bool PlanCapture {
			get => (bool)planCapture.GetValue(this.instance);
			set => planCapture.SetValue(this.instance, value);
		}

		/// <summary>
		/// Perform rendezvous, vs simple transfer
		/// </summary>
		[KRPCProperty]
		public bool Rendezvous {
			get => (bool)rendezvous.GetValue(this.instance);
			set => rendezvous.SetValue(this.instance, value);
		}

		/// <summary>
		/// Fractional target period offset
		/// </summary>
		[KRPCProperty]
		public double LagTime {
			get => EditableDouble.Get(this.lagTime);
			set => EditableDouble.Set(this.lagTime, value);
		}

		/// <summary>
		/// Simple coplanar Hohmann transfer.
		/// Set it to true if you are used to the old version of transfer maneuver.
		/// </summary>
		/// <remarks>If set to true, TimeSelector property is ignored.</remarks>
		[KRPCProperty]
		public bool Coplanar {
			get => (bool)coplanar.GetValue(this.instance);
			set => coplanar.SetValue(this.instance, value);
		}
	}
}

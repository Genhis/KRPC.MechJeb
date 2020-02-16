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
		private readonly FieldInfo interceptOnly;
		private readonly object periodOffset;
		private readonly FieldInfo simpleTransfer;

		public OperationTransfer() : base("OperationGeneric") {
			this.interceptOnly = this.type.GetCheckedField("intercept_only");
			this.periodOffset = this.type.GetCheckedField("periodOffset").GetValue(this.instance);
			this.simpleTransfer = this.type.GetCheckedField("simpleTransfer");
		}

		/// <summary>
		/// Intercept only, no capture burn (impact/flyby)
		/// </summary>
		[KRPCProperty]
		public bool InterceptOnly {
			get => (bool)this.interceptOnly.GetValue(this.instance);
			set => this.interceptOnly.SetValue(this.instance, value);
		}

		/// <summary>
		/// Fractional target period offset
		/// </summary>
		[KRPCProperty]
		public double PeriodOffset {
			get => EditableVariables.GetDouble(this.periodOffset);
			set => EditableVariables.SetDouble(this.periodOffset, value);
		}

		/// <summary>
		/// Simple coplanar Hohmann transfer.
		/// Set it to true if you are used to the old version of transfer maneuver.
		/// </summary>
		/// <remarks>If set to true, TimeSelector property is ignored.</remarks>
		[KRPCProperty]
		public bool SimpleTransfer {
			get => (bool)this.simpleTransfer.GetValue(this.instance);
			set => this.simpleTransfer.SetValue(this.instance, value);
		}
	}
}

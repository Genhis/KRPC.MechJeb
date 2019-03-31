using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	/// <summary>
	/// Bi-impulsive (Hohmann) transfer to target
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class OperationTransfer : TimedOperation {
		private readonly FieldInfo interceptOnly;
		private readonly object periodOffset;
		private readonly FieldInfo simpleTransfer;

		public OperationTransfer() : base("OperationGeneric") {
			this.interceptOnly = this.type.GetField("intercept_only");
			this.periodOffset = this.type.GetField("periodOffset").GetValue(this.instance);
			this.simpleTransfer = this.type.GetField("simpleTransfer");
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
		/// Simple coplanar Hohmann transfer
		/// </summary>
		/// <remarks>If set to true, TimeSelector property is ignored.</remarks>
		[KRPCProperty]
		public bool SimpleTransfer {
			get => (bool)this.simpleTransfer.GetValue(this.instance);
			set => this.simpleTransfer.SetValue(this.instance, value);
		}
	}
}

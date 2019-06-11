using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class StagingController : KRPCComputerModule {
		private readonly object autostagePreDelay;
		private readonly object autostagePostDelay;
		private readonly object autostageLimit;
		private readonly object fairingMaxDynamicPressure;
		private readonly object fairingMinAltitude;
		private readonly object clampAutoStageThrustPct;
		private readonly object fairingMaxAerothermalFlux;

		private readonly FieldInfo hotStaging;
		private readonly object hotStagingLeadTime;

		private readonly FieldInfo autostagingOnce;

		public StagingController() : base("StagingController") {
			this.autostagePreDelay = this.type.GetField("autostagePreDelay").GetValue(this.instance);
			this.autostagePostDelay = this.type.GetField("autostagePostDelay").GetValue(this.instance);
			this.autostageLimit = this.type.GetField("autostageLimit").GetValue(this.instance);
			this.fairingMaxDynamicPressure = this.type.GetField("fairingMaxDynamicPressure").GetValue(this.instance);
			this.fairingMinAltitude = this.type.GetField("fairingMinAltitude").GetValue(this.instance);
			this.clampAutoStageThrustPct = this.type.GetField("clampAutoStageThrustPct").GetValue(this.instance);
			this.fairingMaxAerothermalFlux = this.type.GetField("fairingMaxAerothermalFlux").GetValue(this.instance);

			this.hotStaging = this.type.GetField("hotStaging");
			this.hotStagingLeadTime = this.type.GetField("hotStagingLeadTime").GetValue(this.instance);

			this.autostagingOnce = this.type.GetField("autostagingOnce");
		}

		/// <summary>
		/// The autopilot will pause the actual staging before ? seconds for each stage.
		/// </summary>
		[KRPCProperty]
		public double AutostagePreDelay {
			get => EditableVariables.GetDouble(this.autostagePreDelay);
			set => EditableVariables.SetDouble(this.autostagePreDelay, value);
		}

		/// <summary>
		/// The autopilot will pause the actual staging after ? seconds for each stage.
		/// </summary>
		[KRPCProperty]
		public double AutostagePostDelay {
			get => EditableVariables.GetDouble(this.autostagePostDelay);
			set => EditableVariables.SetDouble(this.autostagePostDelay, value);
		}

		/// <summary>
		/// Stop at the selected stage - staging will not occur beyond this stage number.
		/// </summary>
		[KRPCProperty]
		public int AutostageLimit {
			get => EditableVariables.GetInt(this.autostageLimit);
			set => EditableVariables.SetInt(this.autostageLimit, value);
		}

		[KRPCProperty]
		public double FairingMaxDynamicPressure {
			get => EditableVariables.GetDouble(this.fairingMaxDynamicPressure);
			set => EditableVariables.SetDouble(this.fairingMaxDynamicPressure, value);
		}

		[KRPCProperty]
		public double FairingMinAltitude {
			get => EditableVariables.GetDouble(this.fairingMinAltitude);
			set => EditableVariables.SetDouble(this.fairingMinAltitude, value);
		}

		[KRPCProperty]
		public double ClampAutoStageThrustPct {
			get => EditableVariables.GetDouble(this.clampAutoStageThrustPct);
			set => EditableVariables.SetDouble(this.clampAutoStageThrustPct, value);
		}

		[KRPCProperty]
		public double FairingMaxAerothermalFlux {
			get => EditableVariables.GetDouble(this.fairingMaxAerothermalFlux);
			set => EditableVariables.SetDouble(this.fairingMaxAerothermalFlux, value);
		}

		[KRPCProperty]
		public bool HotStaging {
			get => (bool)this.hotStaging.GetValue(this.instance);
			set => this.hotStaging.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double HotStagingLeadTime {
			get => EditableVariables.GetDouble(this.hotStagingLeadTime);
			set => EditableVariables.SetDouble(this.hotStagingLeadTime, value);
		}

		/// <summary>
		/// The autostaging mode. If set to true, it will automatically disable itself after one staging action.
		/// </summary>
		/// <remarks>The controller needs to be enabled for this to work.</remarks>
		[KRPCProperty]
		public bool AutostagingOnce {
			get => (bool)this.autostagingOnce.GetValue(this.instance);
			set => this.autostagingOnce.SetValue(this.instance, value);
		}
	}
}

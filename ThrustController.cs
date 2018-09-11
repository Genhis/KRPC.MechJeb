using System.Reflection;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class ThrustController : ComputerModule {
		private readonly FieldInfo limitDynamicPressure;
		private readonly object maxDynamicPressure;
		private readonly FieldInfo limitToPreventOverheats;
		private readonly FieldInfo smoothThrottle;
		private readonly FieldInfo throttleSmoothingTime;
		private readonly FieldInfo limitToPreventFlameout;
		//private readonly FieldInfo limitToPreventUnstableIgnition;
		//private readonly FieldInfo autoRCSUllaging;
		private readonly object flameoutSafetyPct;
		private readonly FieldInfo manageIntakes;
		private readonly FieldInfo limitAcceleration;
		private readonly object maxAcceleration;
		private readonly FieldInfo limitThrottle;
		private readonly object maxThrottle;
		private readonly FieldInfo limiterMinThrottle;
		private readonly object minThrottle;
		private readonly FieldInfo differentialThrottle;
		private readonly FieldInfo differentialThrottleSuccess;
		private readonly FieldInfo electricThrottle;
		private readonly object electricThrottleLo;
		private readonly object electricThrottleHi;

		public ThrustController() : base("ThrustController") {
			this.limitDynamicPressure = this.type.GetField("limitDynamicPressure");
			this.maxDynamicPressure = this.type.GetField("maxDynamicPressure").GetValue(this.instance);
			this.limitToPreventOverheats = this.type.GetField("limitToPreventOverheats");
			this.smoothThrottle = this.type.GetField("smoothThrottle");
			this.throttleSmoothingTime = this.type.GetField("throttleSmoothingTime");
			this.limitToPreventFlameout = this.type.GetField("limitToPreventFlameout");
			this.flameoutSafetyPct = this.type.GetField("flameoutSafetyPct").GetValue(this.instance);
			this.manageIntakes = this.type.GetField("manageIntakes");
			this.limitAcceleration = this.type.GetField("limitAcceleration");
			this.maxAcceleration = this.type.GetField("maxAcceleration").GetValue(this.instance);
			this.limitThrottle = this.type.GetField("limitThrottle");
			this.maxThrottle = this.type.GetField("maxThrottle").GetValue(this.instance);
			this.limiterMinThrottle = this.type.GetField("limiterMinThrottle");
			this.minThrottle = this.type.GetField("minThrottle").GetValue(this.instance);
			this.differentialThrottle = this.type.GetField("differentialThrottle");
			this.differentialThrottleSuccess = this.type.GetField("differentialThrottleSuccess");
			this.electricThrottle = this.type.GetField("electricThrottle");
			this.electricThrottleLo = this.type.GetField("electricThrottleLo").GetValue(this.instance);
			this.electricThrottleHi = this.type.GetField("electricThrottleHi").GetValue(this.instance);
		}

		[KRPCProperty]
		public bool LimitDynamicPressure {
			get => (bool)this.limitDynamicPressure.GetValue(this.instance);
			set => this.limitDynamicPressure.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double MaxDynamicPressure {
			get => EditableVariables.GetDouble(this.maxDynamicPressure);
			set => EditableVariables.SetDouble(this.maxDynamicPressure, value);
		}

		[KRPCProperty]
		public bool LimitToPreventOverheats {
			get => (bool)this.limitToPreventOverheats.GetValue(this.instance);
			set => this.limitToPreventOverheats.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool LimitAcceleration {
			get => (bool)this.limitAcceleration.GetValue(this.instance);
			set => this.limitAcceleration.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double MaxAcceleration {
			get => EditableVariables.GetDouble(this.maxAcceleration);
			set => EditableVariables.SetDouble(this.maxAcceleration, value);
		}

		[KRPCProperty]
		public bool LimitThrottle {
			get => (bool)this.limitThrottle.GetValue(this.instance);
			set => this.limitThrottle.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double MaxThrottle {
			get => EditableVariables.GetDouble(this.maxThrottle);
			set => EditableVariables.SetDouble(this.maxThrottle, value);
		}

		[KRPCProperty]
		public bool LimiterMinThrottle {
			get => (bool)this.limiterMinThrottle.GetValue(this.instance);
			set => this.limiterMinThrottle.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double MinThrottle {
			get => EditableVariables.GetDouble(this.minThrottle);
			set => EditableVariables.SetDouble(this.minThrottle, value);
		}

		[KRPCProperty]
		public bool SmoothThrottle {
			get => (bool)this.smoothThrottle.GetValue(this.instance);
			set => this.smoothThrottle.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double ThrottleSmoothingTime {
			get => (double)this.throttleSmoothingTime.GetValue(this.instance);
			set => this.throttleSmoothingTime.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool LimitToPreventFlameout {
			get => (bool)this.limitToPreventFlameout.GetValue(this.instance);
			set => this.limitToPreventFlameout.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double FlameoutSafetyPct {
			get => EditableVariables.GetDouble(this.flameoutSafetyPct);
			set => EditableVariables.SetDouble(this.flameoutSafetyPct, value);
		}

		[KRPCProperty]
		public bool ManageIntakes {
			get => (bool)this.manageIntakes.GetValue(this.instance);
			set => this.manageIntakes.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool DifferentialThrottle {
			get => (bool)this.differentialThrottle.GetValue(this.instance);
			set => this.differentialThrottle.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public DifferentialThrottleStatus DifferentialThrottleStatus => (DifferentialThrottleStatus)this.differentialThrottleSuccess.GetValue(this.instance);

		[KRPCProperty]
		public bool ElectricThrottle {
			get => (bool)this.electricThrottle.GetValue(this.instance);
			set => this.electricThrottle.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double ElectricThrottleLo {
			get => EditableVariables.GetDouble(this.electricThrottleLo);
			set => EditableVariables.SetDouble(this.electricThrottleLo, value);
		}

		[KRPCProperty]
		public double ElectricThrottleHi {
			get => EditableVariables.GetDouble(this.electricThrottleHi);
			set => EditableVariables.SetDouble(this.electricThrottleHi, value);
		}
	}

	[KRPCEnum(Service = "MechJeb")]
	public enum DifferentialThrottleStatus {
		Success,
		AllEnginesOff,
		MoreEnginesRequired,
		SolverFailed
	}
}

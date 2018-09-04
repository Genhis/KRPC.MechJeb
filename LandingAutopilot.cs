using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class LandingAutopilot : AutopilotModule {
		private readonly object touchdownSpeed;
		private readonly FieldInfo deployGears;
		private readonly object limitGearsStage;
		private readonly FieldInfo deployChutes;
		private readonly object limitChutesStage;
		private readonly FieldInfo rcsAdjustment;

		private readonly MethodInfo landAtPositionTarget;
		private readonly MethodInfo landUntargeted;
		private readonly MethodInfo stopLanding;

		public LandingAutopilot() : base("LandingAutopilot") {
			this.touchdownSpeed = this.type.GetField("touchdownSpeed").GetValue(this.instance);
			this.deployGears = this.type.GetField("deployGears");
			this.limitGearsStage = this.type.GetField("limitGearsStage").GetValue(this.instance);
			this.deployChutes = this.type.GetField("deployChutes");
			this.limitChutesStage = this.type.GetField("limitChutesStage").GetValue(this.instance);
			this.rcsAdjustment = this.type.GetField("rcsAdjustment");

			this.landAtPositionTarget = this.type.GetMethod("LandAtPositionTarget");
			this.landUntargeted = this.type.GetMethod("LandUntargeted");
			this.stopLanding = this.type.GetMethod("StopLanding");
		}

		[KRPCProperty]
		public double TouchdownSpeed {
			get => EditableVariables.GetDouble(this.touchdownSpeed);
			set => EditableVariables.SetDouble(this.touchdownSpeed, value);
		}

		[KRPCProperty]
		public bool DeployGears {
			get => (bool)this.deployGears.GetValue(this.instance);
			set => this.deployGears.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public int LimitGearsStage {
			get => EditableVariables.GetInt(this.limitGearsStage);
			set => EditableVariables.SetInt(this.limitGearsStage, value);
		}

		[KRPCProperty]
		public bool DeployChutes {
			get => (bool)this.deployChutes.GetValue(this.instance);
			set => this.deployChutes.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public int LimitChutesStage {
			get => EditableVariables.GetInt(this.limitChutesStage);
			set => EditableVariables.SetInt(this.limitChutesStage, value);
		}

		[KRPCProperty]
		public bool RcsAdjustment {
			get => (bool)this.rcsAdjustment.GetValue(this.instance);
			set => this.rcsAdjustment.SetValue(this.instance, value);
		}

		[KRPCMethod]
		public void LandAtPositionTarget() {
			this.landAtPositionTarget.Invoke(this.instance, new object[] { this });
		}

		[KRPCMethod]
		public void LandUntargeted() {
			this.landUntargeted.Invoke(this.instance, new object[] { this });
		}

		[KRPCMethod]
		public void StopLanding() {
			this.stopLanding.Invoke(this.instance, null);
		}

		[KRPCMethod]
		public override void DisableAll() {
			this.DeployChutes = false;
			this.DeployGears = false;
			this.RcsAdjustment = false;
		}
	}
}

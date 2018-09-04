using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class AscentAutopilot : ComputerModule {
		private readonly FieldInfo status;
		private readonly FieldInfo ascentPathIdx;
		private readonly FieldInfo ascentPath;
		private readonly object desiredOrbitAltitude;
		private readonly FieldInfo desiredInclination;
		private readonly FieldInfo autoThrottle;
		private readonly FieldInfo correctiveSteering;
		private readonly object correctiveSteeringGain;
		private readonly FieldInfo forceRoll;
		private readonly object verticalRoll;
		private readonly object turnRoll;
		private readonly FieldInfo autodeploySolarPanels;
		private readonly FieldInfo autoDeployAntennas;
		private readonly FieldInfo skipCircularization;
		private readonly PropertyInfo autostage;
		private readonly FieldInfo limitAoA;
		private readonly object maxAoA;
		private readonly object aoALimitFadeoutPressure;
		private readonly object launchPhaseAngle;
		private readonly object launchLANDifference;
		private readonly object warpCountDown;

		private AscentBase currentAscentPath;

		public AscentAutopilot() : base("AscentAutopilot") {
			this.status = this.type.GetField("status");
			this.ascentPathIdx = this.type.GetField("ascentPathIdx");
			this.ascentPath = this.type.GetField("ascentPath");
			this.desiredOrbitAltitude = this.type.GetField("desiredOrbitAltitude").GetValue(this.instance);
			this.desiredInclination = this.type.GetField("desiredInclination");
			this.autoThrottle = this.type.GetField("autoThrottle");
			this.correctiveSteering = this.type.GetField("correctiveSteering");
			this.correctiveSteeringGain = this.type.GetField("correctiveSteeringGain").GetValue(this.instance);
			this.forceRoll = this.type.GetField("forceRoll");
			this.verticalRoll = this.type.GetField("verticalRoll").GetValue(this.instance);
			this.turnRoll = this.type.GetField("turnRoll").GetValue(this.instance);
			this.autodeploySolarPanels = this.type.GetField("autodeploySolarPanels");
			this.autoDeployAntennas = this.type.GetField("autoDeployAntennas");
			this.skipCircularization = this.type.GetField("skipCircularization");
			this.autostage = this.type.GetProperty("autostage");
			this.limitAoA = this.type.GetField("limitAoA");
			this.maxAoA = this.type.GetField("maxAoA").GetValue(this.instance);
			this.aoALimitFadeoutPressure = this.type.GetField("aoALimitFadeoutPressure").GetValue(this.instance);
			this.launchPhaseAngle = this.type.GetField("launchPhaseAngle").GetValue(this.instance);
			this.launchLANDifference = this.type.GetField("launchLANDifference").GetValue(this.instance);
			this.warpCountDown = this.type.GetField("warpCountDown").GetValue(this.instance);

			this.AscentPathClassic = new AscentClassic();
			this.AscentPathGT = new AscentGT();
			this.AscentPathPEG = new AscentPEG();
		}

		[KRPCProperty]
		public string Status => this.status.GetValue(this.instance).ToString();

		[KRPCProperty]
		public int AscentPathIndex {
			get => (int)this.ascentPathIdx.GetValue(this.instance);
			set {
				if(value < 0 || value > 2)
					return;

				this.currentAscentPath.Enabled = false;
				this.ascentPathIdx.SetValue(this.instance, value);
				switch(value) {
					case 0:
						this.currentAscentPath = this.AscentPathClassic;
						break;
					case 1:
						this.currentAscentPath = this.AscentPathGT;
						break;
					case 2:
						this.currentAscentPath = this.AscentPathPEG;
						break;
				}
				this.ascentPath.SetValue(this.instance, this.currentAscentPath.instance);
				this.currentAscentPath.Enabled = true;
			}
		}

		[KRPCProperty]
		public AscentClassic AscentPathClassic { get; }

		[KRPCProperty]
		public AscentGT AscentPathGT { get; }

		[KRPCProperty]
		public AscentPEG AscentPathPEG { get; }

		[KRPCProperty]
		public double DesiredOrbitAltitude {
			get => EditableVariables.GetDouble(this.desiredOrbitAltitude);
			set => EditableVariables.SetDouble(this.desiredOrbitAltitude, value);
		}

		[KRPCProperty]
		public bool CorrectiveSteering {
			get => (bool)this.correctiveSteering.GetValue(this.instance);
			set => this.correctiveSteering.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double CorrectiveSteeringGain {
			get => EditableVariables.GetDouble(this.correctiveSteeringGain);
			set => EditableVariables.SetDouble(this.correctiveSteeringGain, value);
		}

		[KRPCProperty]
		public bool ForceRoll {
			get => (bool)this.forceRoll.GetValue(this.instance);
			set => this.forceRoll.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double VerticalRoll {
			get => EditableVariables.GetDouble(this.verticalRoll);
			set => EditableVariables.SetDouble(this.verticalRoll, value);
		}

		[KRPCProperty]
		public double TurnRoll {
			get => EditableVariables.GetDouble(this.turnRoll);
			set => EditableVariables.SetDouble(this.turnRoll, value);
		}

		[KRPCProperty]
		public double DesiredInclination {
			get => (double)this.desiredInclination.GetValue(this.instance);
			set => this.desiredInclination.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool AutodeploySolarPanels {
			get => (bool)this.autodeploySolarPanels.GetValue(this.instance);
			set => this.autodeploySolarPanels.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool AutoDeployAntennas {
			get => (bool)this.autoDeployAntennas.GetValue(this.instance);
			set => this.autoDeployAntennas.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool SkipCircularization {
			get => (bool)this.skipCircularization.GetValue(this.instance);
			set => this.skipCircularization.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool Autostage {
			get => (bool)this.autostage.GetValue(this.instance, null);
			set => this.autostage.SetValue(this.instance, value, null);
		}

		[KRPCProperty]
		public StagingController StagingController => MechJeb.StagingController;

		[KRPCProperty]
		public bool LimitAoA {
			get => (bool)this.limitAoA.GetValue(this.instance);
			set => this.limitAoA.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double MaxAoA {
			get => EditableVariables.GetDouble(this.maxAoA);
			set => EditableVariables.SetDouble(this.maxAoA, value);
		}

		[KRPCProperty]
		public double AoALimitFadeoutPressure {
			get => EditableVariables.GetDouble(this.aoALimitFadeoutPressure);
			set => EditableVariables.SetDouble(this.aoALimitFadeoutPressure, value);
		}

		[KRPCProperty]
		public double LaunchPhaseAngle {
			get => EditableVariables.GetDouble(this.launchPhaseAngle);
			set => EditableVariables.SetDouble(this.launchPhaseAngle, value);
		}

		[KRPCProperty]
		public double LaunchLANDifference {
			get => EditableVariables.GetDouble(this.launchLANDifference);
			set => EditableVariables.SetDouble(this.launchLANDifference, value);
		}

		[KRPCProperty]
		public int WarpCountDown {
			get => EditableVariables.GetInt(this.warpCountDown);
			set => EditableVariables.SetInt(this.warpCountDown, value);
		}
	}

	public abstract class AscentBase : ComputerModule {
		public AscentBase(string moduleType) : base(moduleType) { }
	}
}

using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// This module controls the Ascent Guidance in MechJeb 2.
	/// </summary>
	/// <remarks>
	/// See <a href="https://github.com/MuMech/MechJeb2/wiki/Ascent-Guidance#initial-pitch-over-issues">MechJeb2 wiki</a> for more guidance on how to optimally set up this autopilot.
	/// </remarks>
	[KRPCClass(Service = "MechJeb")]
	public class AscentAutopilot : KRPCComputerModule {
		private static FieldInfo desiredInclination;
		private static FieldInfo launchingToPlane;
		private static FieldInfo launchingToRendezvous;

		private readonly object guiInstance;

		private readonly FieldInfo status;
		private readonly PropertyInfo ascentPathIdx;
		private readonly object desiredOrbitAltitude;
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

		private readonly MethodInfo startCountdown;

		public AscentAutopilot() : base("AscentAutopilot") {
			this.guiInstance = MechJeb.GetComputerModule("AscentGuidance");

			this.status = this.type.GetCheckedField("status");
			this.ascentPathIdx = this.type.GetCheckedProperty("ascentPathIdxPublic");
			this.desiredOrbitAltitude = this.type.GetCheckedField("desiredOrbitAltitude").GetValue(this.instance);
			this.autoThrottle = this.type.GetCheckedField("autoThrottle");
			this.correctiveSteering = this.type.GetCheckedField("correctiveSteering");
			this.correctiveSteeringGain = this.type.GetCheckedField("correctiveSteeringGain").GetValue(this.instance);
			this.forceRoll = this.type.GetCheckedField("forceRoll");
			this.verticalRoll = this.type.GetCheckedField("verticalRoll").GetValue(this.instance);
			this.turnRoll = this.type.GetCheckedField("turnRoll").GetValue(this.instance);
			this.autodeploySolarPanels = this.type.GetCheckedField("autodeploySolarPanels");
			this.autoDeployAntennas = this.type.GetCheckedField("autoDeployAntennas");
			this.skipCircularization = this.type.GetCheckedField("skipCircularization");
			this.autostage = this.type.GetCheckedProperty("autostage");
			this.limitAoA = this.type.GetCheckedField("limitAoA");
			this.maxAoA = this.type.GetCheckedField("maxAoA").GetValue(this.instance);
			this.aoALimitFadeoutPressure = this.type.GetCheckedField("aoALimitFadeoutPressure").GetValue(this.instance);
			this.launchPhaseAngle = this.type.GetCheckedField("launchPhaseAngle").GetValue(this.instance);
			this.launchLANDifference = this.type.GetCheckedField("launchLANDifference").GetValue(this.instance);
			this.warpCountDown = this.type.GetCheckedField("warpCountDown").GetValue(this.instance);

			this.startCountdown = this.type.GetCheckedMethod("StartCountdown");

			this.AscentPathClassic = new AscentClassic();
			this.AscentPathGT = new AscentGT();
			this.AscentPathPVG = new AscentPVG();

			// Retrieve the current path index set in mechjeb and enable the path representing that index.
			// It fixes the issue with AscentAutopilot reporting empty status due to a disabled path.
			this.AscentPathIndex = this.AscentPathIndex;
		}

		internal static new bool InitTypes(Type t) {
			switch(t.FullName) {
				case "MuMech.MechJebModuleAscentGuidance":
					desiredInclination = t.GetField("desiredInclination");
					launchingToPlane = t.GetField("launchingToPlane");
					launchingToRendezvous = t.GetField("launchingToRendezvous");
					return true;
				default:
					return LaunchTiming.InitTypes(t);
			}
		}

		/// <summary>
		/// The autopilot status; it depends on the selected ascent path.
		/// </summary>
		[KRPCProperty]
		public string Status => this.status.GetValue(this.instance).ToString();

		/// <summary>
		/// The selected ascent path.
		/// 
		/// 0 = <see cref="AscentClassic" /> (Classic Ascent Profile)
		/// 
		/// 1 = <see cref="AscentGT" /> (Stock-style GravityTurn)
		/// 
		/// 2 = <see cref="AscentPVG" /> (Primer Vector Guidance (RSS/RO))
		/// </summary>
		[KRPCProperty]
		public int AscentPathIndex {
			get => (int)this.ascentPathIdx.GetValue(this.instance, null);
			set {
				if(value < 0 || value > 2)
					return;

				this.ascentPathIdx.SetValue(this.instance, value, null);
			}
		}

		/// <summary>
		/// Get Classic Ascent Profile settings.
		/// </summary>
		[KRPCProperty]
		public AscentClassic AscentPathClassic { get; }

		/// <summary>
		/// Get Stock-style GravityTurn profile settings.
		/// </summary>
		[KRPCProperty]
		public AscentGT AscentPathGT { get; }

		/// <summary>
		/// Get Powered Explicit Guidance (RSS/RO) profile settings.
		/// </summary>
		[KRPCProperty]
		public AscentPVG AscentPathPVG { get; }

		/// <summary>
		/// The desired altitude in kilometres for the final circular orbit.
		/// </summary>
		[KRPCProperty]
		public double DesiredOrbitAltitude {
			get => EditableVariables.GetDouble(this.desiredOrbitAltitude);
			set => EditableVariables.SetDouble(this.desiredOrbitAltitude, value);
		}

		/// <summary>
		/// The desired inclination in degrees for the final circular orbit.
		/// </summary>
		[KRPCProperty]
		public double DesiredInclination {
			// We need to get desiredInclinationGUI value here because it may change over time.
			get => EditableVariables.GetDouble(desiredInclination, this.guiInstance);
			set => EditableVariables.SetDouble(desiredInclination, this.guiInstance, value);
		}

		/// <remarks>Equivalent to <see cref="MechJeb.ThrustController" />.</remarks>
		[KRPCProperty]
		public ThrustController ThrustController => MechJeb.ThrustController;

		/// <summary>
		/// Will cause the craft to steer based on the more accurate velocity vector rather than positional vector (large craft may actually perform better with this box unchecked).
		/// </summary>
		[KRPCProperty]
		public bool CorrectiveSteering {
			get => (bool)this.correctiveSteering.GetValue(this.instance);
			set => this.correctiveSteering.SetValue(this.instance, value);
		}

		/// <summary>
		/// The gain of corrective steering used by the autopilot.
		/// </summary>
		/// <remarks><see cref="CorrectiveSteering" /> needs to be enabled.</remarks>
		[KRPCProperty]
		public double CorrectiveSteeringGain {
			get => EditableVariables.GetDouble(this.correctiveSteeringGain);
			set => EditableVariables.SetDouble(this.correctiveSteeringGain, value);
		}

		/// <summary>
		/// The state of force roll.
		/// </summary>
		[KRPCProperty]
		public bool ForceRoll {
			get => (bool)this.forceRoll.GetValue(this.instance);
			set => this.forceRoll.SetValue(this.instance, value);
		}

		/// <summary>
		/// The vertical/climb roll used by the autopilot.
		/// </summary>
		/// <remarks><see cref="ForceRoll" /> needs to be enabled.</remarks>
		[KRPCProperty]
		public double VerticalRoll {
			get => EditableVariables.GetDouble(this.verticalRoll);
			set => EditableVariables.SetDouble(this.verticalRoll, value);
		}

		/// <summary>
		/// The turn roll used by the autopilot.
		/// </summary>
		/// <remarks><see cref="ForceRoll" /> needs to be enabled.</remarks>
		[KRPCProperty]
		public double TurnRoll {
			get => EditableVariables.GetDouble(this.turnRoll);
			set => EditableVariables.SetDouble(this.turnRoll, value);
		}

		/// <summary>
		/// Whether to deploy solar panels automatically when the ascent finishes.
		/// </summary>
		[KRPCProperty]
		public bool AutodeploySolarPanels {
			get => (bool)this.autodeploySolarPanels.GetValue(this.instance);
			set => this.autodeploySolarPanels.SetValue(this.instance, value);
		}

		/// <summary>
		/// Whether to deploy antennas automatically when the ascent finishes.
		/// </summary>
		[KRPCProperty]
		public bool AutoDeployAntennas {
			get => (bool)this.autoDeployAntennas.GetValue(this.instance);
			set => this.autoDeployAntennas.SetValue(this.instance, value);
		}

		/// <summary>
		/// Whether to skip circularization burn and do only the ascent.
		/// </summary>
		[KRPCProperty]
		public bool SkipCircularization {
			get => (bool)this.skipCircularization.GetValue(this.instance);
			set => this.skipCircularization.SetValue(this.instance, value);
		}

		/// <summary>
		/// The autopilot will automatically stage when the current stage has run out of fuel.
		/// Paramethers can be set in <see cref="KRPC.MechJeb.StagingController" />.
		/// </summary>
		[KRPCProperty]
		public bool Autostage {
			get => (bool)this.autostage.GetValue(this.instance, null);
			set => this.autostage.SetValue(this.instance, value, null);
		}

		/// <remarks>Equivalent to <see cref="MechJeb.StagingController" />.</remarks>
		[KRPCProperty]
		public StagingController StagingController => MechJeb.StagingController;

		/// <summary>
		/// Whether to limit angle of attack.
		/// </summary>
		[KRPCProperty]
		public bool LimitAoA {
			get => (bool)this.limitAoA.GetValue(this.instance);
			set => this.limitAoA.SetValue(this.instance, value);
		}

		/// <summary>
		/// The maximal angle of attack used by the autopilot.
		/// </summary>
		/// <remarks><see cref="LimitAoA" /> needs to be enabled</remarks>
		[KRPCProperty]
		public double MaxAoA {
			get => EditableVariables.GetDouble(this.maxAoA);
			set => EditableVariables.SetDouble(this.maxAoA, value);
		}

		/// <summary>
		/// The pressure value when AoA limit is automatically deactivated.
		/// </summary>
		/// <remarks><see cref="LimitAoA" /> needs to be enabled</remarks>
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

		[KRPCMethod]
		public void LaunchToRendezvous() {
			this.AbortLaunch();
			launchingToRendezvous.SetValue(this.guiInstance, true);
			this.startCountdown.Invoke(this.instance, new object[] { Planetarium.GetUniversalTime() + LaunchTiming.TimeToPhaseAngle(this.LaunchPhaseAngle) });
		}

		/// <summary>
		/// Launch into the plane of the selected target.
		/// </summary>
		[KRPCMethod]
		public void LaunchToTargetPlane() {
			this.AbortLaunch();
			launchingToPlane.SetValue(this.guiInstance, true);
			this.startCountdown.Invoke(this.instance, new object[] { Planetarium.GetUniversalTime() + LaunchTiming.TimeToPhaseAngle(this.LaunchLANDifference) });
		}

		private void AbortLaunch() {
			launchingToPlane.SetValue(this.guiInstance, false);
			launchingToRendezvous.SetValue(this.guiInstance, false);
		}
	}

	public abstract class AscentBase : ComputerModule {
		public AscentBase(string moduleType) : base(moduleType) { }
	}

	public static class LaunchTiming {
		private static MethodInfo timeToPhaseAngle;
		private static MethodInfo timeToPlane;

		internal static bool InitTypes(Type t) {
			switch(t.FullName) {
				case "MuMech.LaunchTiming":
					timeToPhaseAngle = t.GetCheckedMethod("TimeToPhaseAngle");
					timeToPlane = t.GetCheckedMethod("TimeToPlane");
					return true;
				default:
					return false;
			}
		}

		public static double TimeToPhaseAngle(double launchPhaseAngle) {
			return (double)timeToPhaseAngle.Invoke(null, new object[] { launchPhaseAngle, FlightGlobals.ActiveVessel.mainBody, getLongtitude(), MechJeb.TargetController.TargetOrbit.InternalOrbit });
		}

		public static double TimeToPlane(double launchLANDifference) {
			Vessel vessel = FlightGlobals.ActiveVessel;
			CelestialBody body = vessel.mainBody;
			return (double)timeToPlane.Invoke(null, new object[] { launchLANDifference, body, body.GetLatitude(vessel.CoMD), getLongtitude(), MechJeb.TargetController.TargetOrbit.InternalOrbit });
		}

		private static double getLongtitude() {
			Vessel vessel = FlightGlobals.ActiveVessel;
			double longtitude = vessel.mainBody.GetLongitude(vessel.CoMD) % 360;
			if(longtitude > 180)
				longtitude -= 360;

			return longtitude;
		}
	}
}

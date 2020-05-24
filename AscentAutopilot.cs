using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	internal static class AscentGuidance {
		internal new const string MechJebType = "MuMech.MechJebModuleAscentGuidance";

		// Fields and methods
		internal static FieldInfo desiredInclination;
		internal static FieldInfo launchingToPlane;
		internal static FieldInfo launchingToRendezvous;

		internal static void InitType(Type type) {
			desiredInclination = type.GetField("desiredInclination");
			launchingToPlane = type.GetField("launchingToPlane");
			launchingToRendezvous = type.GetField("launchingToRendezvous");
		}
	}

	/// <summary>
	/// This module controls the Ascent Guidance in MechJeb 2.
	/// </summary>
	/// <remarks>
	/// See <a href="https://github.com/MuMech/MechJeb2/wiki/Ascent-Guidance#initial-pitch-over-issues">MechJeb2 wiki</a> for more guidance on how to optimally set up this autopilot.
	/// </remarks>
	[KRPCClass(Service = "MechJeb")]
	public class AscentAutopilot : KRPCComputerModule {
		internal new const string MechJebType = "MuMech.MechJebModuleAscentAutopilot";

		// Fields and methods
		private static FieldInfo status;
		private static PropertyInfo ascentPathIdx;
		private static FieldInfo desiredOrbitAltitudeField;
		private static FieldInfo autoThrottle;
		private static FieldInfo correctiveSteering;
		private static FieldInfo correctiveSteeringGainField;
		private static FieldInfo forceRoll;
		private static FieldInfo verticalRollField;
		private static FieldInfo turnRollField;
		private static FieldInfo autodeploySolarPanels;
		private static FieldInfo autoDeployAntennas;
		private static FieldInfo skipCircularization;
		private static PropertyInfo autostage;
		private static FieldInfo limitAoA;
		private static FieldInfo maxAoAField;
		private static FieldInfo aoALimitFadeoutPressureField;
		private static FieldInfo launchPhaseAngleField;
		private static FieldInfo launchLANDifferenceField;
		private static FieldInfo warpCountDownField;

		private static FieldInfo timedLaunch;
		private static MethodInfo startCountdown;

		// Instance objects
		private object guiInstance;

		private object desiredOrbitAltitude;
		private object correctiveSteeringGain;
		private object verticalRoll;
		private object turnRoll;
		private object maxAoA;
		private object aoALimitFadeoutPressure;
		private object launchPhaseAngle;
		private object launchLANDifference;
		private object warpCountDown;

		internal static new void InitType(Type type) {
			status = type.GetCheckedField("status");
			ascentPathIdx = type.GetCheckedProperty("ascentPathIdxPublic");
			desiredOrbitAltitudeField = type.GetCheckedField("desiredOrbitAltitude");
			autoThrottle = type.GetCheckedField("autoThrottle");
			correctiveSteering = type.GetCheckedField("correctiveSteering");
			correctiveSteeringGainField = type.GetCheckedField("correctiveSteeringGain");
			forceRoll = type.GetCheckedField("forceRoll");
			verticalRollField = type.GetCheckedField("verticalRoll");
			turnRollField = type.GetCheckedField("turnRoll");
			autodeploySolarPanels = type.GetCheckedField("autodeploySolarPanels");
			autoDeployAntennas = type.GetCheckedField("autoDeployAntennas");
			skipCircularization = type.GetCheckedField("skipCircularization");
			autostage = type.GetCheckedProperty("autostage");
			limitAoA = type.GetCheckedField("limitAoA");
			maxAoAField = type.GetCheckedField("maxAoA");
			aoALimitFadeoutPressureField = type.GetCheckedField("aoALimitFadeoutPressure");
			launchPhaseAngleField = type.GetCheckedField("launchPhaseAngle");
			launchLANDifferenceField = type.GetCheckedField("launchLANDifference");
			warpCountDownField = type.GetCheckedField("warpCountDown");

			timedLaunch = type.GetCheckedField("timedLaunch");
			startCountdown = type.GetCheckedMethod("StartCountdown");
		}

		protected internal override void InitInstance(object instance) {
			base.InitInstance(instance);
			this.guiInstance = MechJeb.GetComputerModule("AscentGuidance");

			this.desiredOrbitAltitude = desiredOrbitAltitudeField.GetInstanceValue(instance);
			this.correctiveSteeringGain = correctiveSteeringGainField.GetInstanceValue(instance);
			this.verticalRoll = verticalRollField.GetInstanceValue(instance);
			this.turnRoll = turnRollField.GetInstanceValue(instance);
			this.maxAoA = maxAoAField.GetInstanceValue(instance);
			this.aoALimitFadeoutPressure = aoALimitFadeoutPressureField.GetInstanceValue(instance);
			this.launchPhaseAngle = launchPhaseAngleField.GetInstanceValue(instance);
			this.launchLANDifference = launchLANDifferenceField.GetInstanceValue(instance);
			this.warpCountDown = warpCountDownField.GetInstanceValue(instance);

			this.AscentPathClassic.InitInstance(MechJeb.GetComputerModule("AscentClassic"));
			this.AscentPathGT.InitInstance(MechJeb.GetComputerModule("AscentGT"));
			this.AscentPathPVG.InitInstance(MechJeb.GetComputerModule("AscentPVG"));

			// Retrieve the current path index set in mechjeb and enable the path representing that index.
			// It fixes the issue with AscentAutopilot reporting empty status due to a disabled path.
			if(instance != null)
				this.AscentPathIndex = this.AscentPathIndex;
		}

		public AscentAutopilot() {
			this.AscentPathClassic = new AscentClassic();
			this.AscentPathGT = new AscentGT();
			this.AscentPathPVG = new AscentPVG();
		}

		/// <summary>
		/// The autopilot status; it depends on the selected ascent path.
		/// </summary>
		[KRPCProperty]
		public string Status => status.GetValue(this.instance).ToString();

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
			get => (int)ascentPathIdx.GetValue(this.instance, null);
			set {
				if(value < 0 || value > 2)
					return;

				ascentPathIdx.SetValue(this.instance, value, null);
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
			get => EditableDouble.Get(this.desiredOrbitAltitude);
			set => EditableDouble.Set(this.desiredOrbitAltitude, value);
		}

		/// <summary>
		/// The desired inclination in degrees for the final circular orbit.
		/// </summary>
		[KRPCProperty]
		public double DesiredInclination {
			// We need to get desiredInclinationGUI value here because it may change over time.
			get => EditableDouble.Get(AscentGuidance.desiredInclination, this.guiInstance);
			set => EditableDouble.Set(AscentGuidance.desiredInclination, this.guiInstance, value);
		}

		/// <remarks>Equivalent to <see cref="MechJeb.ThrustController" />.</remarks>
		[KRPCProperty]
		public ThrustController ThrustController => MechJeb.ThrustController;

		/// <summary>
		/// Will cause the craft to steer based on the more accurate velocity vector rather than positional vector (large craft may actually perform better with this box unchecked).
		/// </summary>
		[KRPCProperty]
		public bool CorrectiveSteering {
			get => (bool)correctiveSteering.GetValue(this.instance);
			set => correctiveSteering.SetValue(this.instance, value);
		}

		/// <summary>
		/// The gain of corrective steering used by the autopilot.
		/// </summary>
		/// <remarks><see cref="CorrectiveSteering" /> needs to be enabled.</remarks>
		[KRPCProperty]
		public double CorrectiveSteeringGain {
			get => EditableDouble.Get(this.correctiveSteeringGain);
			set => EditableDouble.Set(this.correctiveSteeringGain, value);
		}

		/// <summary>
		/// The state of force roll.
		/// </summary>
		[KRPCProperty]
		public bool ForceRoll {
			get => (bool)forceRoll.GetValue(this.instance);
			set => forceRoll.SetValue(this.instance, value);
		}

		/// <summary>
		/// The vertical/climb roll used by the autopilot.
		/// </summary>
		/// <remarks><see cref="ForceRoll" /> needs to be enabled.</remarks>
		[KRPCProperty]
		public double VerticalRoll {
			get => EditableDouble.Get(this.verticalRoll);
			set => EditableDouble.Set(this.verticalRoll, value);
		}

		/// <summary>
		/// The turn roll used by the autopilot.
		/// </summary>
		/// <remarks><see cref="ForceRoll" /> needs to be enabled.</remarks>
		[KRPCProperty]
		public double TurnRoll {
			get => EditableDouble.Get(this.turnRoll);
			set => EditableDouble.Set(this.turnRoll, value);
		}

		/// <summary>
		/// Whether to deploy solar panels automatically when the ascent finishes.
		/// </summary>
		[KRPCProperty]
		public bool AutodeploySolarPanels {
			get => (bool)autodeploySolarPanels.GetValue(this.instance);
			set => autodeploySolarPanels.SetValue(this.instance, value);
		}

		/// <summary>
		/// Whether to deploy antennas automatically when the ascent finishes.
		/// </summary>
		[KRPCProperty]
		public bool AutoDeployAntennas {
			get => (bool)autoDeployAntennas.GetValue(this.instance);
			set => autoDeployAntennas.SetValue(this.instance, value);
		}

		/// <summary>
		/// Whether to skip circularization burn and do only the ascent.
		/// </summary>
		[KRPCProperty]
		public bool SkipCircularization {
			get => (bool)skipCircularization.GetValue(this.instance);
			set => skipCircularization.SetValue(this.instance, value);
		}

		/// <summary>
		/// The autopilot will automatically stage when the current stage has run out of fuel.
		/// Paramethers can be set in <see cref="KRPC.MechJeb.StagingController" />.
		/// </summary>
		[KRPCProperty]
		public bool Autostage {
			get => (bool)autostage.GetValue(this.instance, null);
			set => autostage.SetValue(this.instance, value, null);
		}

		/// <remarks>Equivalent to <see cref="MechJeb.StagingController" />.</remarks>
		[KRPCProperty]
		public StagingController StagingController => MechJeb.StagingController;

		/// <summary>
		/// Whether to limit angle of attack.
		/// </summary>
		[KRPCProperty]
		public bool LimitAoA {
			get => (bool)limitAoA.GetValue(this.instance);
			set => limitAoA.SetValue(this.instance, value);
		}

		/// <summary>
		/// The maximal angle of attack used by the autopilot.
		/// </summary>
		/// <remarks><see cref="LimitAoA" /> needs to be enabled</remarks>
		[KRPCProperty]
		public double MaxAoA {
			get => EditableDouble.Get(this.maxAoA);
			set => EditableDouble.Set(this.maxAoA, value);
		}

		/// <summary>
		/// The pressure value when AoA limit is automatically deactivated.
		/// </summary>
		/// <remarks><see cref="LimitAoA" /> needs to be enabled</remarks>
		[KRPCProperty]
		public double AoALimitFadeoutPressure {
			get => EditableDouble.Get(this.aoALimitFadeoutPressure);
			set => EditableDouble.Set(this.aoALimitFadeoutPressure, value);
		}

		[KRPCProperty]
		public double LaunchPhaseAngle {
			get => EditableDouble.Get(this.launchPhaseAngle);
			set => EditableDouble.Set(this.launchPhaseAngle, value);
		}

		[KRPCProperty]
		public double LaunchLANDifference {
			get => EditableDouble.Get(this.launchLANDifference);
			set => EditableDouble.Set(this.launchLANDifference, value);
		}

		[KRPCProperty]
		public int WarpCountDown {
			get => EditableInt.Get(this.warpCountDown);
			set => EditableInt.Set(this.warpCountDown, value);
		}

		/// <summary>
		/// Current autopilot mode. Useful for determining whether the autopilot is performing a timed launch or not.
		/// </summary>
		[KRPCProperty]
		public AscentLaunchMode LaunchMode {
			get {
				if(!(bool)timedLaunch.GetValue(this.instance))
					return AscentLaunchMode.Normal;
				if((bool)AscentGuidance.launchingToRendezvous.GetValue(this.guiInstance))
					return AscentLaunchMode.Rendezvous;
				if((bool)AscentGuidance.launchingToPlane.GetValue(this.guiInstance))
					return AscentLaunchMode.TargetPlane;
				return AscentLaunchMode.Unknown;
			}
		}

		/// <summary>
		/// Abort a known timed launch when it has not started yet
		/// </summary>
		[KRPCMethod]
		public void AbortTimedLaunch() {
			if(this.LaunchMode == AscentLaunchMode.Unknown)
				throw new InvalidOperationException("There is an unknown timed launch ongoing which can't be aborted");

			AscentGuidance.launchingToPlane.SetValue(this.guiInstance, false);
			AscentGuidance.launchingToRendezvous.SetValue(this.guiInstance, false);
			timedLaunch.SetValue(this.instance, false);
		}

		/// <summary>
		/// Launch to rendezvous with the selected target.
		/// </summary>
		[KRPCMethod]
		public void LaunchToRendezvous() {
			this.AbortTimedLaunch();
			AscentGuidance.launchingToRendezvous.SetValue(this.guiInstance, true);
			startCountdown.Invoke(this.instance, new object[] { Planetarium.GetUniversalTime() + LaunchTiming.TimeToPhaseAngle(this.LaunchPhaseAngle) });
		}

		/// <summary>
		/// Launch into the plane of the selected target.
		/// </summary>
		[KRPCMethod]
		public void LaunchToTargetPlane() {
			this.AbortTimedLaunch();
			AscentGuidance.launchingToPlane.SetValue(this.guiInstance, true);
			startCountdown.Invoke(this.instance, new object[] { Planetarium.GetUniversalTime() + LaunchTiming.TimeToPhaseAngle(this.LaunchLANDifference) });
		}

		[KRPCEnum(Service = "MechJeb")]
		public enum AscentLaunchMode {
			/// <summary>
			/// The autopilot is not performing a timed launch.
			/// </summary>
			Normal,

			/// <summary>
			/// The autopilot is performing a timed launch to rendezvous with the target vessel.
			/// </summary>
			Rendezvous,

			/// <summary>
			/// The autopilot is performing a timed launch to target plane.
			/// </summary>
			TargetPlane,

			/// <summary>
			/// The autopilot is performing an unknown timed launch.
			/// </summary>
			Unknown = 99
		}
	}

	public abstract class AscentBase : ComputerModule { }

	public static class LaunchTiming {
		internal const string MechJebType = "MuMech.LaunchTiming";

		// Fields and methods
		private static MethodInfo timeToPhaseAngle;
		private static MethodInfo timeToPlane;

		internal static void InitType(Type type) {
			timeToPhaseAngle = type.GetCheckedMethod("TimeToPhaseAngle");
			timeToPlane = type.GetCheckedMethod("TimeToPlane");
		}

		public static double TimeToPhaseAngle(double launchPhaseAngle) {
			return (double)timeToPhaseAngle.Invoke(null, new object[] { launchPhaseAngle, FlightGlobals.ActiveVessel.mainBody, GetLongtitude(), MechJeb.TargetController.TargetOrbit.InternalOrbit });
		}

		public static double TimeToPlane(double launchLANDifference) {
			Vessel vessel = FlightGlobals.ActiveVessel;
			CelestialBody body = vessel.mainBody;
			return (double)timeToPlane.Invoke(null, new object[] { launchLANDifference, body, body.GetLatitude(vessel.CoMD), GetLongtitude(), MechJeb.TargetController.TargetOrbit.InternalOrbit });
		}

		private static double GetLongtitude() {
			Vessel vessel = FlightGlobals.ActiveVessel;
			double longtitude = vessel.mainBody.GetLongitude(vessel.CoMD) % 360;
			if(longtitude > 180)
				longtitude -= 360;

			return longtitude;
		}
	}
}

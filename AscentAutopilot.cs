using System;
using System.Collections.Generic;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.MechJeb.Util;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// Reflection cache for MuMech.MechJebModuleAscentSettings — the v2.15.0 class that
	/// owns every configurable field for the ascent guidance autopilot.
	/// </summary>
	internal static class AscentSettings {
		internal const string MechJebType = "MuMech.MechJebModuleAscentSettings";

		// Timed-launch flags
		internal static FieldInfo launchingToPlane;
		internal static FieldInfo launchingToRendezvous;
		internal static FieldInfo launchingToMatchLan;
		internal static FieldInfo launchingToLan;
		internal static FieldInfo overrideWarpToPlane;

		// Ascent type (selector)
		internal static FieldInfo ascentTypeInteger;

		// Target orbit
		internal static FieldInfo desiredOrbitAltitude;
		internal static FieldInfo desiredApoapsis;
		internal static FieldInfo desiredAttachAlt;
		internal static FieldInfo desiredAttachAltFixed;
		internal static FieldInfo desiredFPA;
		internal static FieldInfo desiredInclination;
		internal static FieldInfo desiredLan;
		internal static FieldInfo attachAltFlag;

		// Launch timing
		internal static FieldInfo launchPhaseAngle;
		internal static FieldInfo launchLANDifference;
		internal static FieldInfo warpCountDown;

		// General / steering
		internal static FieldInfo correctiveSteering;
		internal static FieldInfo correctiveSteeringGain;
		internal static FieldInfo forceRoll;
		internal static FieldInfo verticalRoll;
		internal static FieldInfo turnRoll;
		internal static FieldInfo rollAltitude;
		internal static FieldInfo autodeploySolarPanels;
		internal static FieldInfo autoDeployAntennas;
		internal static FieldInfo skipCircularization;
		internal static PropertyInfo autostage;
		internal static FieldInfo limitAoA;
		internal static FieldInfo maxAoA;
		internal static FieldInfo aoALimitFadeoutPressure;
		internal static FieldInfo limitingAoA;
		internal static FieldInfo limitQa;
		internal static FieldInfo limitQaEnabled;

		// Classic path
		internal static FieldInfo turnStartAltitude;
		internal static FieldInfo turnStartVelocity;
		internal static FieldInfo turnEndAltitude;
		internal static FieldInfo turnEndAngle;
		internal static FieldInfo turnShapeExponent;
		internal static FieldInfo autoPath;
		internal static FieldInfo autoTurnPerc;
		internal static FieldInfo autoTurnSpdFactor;
		internal static PropertyInfo autoTurnStartAltitude;
		internal static PropertyInfo autoTurnStartVelocity;
		internal static PropertyInfo autoTurnEndAltitude;

		// PVG path
		internal static FieldInfo pitchStartVelocity;
		internal static FieldInfo pitchRate;
		internal static FieldInfo dynamicPressureTrigger;
		internal static FieldInfo stagingTrigger;
		internal static FieldInfo stagingTriggerFlag;
		internal static FieldInfo fixedCoast;
		internal static FieldInfo fixedCoastLength;
		internal static FieldInfo coastBeforeFlag;
		internal static FieldInfo minDeltaV;
		internal static FieldInfo lastStage;
		internal static PropertyInfo optimizeStage;
		internal static FieldInfo optimizeStageInternal;
		internal static FieldInfo optimizeStageFlag;
		internal static PropertyInfo coastStage;
		internal static FieldInfo coastStageInternal;
		internal static FieldInfo coastStageFlag;
		internal static PropertyInfo spinupStage;
		internal static FieldInfo spinupStageInternal;
		internal static FieldInfo spinupStageFlag;
		internal static FieldInfo spinupAngularVelocity;
		internal static FieldInfo spinupLeadTime;
		internal static PropertyInfo unguidedStages;
		internal static FieldInfo unguidedStagesFlag;

		internal static void InitType(Type type) {
			launchingToPlane = type.GetCheckedField("LaunchingToPlane");
			launchingToRendezvous = type.GetCheckedField("LaunchingToRendezvous");
			launchingToMatchLan = type.GetCheckedField("LaunchingToMatchLan");
			launchingToLan = type.GetCheckedField("LaunchingToLan");
			overrideWarpToPlane = type.GetCheckedField("OverrideWarpToPlane");

			ascentTypeInteger = type.GetCheckedField("AscentTypeInteger");

			desiredOrbitAltitude = type.GetCheckedField("DesiredOrbitAltitude");
			desiredApoapsis = type.GetCheckedField("DesiredApoapsis");
			desiredAttachAlt = type.GetCheckedField("DesiredAttachAlt");
			desiredAttachAltFixed = type.GetCheckedField("DesiredAttachAltFixed");
			desiredFPA = type.GetCheckedField("DesiredFPA");
			desiredInclination = type.GetCheckedField("DesiredInclination");
			desiredLan = type.GetCheckedField("DesiredLan");
			attachAltFlag = type.GetCheckedField("AttachAltFlag");

			launchPhaseAngle = type.GetCheckedField("LaunchPhaseAngle");
			launchLANDifference = type.GetCheckedField("LaunchLANDifference");
			warpCountDown = type.GetCheckedField("WarpCountDown");

			correctiveSteering = type.GetCheckedField("CorrectiveSteering");
			correctiveSteeringGain = type.GetCheckedField("CorrectiveSteeringGain");
			forceRoll = type.GetCheckedField("ForceRoll");
			verticalRoll = type.GetCheckedField("VerticalRoll");
			turnRoll = type.GetCheckedField("TurnRoll");
			rollAltitude = type.GetCheckedField("RollAltitude");
			autodeploySolarPanels = type.GetCheckedField("AutodeploySolarPanels");
			autoDeployAntennas = type.GetCheckedField("AutoDeployAntennas");
			skipCircularization = type.GetCheckedField("SkipCircularization");
			autostage = type.GetCheckedProperty("Autostage");
			limitAoA = type.GetCheckedField("LimitAoA");
			maxAoA = type.GetCheckedField("MaxAoA");
			aoALimitFadeoutPressure = type.GetCheckedField("AOALimitFadeoutPressure");
			limitingAoA = type.GetCheckedField("LimitingAoA");
			limitQa = type.GetCheckedField("LimitQa");
			limitQaEnabled = type.GetCheckedField("LimitQaEnabled");

			turnStartAltitude = type.GetCheckedField("TurnStartAltitude");
			turnStartVelocity = type.GetCheckedField("TurnStartVelocity");
			turnEndAltitude = type.GetCheckedField("TurnEndAltitude");
			turnEndAngle = type.GetCheckedField("TurnEndAngle");
			turnShapeExponent = type.GetCheckedField("TurnShapeExponent");
			autoPath = type.GetCheckedField("AutoPath");
			autoTurnPerc = type.GetCheckedField("AutoTurnPerc");
			autoTurnSpdFactor = type.GetCheckedField("AutoTurnSpdFactor");
			autoTurnStartAltitude = type.GetCheckedProperty("AutoTurnStartAltitude");
			autoTurnStartVelocity = type.GetCheckedProperty("AutoTurnStartVelocity");
			autoTurnEndAltitude = type.GetCheckedProperty("AutoTurnEndAltitude");

			pitchStartVelocity = type.GetCheckedField("PitchStartVelocity");
			pitchRate = type.GetCheckedField("PitchRate");
			dynamicPressureTrigger = type.GetCheckedField("DynamicPressureTrigger");
			stagingTrigger = type.GetCheckedField("StagingTrigger");
			stagingTriggerFlag = type.GetCheckedField("StagingTriggerFlag");
			fixedCoast = type.GetCheckedField("FixedCoast");
			fixedCoastLength = type.GetCheckedField("FixedCoastLength");
			coastBeforeFlag = type.GetCheckedField("CoastBeforeFlag");
			minDeltaV = type.GetCheckedField("MinDeltaV");
			lastStage = type.GetCheckedField("LastStage");
			optimizeStage = type.GetCheckedProperty("OptimizeStage");
			optimizeStageInternal = type.GetCheckedField("OptimizeStageInternal");
			optimizeStageFlag = type.GetCheckedField("OptimizeStageFlag");
			coastStage = type.GetCheckedProperty("CoastStage");
			coastStageInternal = type.GetCheckedField("CoastStageInternal");
			coastStageFlag = type.GetCheckedField("CoastStageFlag");
			spinupStage = type.GetCheckedProperty("SpinupStage");
			spinupStageInternal = type.GetCheckedField("SpinupStageInternal");
			spinupStageFlag = type.GetCheckedField("SpinupStageFlag");
			spinupAngularVelocity = type.GetCheckedField("SpinupAngularVelocity");
			spinupLeadTime = type.GetCheckedField("SpinupLeadTime");
			unguidedStages = type.GetCheckedProperty("UnguidedStages");
			unguidedStagesFlag = type.GetCheckedField("UnguidedStagesFlag");
		}
	}

	/// <summary>
	/// Reflection cache for MuMech.MechJebModuleAscentBaseAutopilot — the abstract base
	/// that holds runtime state and StartCountdown. Inherited by the Classic and PVG
	/// concrete autopilots.
	/// </summary>
	internal static class AscentBaseAutopilot {
		internal const string MechJebType = "MuMech.MechJebModuleAscentBaseAutopilot";

		internal static FieldInfo status;
		internal static FieldInfo timedLaunch;
		internal static FieldInfo currentMaxAoA;
		internal static FieldInfo launchTime;
		internal static FieldInfo launchStarted;
		internal static FieldInfo mode;
		internal static MethodInfo startCountdown;

		internal static void InitType(Type type) {
			status = type.GetCheckedField("Status");
			timedLaunch = type.GetCheckedField("TimedLaunch");
			currentMaxAoA = type.GetCheckedField("CurrentMaxAoA");
			launchTime = type.GetCheckedField("_launchTime", BindingFlags.NonPublic | BindingFlags.Instance);
			launchStarted = type.GetCheckedField("_launchStarted", BindingFlags.NonPublic | BindingFlags.Instance);
			mode = type.GetCheckedField("_mode", BindingFlags.NonPublic | BindingFlags.Instance);
			startCountdown = type.GetCheckedMethod("StartCountdown", new Type[] { typeof(double) });
		}
	}

	/// <summary>
	/// This module controls the Ascent Guidance in MechJeb 2.
	/// </summary>
	/// <remarks>
	/// See <a href="https://github.com/MuMech/MechJeb2/wiki/Ascent-Guidance#initial-pitch-over-issues">MechJeb2 wiki</a> for more guidance on how to optimally set up this autopilot.
	///
	/// Enabling/disabling this autopilot toggles the concrete autopilot matching the current
	/// <see cref="AscentType" />. Changing <see cref="AscentType" /> while the autopilot is enabled
	/// does not automatically transfer enablement to the new path; disable and re-enable.
	/// </remarks>
	[KRPCClass(Service = "MechJeb")]
	public class AscentAutopilot : KRPCComputerModule {
		// Runtime instance handles
		private object classicAutopilot;
		private object pvgAutopilot;

		// Pre-fetched EditableDouble/EditableInt instances (settings)
		private object desiredOrbitAltitude;
		private object desiredApoapsis;
		private object desiredAttachAlt;
		private object desiredAttachAltFixed;
		private object desiredFPA;
		private object desiredInclination;
		private object desiredLan;
		private object launchPhaseAngle;
		private object launchLANDifference;
		private object warpCountDown;
		private object correctiveSteeringGain;
		private object verticalRoll;
		private object turnRoll;
		private object rollAltitude;
		private object maxAoA;
		private object aoALimitFadeoutPressure;
		private object limitQa;
		private object turnStartAltitude;
		private object turnStartVelocity;
		private object turnEndAltitude;
		private object turnEndAngle;
		private object turnShapeExponent;
		private object pitchStartVelocity;
		private object pitchRate;
		private object dynamicPressureTrigger;
		private object stagingTrigger;
		private object fixedCoastLength;
		private object minDeltaV;
		private object lastStage;
		private object optimizeStageInternal;
		private object coastStageInternal;
		private object spinupStageInternal;
		private object spinupAngularVelocity;
		private object spinupLeadTime;

		protected internal override void InitInstance(object instance) {
			base.InitInstance(instance);

			this.classicAutopilot = MechJeb.GetComputerModule("AscentClassicAutopilot");
			this.pvgAutopilot = MechJeb.GetComputerModule("AscentPVGAutopilot");

			this.desiredOrbitAltitude = AscentSettings.desiredOrbitAltitude.GetInstanceValue(instance);
			this.desiredApoapsis = AscentSettings.desiredApoapsis.GetInstanceValue(instance);
			this.desiredAttachAlt = AscentSettings.desiredAttachAlt.GetInstanceValue(instance);
			this.desiredAttachAltFixed = AscentSettings.desiredAttachAltFixed.GetInstanceValue(instance);
			this.desiredFPA = AscentSettings.desiredFPA.GetInstanceValue(instance);
			this.desiredInclination = AscentSettings.desiredInclination.GetInstanceValue(instance);
			this.desiredLan = AscentSettings.desiredLan.GetInstanceValue(instance);
			this.launchPhaseAngle = AscentSettings.launchPhaseAngle.GetInstanceValue(instance);
			this.launchLANDifference = AscentSettings.launchLANDifference.GetInstanceValue(instance);
			this.warpCountDown = AscentSettings.warpCountDown.GetInstanceValue(instance);
			this.correctiveSteeringGain = AscentSettings.correctiveSteeringGain.GetInstanceValue(instance);
			this.verticalRoll = AscentSettings.verticalRoll.GetInstanceValue(instance);
			this.turnRoll = AscentSettings.turnRoll.GetInstanceValue(instance);
			this.rollAltitude = AscentSettings.rollAltitude.GetInstanceValue(instance);
			this.maxAoA = AscentSettings.maxAoA.GetInstanceValue(instance);
			this.aoALimitFadeoutPressure = AscentSettings.aoALimitFadeoutPressure.GetInstanceValue(instance);
			this.limitQa = AscentSettings.limitQa.GetInstanceValue(instance);
			this.turnStartAltitude = AscentSettings.turnStartAltitude.GetInstanceValue(instance);
			this.turnStartVelocity = AscentSettings.turnStartVelocity.GetInstanceValue(instance);
			this.turnEndAltitude = AscentSettings.turnEndAltitude.GetInstanceValue(instance);
			this.turnEndAngle = AscentSettings.turnEndAngle.GetInstanceValue(instance);
			this.turnShapeExponent = AscentSettings.turnShapeExponent.GetInstanceValue(instance);
			this.pitchStartVelocity = AscentSettings.pitchStartVelocity.GetInstanceValue(instance);
			this.pitchRate = AscentSettings.pitchRate.GetInstanceValue(instance);
			this.dynamicPressureTrigger = AscentSettings.dynamicPressureTrigger.GetInstanceValue(instance);
			this.stagingTrigger = AscentSettings.stagingTrigger.GetInstanceValue(instance);
			this.fixedCoastLength = AscentSettings.fixedCoastLength.GetInstanceValue(instance);
			this.minDeltaV = AscentSettings.minDeltaV.GetInstanceValue(instance);
			this.lastStage = AscentSettings.lastStage.GetInstanceValue(instance);
			this.optimizeStageInternal = AscentSettings.optimizeStageInternal.GetInstanceValue(instance);
			this.coastStageInternal = AscentSettings.coastStageInternal.GetInstanceValue(instance);
			this.spinupStageInternal = AscentSettings.spinupStageInternal.GetInstanceValue(instance);
			this.spinupAngularVelocity = AscentSettings.spinupAngularVelocity.GetInstanceValue(instance);
			this.spinupLeadTime = AscentSettings.spinupLeadTime.GetInstanceValue(instance);
		}

		private object ActiveAutopilot => this.AscentType == 0 ? this.classicAutopilot : this.pvgAutopilot;

		/// <summary>
		/// Whether the ascent autopilot is engaged. Setting this to <c>true</c> engages the autopilot
		/// matching the current <see cref="AscentType" />; setting it to <c>false</c> disengages both.
		/// </summary>
		[KRPCProperty]
		public override bool Enabled {
			get => IsEnabled(this.classicAutopilot) || IsEnabled(this.pvgAutopilot);
			set {
				if(value) {
					SetEnabled(this.ActiveAutopilot, this, true);
				} else {
					SetEnabled(this.classicAutopilot, this, false);
					SetEnabled(this.pvgAutopilot, this, false);
				}
			}
		}

		/// <summary>
		/// The autopilot status; it depends on the selected ascent path.
		/// </summary>
		[KRPCProperty]
		public string Status {
			get {
				object active = this.ActiveAutopilot;
				return active == null ? "" : (string)AscentBaseAutopilot.status.GetValue(active);
			}
		}

		/// <summary>
		/// The selected ascent type.
		///
		/// 0 = Classic Ascent Profile
		///
		/// 1 = Primer Vector Guidance (RSS/RO)
		/// </summary>
		[KRPCProperty]
		public int AscentType {
			get => (int)AscentSettings.ascentTypeInteger.GetValue(this.instance);
			set {
				if(value < 0 || value > 1)
					return;
				AscentSettings.ascentTypeInteger.SetValue(this.instance, value);
			}
		}

		// ---------- Target orbit ----------

		/// <summary>
		/// The desired altitude in kilometres for the final circular orbit.
		/// </summary>
		[KRPCProperty]
		public double DesiredOrbitAltitude {
			get => EditableDouble.Get(this.desiredOrbitAltitude);
			set => EditableDouble.Set(this.desiredOrbitAltitude, value);
		}

		/// <summary>
		/// The target apoapsis in meters (PVG).
		/// </summary>
		[KRPCProperty]
		public double DesiredApoapsis {
			get => EditableDouble.Get(this.desiredApoapsis);
			set => EditableDouble.Set(this.desiredApoapsis, value);
		}

		/// <summary>
		/// The desired inclination in degrees for the final circular orbit.
		/// </summary>
		[KRPCProperty]
		public double DesiredInclination {
			get => EditableDouble.Get(this.desiredInclination);
			set => EditableDouble.Set(this.desiredInclination, value);
		}

		/// <summary>
		/// The target longitude of ascending node in degrees.
		/// </summary>
		[KRPCProperty]
		public double DesiredLan {
			get => EditableDouble.Get(this.desiredLan);
			set => EditableDouble.Set(this.desiredLan, value);
		}

		/// <summary>
		/// Whether to attach to the orbit at a specific altitude (PVG).
		/// </summary>
		[KRPCProperty]
		public bool AttachAltFlag {
			get => (bool)AscentSettings.attachAltFlag.GetValue(this.instance);
			set => AscentSettings.attachAltFlag.SetValue(this.instance, value);
		}

		/// <summary>
		/// The attach altitude when <see cref="AttachAltFlag" /> is enabled (PVG).
		/// </summary>
		[KRPCProperty]
		public double DesiredAttachAlt {
			get => EditableDouble.Get(this.desiredAttachAlt);
			set => EditableDouble.Set(this.desiredAttachAlt, value);
		}

		/// <summary>
		/// The fixed attach altitude (PVG).
		/// </summary>
		[KRPCProperty]
		public double DesiredAttachAltFixed {
			get => EditableDouble.Get(this.desiredAttachAltFixed);
			set => EditableDouble.Set(this.desiredAttachAltFixed, value);
		}

		/// <summary>
		/// The desired flight path angle at attach point (PVG).
		/// </summary>
		[KRPCProperty]
		public double DesiredFPA {
			get => EditableDouble.Get(this.desiredFPA);
			set => EditableDouble.Set(this.desiredFPA, value);
		}

		// ---------- Launch timing ----------

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
		/// When enabled, timed launch methods start immediately and skip the warp.
		/// </summary>
		[KRPCProperty]
		public bool OverrideWarpToPlane {
			get => (bool)AscentSettings.overrideWarpToPlane.GetValue(this.instance);
			set => AscentSettings.overrideWarpToPlane.SetValue(this.instance, value);
		}

		// ---------- Steering / general ----------

		/// <summary>
		/// Will cause the craft to steer based on the more accurate velocity vector rather than positional vector
		/// (large craft may actually perform better with this box unchecked).
		/// </summary>
		[KRPCProperty]
		public bool CorrectiveSteering {
			get => (bool)AscentSettings.correctiveSteering.GetValue(this.instance);
			set => AscentSettings.correctiveSteering.SetValue(this.instance, value);
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
			get => (bool)AscentSettings.forceRoll.GetValue(this.instance);
			set => AscentSettings.forceRoll.SetValue(this.instance, value);
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
		/// Altitude at which the roll transitions from vertical to turn roll.
		/// </summary>
		[KRPCProperty]
		public double RollAltitude {
			get => EditableDouble.Get(this.rollAltitude);
			set => EditableDouble.Set(this.rollAltitude, value);
		}

		/// <summary>
		/// Whether to deploy solar panels automatically when the ascent finishes.
		/// </summary>
		[KRPCProperty]
		public bool AutodeploySolarPanels {
			get => (bool)AscentSettings.autodeploySolarPanels.GetValue(this.instance);
			set => AscentSettings.autodeploySolarPanels.SetValue(this.instance, value);
		}

		/// <summary>
		/// Whether to deploy antennas automatically when the ascent finishes.
		/// </summary>
		[KRPCProperty]
		public bool AutoDeployAntennas {
			get => (bool)AscentSettings.autoDeployAntennas.GetValue(this.instance);
			set => AscentSettings.autoDeployAntennas.SetValue(this.instance, value);
		}

		/// <summary>
		/// Whether to skip circularization burn and do only the ascent.
		/// </summary>
		[KRPCProperty]
		public bool SkipCircularization {
			get => (bool)AscentSettings.skipCircularization.GetValue(this.instance);
			set => AscentSettings.skipCircularization.SetValue(this.instance, value);
		}

		/// <summary>
		/// The autopilot will automatically stage when the current stage has run out of fuel.
		/// Parameters can be set in <see cref="KRPC.MechJeb.StagingController" />.
		/// </summary>
		[KRPCProperty]
		public bool Autostage {
			get => (bool)AscentSettings.autostage.GetValue(this.instance, null);
			set => AscentSettings.autostage.SetValue(this.instance, value, null);
		}

		/// <remarks>Equivalent to <see cref="MechJeb.StagingController" />.</remarks>
		[KRPCProperty]
		public StagingController StagingController => MechJeb.StagingController;

		/// <remarks>Equivalent to <see cref="MechJeb.ThrustController" />.</remarks>
		[KRPCProperty]
		public ThrustController ThrustController => MechJeb.ThrustController;

		/// <summary>
		/// Whether to limit angle of attack.
		/// </summary>
		[KRPCProperty]
		public bool LimitAoA {
			get => (bool)AscentSettings.limitAoA.GetValue(this.instance);
			set => AscentSettings.limitAoA.SetValue(this.instance, value);
		}

		/// <summary>
		/// The maximal angle of attack used by the autopilot.
		/// </summary>
		/// <remarks><see cref="LimitAoA" /> needs to be enabled.</remarks>
		[KRPCProperty]
		public double MaxAoA {
			get => EditableDouble.Get(this.maxAoA);
			set => EditableDouble.Set(this.maxAoA, value);
		}

		/// <summary>
		/// The pressure value when AoA limit is automatically deactivated.
		/// </summary>
		/// <remarks><see cref="LimitAoA" /> needs to be enabled.</remarks>
		[KRPCProperty]
		public double AoALimitFadeoutPressure {
			get => EditableDouble.Get(this.aoALimitFadeoutPressure);
			set => EditableDouble.Set(this.aoALimitFadeoutPressure, value);
		}

		/// <summary>
		/// A runtime flag indicating the autopilot is actively limiting AoA.
		/// </summary>
		[KRPCProperty]
		public bool LimitingAoA => (bool)AscentSettings.limitingAoA.GetValue(this.instance);

		/// <summary>
		/// Whether to limit dynamic pressure × AoA (Q α).
		/// </summary>
		[KRPCProperty]
		public bool LimitQaEnabled {
			get => (bool)AscentSettings.limitQaEnabled.GetValue(this.instance);
			set => AscentSettings.limitQaEnabled.SetValue(this.instance, value);
		}

		/// <summary>
		/// The Q α limit when <see cref="LimitQaEnabled" /> is on.
		/// </summary>
		[KRPCProperty]
		public double LimitQa {
			get => EditableDouble.Get(this.limitQa);
			set => EditableDouble.Set(this.limitQa, value);
		}

		/// <summary>
		/// Current maximum angle of attack reported by the autopilot.
		/// </summary>
		[KRPCProperty]
		public double CurrentMaxAoA => (double)AscentBaseAutopilot.currentMaxAoA.GetValue(this.ActiveAutopilot);

		// ---------- Classic path ----------

		/// <summary>
		/// The turn starts when this altitude is reached (Classic).
		/// </summary>
		[KRPCProperty]
		public double TurnStartAltitude {
			get => EditableDouble.Get(this.turnStartAltitude);
			set => EditableDouble.Set(this.turnStartAltitude, value);
		}

		/// <summary>
		/// The turn starts when this velocity is reached (Classic).
		/// </summary>
		[KRPCProperty]
		public double TurnStartVelocity {
			get => EditableDouble.Get(this.turnStartVelocity);
			set => EditableDouble.Set(this.turnStartVelocity, value);
		}

		/// <summary>
		/// The turn ends when this altitude is reached (Classic).
		/// </summary>
		[KRPCProperty]
		public double TurnEndAltitude {
			get => EditableDouble.Get(this.turnEndAltitude);
			set => EditableDouble.Set(this.turnEndAltitude, value);
		}

		/// <summary>
		/// The final flight path angle (Classic).
		/// </summary>
		[KRPCProperty]
		public double TurnEndAngle {
			get => EditableDouble.Get(this.turnEndAngle);
			set => EditableDouble.Set(this.turnEndAngle, value);
		}

		/// <summary>
		/// A value between 0 and 1 describing how steep the turn is (Classic).
		/// </summary>
		[KRPCProperty]
		public double TurnShapeExponent {
			get => EditableDouble.Get(this.turnShapeExponent);
			set => EditableDouble.Set(this.turnShapeExponent, value);
		}

		/// <summary>
		/// Whether to enable automatic altitude turn (Classic).
		/// </summary>
		[KRPCProperty]
		public bool AutoPath {
			get => (bool)AscentSettings.autoPath.GetValue(this.instance);
			set => AscentSettings.autoPath.SetValue(this.instance, value);
		}

		/// <summary>
		/// A value between 0 and 1 (Classic).
		/// </summary>
		[KRPCProperty]
		public float AutoTurnPercent {
			get => (float)AscentSettings.autoTurnPerc.GetValue(this.instance);
			set => AscentSettings.autoTurnPerc.SetValue(this.instance, value);
		}

		/// <summary>
		/// A value between 0 and 1 (Classic).
		/// </summary>
		[KRPCProperty]
		public float AutoTurnSpeedFactor {
			get => (float)AscentSettings.autoTurnSpdFactor.GetValue(this.instance);
			set => AscentSettings.autoTurnSpdFactor.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double AutoTurnStartAltitude => (double)AscentSettings.autoTurnStartAltitude.GetValue(this.instance, null);

		[KRPCProperty]
		public double AutoTurnStartVelocity => (double)AscentSettings.autoTurnStartVelocity.GetValue(this.instance, null);

		[KRPCProperty]
		public double AutoTurnEndAltitude => (double)AscentSettings.autoTurnEndAltitude.GetValue(this.instance, null);

		// ---------- PVG path ----------

		[KRPCProperty]
		public double PitchStartVelocity {
			get => EditableDouble.Get(this.pitchStartVelocity);
			set => EditableDouble.Set(this.pitchStartVelocity, value);
		}

		[KRPCProperty]
		public double PitchRate {
			get => EditableDouble.Get(this.pitchRate);
			set => EditableDouble.Set(this.pitchRate, value);
		}

		[KRPCProperty]
		public double DynamicPressureTrigger {
			get => EditableDouble.Get(this.dynamicPressureTrigger);
			set => EditableDouble.Set(this.dynamicPressureTrigger, value);
		}

		[KRPCProperty]
		public int StagingTrigger {
			get => EditableInt.Get(this.stagingTrigger);
			set => EditableInt.Set(this.stagingTrigger, value);
		}

		[KRPCProperty]
		public bool StagingTriggerFlag {
			get => (bool)AscentSettings.stagingTriggerFlag.GetValue(this.instance);
			set => AscentSettings.stagingTriggerFlag.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public bool FixedCoast {
			get => (bool)AscentSettings.fixedCoast.GetValue(this.instance);
			set => AscentSettings.fixedCoast.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double FixedCoastLength {
			get => EditableDouble.Get(this.fixedCoastLength);
			set => EditableDouble.Set(this.fixedCoastLength, value);
		}

		[KRPCProperty]
		public bool CoastBeforeFlag {
			get => (bool)AscentSettings.coastBeforeFlag.GetValue(this.instance);
			set => AscentSettings.coastBeforeFlag.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double MinDeltaV {
			get => EditableDouble.Get(this.minDeltaV);
			set => EditableDouble.Set(this.minDeltaV, value);
		}

		[KRPCProperty]
		public int LastStage {
			get => EditableInt.Get(this.lastStage);
			set => EditableInt.Set(this.lastStage, value);
		}

		[KRPCProperty]
		public int OptimizeStage {
			get => (int)AscentSettings.optimizeStage.GetValue(this.instance, null);
			set => EditableInt.Set(this.optimizeStageInternal, value);
		}

		[KRPCProperty]
		public bool OptimizeStageFlag {
			get => (bool)AscentSettings.optimizeStageFlag.GetValue(this.instance);
			set => AscentSettings.optimizeStageFlag.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public int CoastStage {
			get => (int)AscentSettings.coastStage.GetValue(this.instance, null);
			set => EditableInt.Set(this.coastStageInternal, value);
		}

		[KRPCProperty]
		public bool CoastStageFlag {
			get => (bool)AscentSettings.coastStageFlag.GetValue(this.instance);
			set => AscentSettings.coastStageFlag.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public int SpinupStage {
			get => (int)AscentSettings.spinupStage.GetValue(this.instance, null);
			set => EditableInt.Set(this.spinupStageInternal, value);
		}

		[KRPCProperty]
		public bool SpinupStageFlag {
			get => (bool)AscentSettings.spinupStageFlag.GetValue(this.instance);
			set => AscentSettings.spinupStageFlag.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double SpinupAngularVelocity {
			get => EditableDouble.Get(this.spinupAngularVelocity);
			set => EditableDouble.Set(this.spinupAngularVelocity, value);
		}

		[KRPCProperty]
		public double SpinupLeadTime {
			get => EditableDouble.Get(this.spinupLeadTime);
			set => EditableDouble.Set(this.spinupLeadTime, value);
		}

		/// <summary>
		/// The list of stages that are unguided (PVG).
		/// </summary>
		[KRPCProperty]
		public IList<int> UnguidedStages => (List<int>)AscentSettings.unguidedStages.GetValue(this.instance, null);

		[KRPCProperty]
		public bool UnguidedStagesFlag {
			get => (bool)AscentSettings.unguidedStagesFlag.GetValue(this.instance);
			set => AscentSettings.unguidedStagesFlag.SetValue(this.instance, value);
		}

		// ---------- Timed launch ----------

		/// <summary>
		/// Whether the autopilot is in the middle of a timed countdown before an automated launch.
		/// </summary>
		[KRPCProperty]
		public bool TimedLaunch => (bool)AscentBaseAutopilot.timedLaunch.GetValue(this.ActiveAutopilot);

		/// <summary>
		/// Current autopilot launch mode. Use this to determine whether the autopilot is performing
		/// a timed launch and, if so, what type.
		/// </summary>
		[KRPCProperty]
		public AscentLaunchMode LaunchMode {
			get {
				object active = this.ActiveAutopilot;
				if(active == null || !(bool)AscentBaseAutopilot.timedLaunch.GetValue(active))
					return AscentLaunchMode.Normal;
				if((bool)AscentSettings.launchingToRendezvous.GetValue(this.instance))
					return AscentLaunchMode.Rendezvous;
				if((bool)AscentSettings.launchingToPlane.GetValue(this.instance))
					return AscentLaunchMode.TargetPlane;
				if((bool)AscentSettings.launchingToMatchLan.GetValue(this.instance))
					return AscentLaunchMode.MatchLan;
				if((bool)AscentSettings.launchingToLan.GetValue(this.instance))
					return AscentLaunchMode.Lan;
				return AscentLaunchMode.Unknown;
			}
		}

		/// <summary>
		/// Abort an ongoing timed launch before it has started.
		/// </summary>
		[KRPCMethod]
		public void AbortTimedLaunch() {
			if(this.LaunchMode == AscentLaunchMode.Unknown)
				throw new InvalidOperationException("There is an unknown timed launch ongoing which can't be aborted");

			AscentSettings.launchingToPlane.SetValue(this.instance, false);
			AscentSettings.launchingToRendezvous.SetValue(this.instance, false);
			AscentSettings.launchingToMatchLan.SetValue(this.instance, false);
			AscentSettings.launchingToLan.SetValue(this.instance, false);

			object active = this.ActiveAutopilot;
			if(active != null)
				AscentBaseAutopilot.timedLaunch.SetValue(active, false);
		}

		private void StartCountdown(double timeOffset) {
			AscentBaseAutopilot.startCountdown.Invoke(this.ActiveAutopilot, new object[] { MechJeb.vesselState.Time + timeOffset });
		}

		/// <summary>
		/// Launch to rendezvous with the selected target. Only supported in Classic ascent mode.
		/// </summary>
		[KRPCMethod]
		public void LaunchToRendezvous() {
			if(!MechJeb.TargetController.NormalTargetExists)
				throw new InvalidOperationException("Invalid target");
			if(this.AscentType != 0)
				throw new InvalidOperationException("Rendezvous is only supported in Classic ascent mode");

			this.AbortTimedLaunch();
			try {
				AscentSettings.launchingToRendezvous.SetValue(this.instance, true);
				this.StartCountdown(LaunchTiming.TimeToPhaseAngle(this.LaunchPhaseAngle));
			}
			catch(Exception) {
				this.AbortTimedLaunch();
				throw;
			}
		}

		/// <summary>
		/// Launch into the plane of the selected target.
		/// </summary>
		[KRPCMethod]
		public void LaunchToTargetPlane() {
			if(!MechJeb.TargetController.NormalTargetExists)
				throw new InvalidOperationException("Invalid target");

			this.AbortTimedLaunch();
			try {
				Orbit target = MechJeb.TargetController.InternalTargetOrbit;
				AscentSettings.launchingToPlane.SetValue(this.instance, true);

				Tuple<double, double> item = MathFunctions.MinimumTimeToPlane(target.LAN - this.LaunchLANDifference, target.inclination);
				this.StartCountdown(item.Item1);
				this.DesiredInclination = item.Item2;
			}
			catch(Exception) {
				this.AbortTimedLaunch();
				throw;
			}
		}

		/// <summary>
		/// Launch to match the selected target's longitude of ascending node exactly.
		/// </summary>
		[KRPCMethod]
		public void LaunchToMatchLan() {
			if(!MechJeb.TargetController.NormalTargetExists)
				throw new InvalidOperationException("Invalid target");

			this.AbortTimedLaunch();
			try {
				Orbit target = MechJeb.TargetController.InternalTargetOrbit;
				AscentSettings.launchingToMatchLan.SetValue(this.instance, true);

				Tuple<double, double> item = MathFunctions.MinimumTimeToPlane(target.LAN, target.inclination);
				this.StartCountdown(item.Item1);
				this.DesiredInclination = item.Item2;
			}
			catch(Exception) {
				this.AbortTimedLaunch();
				throw;
			}
		}

		/// <summary>
		/// Launch to a specific longitude of ascending node using the current <see cref="DesiredInclination" />.
		/// </summary>
		[KRPCMethod]
		public void LaunchToLan(double lan) {
			this.AbortTimedLaunch();
			try {
				this.DesiredLan = lan;
				AscentSettings.launchingToLan.SetValue(this.instance, true);
				double time = MathFunctions.TimeToPlane(lan, this.DesiredInclination);
				this.StartCountdown(time);
			}
			catch(Exception) {
				this.AbortTimedLaunch();
				throw;
			}
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
			/// The autopilot is performing a timed launch to match the target's longitude of ascending node.
			/// </summary>
			MatchLan,

			/// <summary>
			/// The autopilot is performing a timed launch to a manually specified longitude of ascending node.
			/// </summary>
			Lan,

			/// <summary>
			/// The autopilot is performing an unknown timed launch.
			/// </summary>
			Unknown = 99
		}
	}
}

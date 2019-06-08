using System.Reflection;

using KRPC.MechJeb.Util;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// The Smart A.S.S. module provides aids for vessel pitch control.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class SmartASS : DisplayModule {
		private readonly FieldInfo mode;
		private readonly FieldInfo target;

		private readonly FieldInfo forceRol;
		private readonly FieldInfo forcePitch;
		private readonly FieldInfo forceYaw;

		private readonly FieldInfo srfHdg;
		private readonly FieldInfo srfPit;
		private readonly FieldInfo srfRol;

		private readonly FieldInfo srfVelYaw;
		private readonly FieldInfo srfVelPit;
		private readonly FieldInfo srfVelRol;

		private readonly FieldInfo advReference;
		private readonly FieldInfo advDirection;

		private readonly MethodInfo engage;

		public SmartASS() : base("SmartASS") {
			this.mode = this.type.GetField("mode");
			this.target = this.type.GetField("target");

			this.forceRol = this.type.GetField("forceRol");
			this.forcePitch = this.type.GetField("forcePitch");
			this.forceYaw = this.type.GetField("forceYaw");

			this.srfHdg = this.type.GetField("srfHdg");
			this.srfPit = this.type.GetField("srfPit");
			this.srfRol = this.type.GetField("srfRol");

			this.srfVelYaw = this.type.GetField("srfVelYaw");
			this.srfVelPit = this.type.GetField("srfVelPit");
			this.srfVelRol = this.type.GetField("srfVelRol");

			this.advReference = this.type.GetField("advReference");
			this.advDirection = this.type.GetField("advDirection");

			this.engage = this.type.GetMethod("Engage");
		}

		/// <summary>
		/// GUI mode; doesn't do anything except changing SmartASS GUI buttons to a specified mode.
		/// </summary>
		[KRPCProperty]
		public SmartASSInterfaceMode InterfaceMode {
			get => (SmartASSInterfaceMode)this.mode.GetValue(this.instance);
			set {
				if(value == SmartASSInterfaceMode.Automatic)
					throw new MJServiceException("Cannot set SmartASSInterfaceMode to Automatic");

				this.mode.SetValue(this.instance, (int)value);
			}
		}

		/// <summary>
		/// Current autopilot mode.
		/// </summary>
		[KRPCProperty]
		public SmartASSAutopilotMode AutopilotMode {
			get => (SmartASSAutopilotMode)this.target.GetValue(this.instance);
			set {
				if(value == SmartASSAutopilotMode.Automatic)
					throw new MJServiceException("Cannot set SmartASSAutopilotMode to Automatic");

				this.target.SetValue(this.instance, (int)value);
			}
		}

		/// <summary>
		/// Enable yaw control for <see cref="SmartASS.SurfaceHeading" />, <see cref="SmartASSAutopilotMode.SurfacePrograde" /> and <see cref="SmartASSAutopilotMode.SurfaceRetrograde" />.
		/// </summary>
		[KRPCProperty]
		public bool ForceYaw {
			get => (bool)this.forceYaw.GetValue(this.instance);
			set => this.forceYaw.SetValue(this.instance, value);
		}

		/// <summary>
		/// Enable pitch control for <see cref="SmartASS.SurfacePitch" />, <see cref="SmartASSAutopilotMode.SurfacePrograde" /> and <see cref="SmartASSAutopilotMode.SurfaceRetrograde" />.
		/// </summary>
		[KRPCProperty]
		public bool ForcePitch {
			get => (bool)this.forcePitch.GetValue(this.instance);
			set => this.forcePitch.SetValue(this.instance, value);
		}

		/// <summary>
		/// Enable roll control.
		/// </summary>
		[KRPCProperty]
		public bool ForceRoll {
			get => (bool)this.forceRol.GetValue(this.instance);
			set => this.forceRol.SetValue(this.instance, value);
		}

		/// <summary>
		/// Heading; Also called or azimuth, or the direction where you want to go.
		/// </summary>
		/// <remarks>Works only in <see cref="SmartASSAutopilotMode.Surface" /> mode.</remarks>
		[KRPCProperty]
		public double SurfaceHeading {
			get => EditableVariables.GetDouble(this.srfHdg, this.instance);
			set => EditableVariables.SetDouble(this.srfHdg, this.instance, value);
		}

		/// <summary>
		/// Pitch or inclination; 0 is horizontal and 90 is straight up. Can be negative.
		/// </summary>
		/// <remarks>Works only in <see cref="SmartASSAutopilotMode.Surface" /> mode.</remarks>
		[KRPCProperty]
		public double SurfacePitch {
			get => EditableVariables.GetDouble(this.srfPit, this.instance);
			set => EditableVariables.SetDouble(this.srfPit, this.instance, value);
		}

		/// <summary>
		/// Roll; 0 is top side up.
		/// </summary>
		/// <remarks>Works only in <see cref="SmartASSAutopilotMode.Surface" /> mode.</remarks>
		[KRPCProperty]
		public double SurfaceRoll {
			get => EditableVariables.GetDouble(this.srfRol, this.instance);
			set => EditableVariables.SetDouble(this.srfRol, this.instance, value);
		}
		
		/// <remarks>Works only in <see cref="SmartASSAutopilotMode.SurfacePrograde" /> and <see cref="SmartASSAutopilotMode.SurfaceRetrograde" /> mode.</remarks>
		[KRPCProperty]
		public double SurfaceVelYaw {
			get => EditableVariables.GetDouble(this.srfVelYaw, this.instance);
			set => EditableVariables.SetDouble(this.srfVelYaw, this.instance, value);
		}

		/// <summary>
		/// Pitch or inclination; 0 is horizontal and 90 is straight up. Can be negative.
		/// </summary>
		/// <remarks>Works only in <see cref="SmartASSAutopilotMode.SurfacePrograde" /> and <see cref="SmartASSAutopilotMode.SurfaceRetrograde" /> mode.</remarks>
		[KRPCProperty]
		public double SurfaceVelPitch {
			get => EditableVariables.GetDouble(this.srfPit, this.instance);
			set => EditableVariables.SetDouble(this.srfPit, this.instance, value);
		}

		/// <summary>
		/// Roll; 0 is top side up.
		/// </summary>
		/// <remarks>Works only in <see cref="SmartASSAutopilotMode.SurfacePrograde" /> and <see cref="SmartASSAutopilotMode.SurfaceRetrograde" /> mode.</remarks>
		[KRPCProperty]
		public double SurfaceVelRoll {
			get => EditableVariables.GetDouble(this.srfRol, this.instance);
			set => EditableVariables.SetDouble(this.srfRol, this.instance, value);
		}

		/// <remarks>Works only in <see cref="SmartASSAutopilotMode.Advanced" /> mode.</remarks>
		[KRPCProperty]
		public AttitudeReference AdvancedReference {
			get => (AttitudeReference)this.advReference.GetValue(this.instance);
			set => this.advReference.SetValue(this.instance, (int)value);
		}

		/// <remarks>Works only in <see cref="SmartASSAutopilotMode.Advanced" /> mode.</remarks>
		[KRPCProperty]
		public Vector6.Direction AdvancedDirection {
			get => (Vector6.Direction)this.advDirection.GetValue(this.instance);
			set => this.advDirection.SetValue(this.instance, (int)value);
		}

		/// <summary>
		/// Update SmartASS position to use new values.
		/// </summary>
		/// <param name="resetPID">False most of the time, use true only if it doesn't work.</param>
		[KRPCMethod]
		public void Update(bool resetPID) {
			this.engage.Invoke(this.instance, new object[] { resetPID });
		}
		
		[KRPCEnum(Service = "MechJeb")]
		public enum SmartASSInterfaceMode {
			Orbital,
			Surface,
			Target,
			Advanced,

			/// <summary>
			/// Internal mode, do not set.
			/// </summary>
			Automatic
		}

		[KRPCEnum(Service = "MechJeb")]
		public enum SmartASSAutopilotMode {
			/// <summary>
			/// Switch off Smart A.S.S.
			/// </summary>
			Off,

			/// <summary>
			/// "Kill" the spacecraft's rotation (counters rotation/tumbling).
			/// </summary>
			KillRot,

			/// <summary>
			/// Point the vessel to a maneuver node.
			/// </summary>
			Node,

			/// <summary>
			/// SURFACE: Orient the vessel in specific direction relative to surface.
			/// </summary>
			Surface,

			/// <summary>
			/// ORBIT: Orient to orbital prograde.
			/// </summary>
			Prograde,

			/// <summary>
			/// ORBIT: Orient to orbital retrograde.
			/// </summary>
			Retrograde,

			/// <summary>
			/// ORBIT: Orient to orbital normal (change inclination).
			/// </summary>
			NormalPlus,

			/// <summary>
			/// ORBIT: Orient to orbital anti-normal (change inclination).
			/// </summary>
			NormalMinus,

			/// <summary>
			/// ORBIT: Orient to radial outward (away from SOI).
			/// </summary>
			RadialPlus,

			/// <summary>
			/// ORBIT: Orient to radial inward (towards SOI).
			/// </summary>
			RadialMinus,

			/// <summary>
			/// TARGET: Orient toward your relative velocity. Burning this direction will increase your relative velocity.
			/// </summary>
			RelativePlus,

			/// <summary>
			/// TARGET: Orient away from your relative velocity. Burning this direction will decrease your relative velocity.
			/// </summary>
			RelativeMinus,

			/// <summary>
			/// TARGET: Orient towards the target.
			/// </summary>
			TargetPlus,

			/// <summary>
			/// TARGET: Orient away from the target.
			/// </summary>
			TargetMinus,

			/// <summary>
			/// TARGET: Orient parallel to the target's orientation. If the target is a docking node it orients the ship along the docking axis, pointing away from the node.
			/// </summary>
			ParallelPlus,

			/// <summary>
			/// TARGET: Orient antiparallel to the target's orientation. If the target is a docking node it orients the ship along the docking axis, pointing toward the node.
			/// </summary>
			ParallelMinus,

			/// <summary>
			/// Advanced mode.
			/// </summary>
			Advanced,

			/// <summary>
			/// Automatic mode (internal mode, only for getting status).
			/// </summary>
			Automatic,

			/// <summary>
			/// SURFACE: Orient in the direction of movement relative to the ground. Useful during lift-off for rockets which don't have fins or are otherwise instable.
			/// </summary>
			SurfacePrograde,

			/// <summary>
			/// SURFACE: Orient in the opposite direction of movement relative to the ground. Useful during reentry or aerobraking with an aerodynamically unstable craft.
			/// </summary>
			SurfaceRetrograde,

			/// <summary>
			/// SURFACE: Orient in the direction of horizontal movement relative to the ground.
			/// </summary>
			HorizontalPlus,

			/// <summary>
			/// SURFACE: Orient in the opposite direction of horizontal movement relative to the ground.
			/// </summary>
			HorizontalMinus,

			/// <summary>
			/// SURFACE: Orient "up", perpendicular to the surface.
			/// </summary>
			VerticalPlus
		}

		[KRPCEnum(Service = "MechJeb")]
		public enum AttitudeReference {
			/// <summary>
			/// World coordinate system.
			/// </summary>
			Inertial,

			/// <summary>
			/// forward = prograde, left = normal plus, up = radial plus
			/// </summary>
			Orbit,

			/// <summary>
			/// forward = surface projection of orbit velocity, up = surface normal
			/// </summary>
			OrbitHorizontal,

			/// <summary>
			/// forward = north, left = west, up = surface normal
			/// </summary>
			SurfaceNorth,

			/// <summary>
			/// forward = surface frame vessel velocity, up = perpendicular component of surface normal
			/// </summary>
			SurfaceVelocity,

			/// <summary>
			/// forward = toward target, up = perpendicular component of vessel heading
			/// </summary>
			Target,

			/// <summary>
			/// forward = toward relative velocity direction, up = tbd
			/// </summary>
			RelativeVelocity,

			/// <summary>
			/// forward = direction target is facing, up = target up
			/// </summary>
			TargetOrientation,

			/// <summary>
			/// forward = next maneuver node direction, up = tbd
			/// </summary>
			ManeuverNode,

			/// <summary>
			/// forward = orbit velocity of the parent body orbiting the sun, up = radial plus of that orbit
			/// </summary>
			Sun,

			/// <summary>
			/// forward = surface velocity horizontal component, up = surface normal
			/// </summary>
			SurfaceHorizontal
		}
	}
}

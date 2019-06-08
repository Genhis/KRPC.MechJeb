using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// The Translatron module controls the vessel's throttle/velocity.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class Translatron : DisplayModule {
		private readonly object thrustInstance;
		private readonly FieldInfo transSpdAct;
		private readonly FieldInfo transKillH;
		private readonly FieldInfo tMode;

		private readonly FieldInfo transSpd;

		private readonly MethodInfo setMode;
		private readonly MethodInfo panicSwitch;

		public Translatron() : base("Translatron") {
			this.thrustInstance = MechJeb.ThrustController.instance;
			var thrustType = MechJeb.ThrustController.type;

			this.tMode = thrustType.GetField("tmode");
			this.transSpdAct = thrustType.GetField("trans_spd_act");
			this.transKillH = thrustType.GetField("trans_kill_h");

			this.transSpd = this.type.GetField("trans_spd");

			this.setMode = this.type.GetMethod("SetMode");
			this.panicSwitch = this.type.GetMethod("PanicSwitch");
		}

		/// <summary>
		/// Speed which trasnlatron will hold
		/// </summary>
		[KRPCProperty]
		public double TranslationSpeed {
			get => EditableVariables.GetDouble(this.transSpd, this.instance);
			set {
				EditableVariables.SetDouble(this.transSpd, this.instance, value);
				this.transSpdAct.SetValue(this.thrustInstance, (float)value);
			}
		}

		/// <summary>
		/// Kill horizontal speed
		/// </summary>
		[KRPCProperty]
		public bool KillHorizontalSpeed {
			get => (bool)this.transKillH.GetValue(this.thrustInstance);
			set => this.transKillH.SetValue(this.thrustInstance, value);
		}

		/// <summary>
		/// Sets translatron mode
		/// </summary>
		[KRPCProperty]
		public TranslatronMode Mode {
			get => (TranslatronMode)this.tMode.GetValue(this.thrustInstance);
			set {
				if(value == TranslatronMode.KeepRelative || value == TranslatronMode.Direct)
					throw new MJServiceException("Cannot set TranslatronMode to internal values");

				this.setMode.Invoke(this.instance, new object[] { (int)value });
			}
		}

		/// <summary>
		/// Abort mission by seperating all but the last stage and activating landing autopilot.
		/// </summary>
		[KRPCMethod]
		public void PanicSwitch() {
			this.panicSwitch.Invoke(this.instance, null);
		}

		[KRPCEnum(Service = "MechJeb")]
		public enum TranslatronMode {
			/// <summary>
			/// Switch off Translatron.
			/// </summary>
			Off,

			/// <summary>
			/// Keep orbital velocity.
			/// </summary>
			KeepOrbital,

			/// <summary>
			/// Keep surface velocity.
			/// </summary>
			KeepSurface,

			/// <summary>
			/// Keep vertical velocity (climb/descent speed).
			/// </summary>
			KeepVertical,

			/// <summary>
			/// Internal mode, do not set.
			/// </summary>
			KeepRelative,

			/// <summary>
			/// Internal mode, do not set.
			/// </summary>
			Direct
		}
	}
}

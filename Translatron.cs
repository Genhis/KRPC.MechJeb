using KRPC.Service.Attributes;
using System.Reflection;

namespace KRPC.MechJeb
{
    /// <summary>
    /// The Translatron module controls the vessel's throttle/velocity.
    /// </summary>
    [KRPCClass(Service = "MechJeb")]
    public class Translatron : DisplayModule
    {
        private readonly object thrustInstance;
        private readonly FieldInfo trans_spd_act;
        private readonly FieldInfo trans_kill_h;

        private readonly FieldInfo trans_spd;

        private readonly MethodInfo setMode;
        private readonly MethodInfo panicSwitch;

        public Translatron() : base("Translatron")
        {
            var core = this.type.GetField("core").GetValue(this.instance);
            this.thrustInstance = core.GetType().GetField("thrust").GetValue(core);
            var thrustType = this.thrustInstance.GetType();

            this.trans_spd_act = thrustType.GetField("trans_spd_act");
            this.trans_kill_h = thrustType.GetField("trans_kill_h");

            this.trans_spd = this.type.GetField("trans_spd");

            this.setMode = this.type.GetMethod("SetMode");
            this.panicSwitch = this.type.GetMethod("PanicSwitch");
        }

        /// <summary>
        /// Speed which trasnlatron will hold
        /// </summary>
        [KRPCProperty]
        public double TranslationSpeed
        {
            get => EditableVariables.GetDouble(this.trans_spd, this.instance);
            set => EditableVariables.SetDouble(this.trans_spd, this.instance, value);
        }

        /// <summary>
        /// Kill horizontal speed
        /// </summary>
        [KRPCProperty]
        public bool KillHs
        {
            get => (bool)this.trans_kill_h.GetValue(this.thrustInstance);
            set => this.trans_kill_h.SetValue(this.thrustInstance, (bool)value);
        }

        /// <summary>
        /// Sets translatron mode
        /// </summary>
        /// <param name="tmode">TranslatronMode you want to use <see cref="TranslatronMode"/></param>
        [KRPCMethod]
        public void SetMode(TranslatronMode tmode)
        {
            this.setMode.Invoke(this.instance, new object[] { (int)tmode });
        }

        /// <summary>
        /// Abort mission by seperating all but the last stage and activating landing autopilot.
        /// </summary>
        [KRPCMethod]
        public void PanicSwitch()
        {
            this.panicSwitch.Invoke(this.instance, null);
        }

        /// <summary>
        /// Execute the selected action.
        /// </summary>
        [KRPCMethod]
        public void Execute()
        {
            this.trans_spd_act.SetValue(this.thrustInstance, (float)TranslationSpeed);
        }

        [KRPCEnum(Service = "MechJeb")]
        public enum TranslatronMode
        {
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

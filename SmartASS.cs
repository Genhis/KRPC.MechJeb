using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb
{
    [KRPCClass(Service = "MechJeb")]
    public class SmartASS : DisplayModule
    {
        private readonly FieldInfo mode;
        private readonly FieldInfo target;
        private readonly object srfHdg;
        private readonly object srfPit;
        private readonly object srfRol;
        private readonly object srfVelYaw;
        private readonly object srfVelPit;
        private readonly object srfVelRol;
        private readonly object rol;
        private readonly FieldInfo advReference;
        private readonly FieldInfo advDirection;
        private readonly FieldInfo forceRol;
        private readonly FieldInfo forcePitch;
        private readonly FieldInfo forceYaw;
        private readonly FieldInfo autoDisableSmartASS;

        private readonly MethodInfo engage;

        public SmartASS() : base("SmartASS")
        {
            this.mode = this.type.GetField("mode");
            this.target = this.type.GetField("target");
            this.srfHdg = this.type.GetField("srfHdg").GetValue(this.instance);
            this.srfPit = this.type.GetField("srfPit").GetValue(this.instance);
            this.srfRol = this.type.GetField("srfRol").GetValue(this.instance);
            this.srfVelYaw = this.type.GetField("srfVelYaw").GetValue(this.instance);
            this.srfVelPit = this.type.GetField("srfVelPit").GetValue(this.instance);
            this.srfVelRol = this.type.GetField("srfVelRol").GetValue(this.instance);
            this.rol = this.type.GetField("rol").GetValue(this.instance);
            this.advReference = this.type.GetField("advReference");
            this.advDirection = this.type.GetField("advDirection");
            this.forceRol = this.type.GetField("forceRol");
            this.forcePitch = this.type.GetField("forcePitch");
            this.forceYaw = this.type.GetField("forceYaw");
            this.autoDisableSmartASS = this.type.GetField("autoDisableSmartASS");

            this.engage = this.type.GetMethod("Engage");
        }

        [KRPCProperty]
        public SmartAssMode Mode
        {
            get => (SmartAssMode)this.mode.GetValue(this.instance);
            set => this.mode.SetValue(this.instance, value);
        }

        [KRPCProperty]
        public SmartAssTarget Target
        {
            get => (SmartAssTarget)this.mode.GetValue(this.instance);
            set => this.mode.SetValue(this.instance, value);
        }

        [KRPCProperty]
        public double SurfaceHeading
        {
            get => EditableVariables.GetDouble(this.srfHdg);
            set => EditableVariables.SetDouble(this.srfHdg, value);
        }

        [KRPCProperty]
        public double SurfacePitch
        {
            get => EditableVariables.GetDouble(this.srfPit);
            set => EditableVariables.SetDouble(this.srfPit, value);
        }

        [KRPCProperty]
        public double SurfaceRoll
        {
            get => EditableVariables.GetDouble(this.srfRol);
            set => EditableVariables.SetDouble(this.srfRol, value);
        }

        [KRPCProperty]
        public double SurfaceVelYaw
        {
            get => EditableVariables.GetDouble(this.srfVelYaw);
            set => EditableVariables.SetDouble(this.srfVelYaw, value);
        }

        [KRPCProperty]
        public double SurfaceVelPitch
        {
            get => EditableVariables.GetDouble(this.srfVelPit);
            set => EditableVariables.SetDouble(this.srfVelPit, value);
        }

        [KRPCProperty]
        public double SurfaceVelRoll
        {
            get => EditableVariables.GetDouble(this.srfVelRol);
            set => EditableVariables.SetDouble(this.srfVelRol, value);
        }

        [KRPCProperty]
        public double Roll
        {
            get => EditableVariables.GetDouble(this.rol);
            set => EditableVariables.SetDouble(this.rol, value);
        }

        [KRPCProperty]
        public bool ForceRoll
        {
            get => (bool)this.forceRol.GetValue(this.instance);
            set => this.forceRol.SetValue(this.instance, value);
        }

        [KRPCProperty]
        public bool ForcePitch
        {
            get => (bool)this.forcePitch.GetValue(this.instance);
            set => this.forcePitch.SetValue(this.instance, value);
        }

        [KRPCProperty]
        public bool ForceYaw
        {
            get => (bool)this.forceYaw.GetValue(this.instance);
            set => this.forceYaw.SetValue(this.instance, value);
        }

        [KRPCProperty]
        public bool AutoDisableSmartASS
        {
            get => (bool)this.autoDisableSmartASS.GetValue(this.instance);
            set => this.autoDisableSmartASS.SetValue(this.instance, value);
        }

        [KRPCMethod]
        public void Engage(bool resetPID = true)
        {
            this.engage.Invoke(this.instance, new object[] { resetPID });
        }


        [KRPCEnum(Service="MechJeb")]
        public enum SmartAssMode
        {
            ORBITAL,
            SURFACE,
            TARGET,
            ADVANCED,
            AUTO
        }

        [KRPCEnum(Service = "MechJeb")]
        public enum SmartAssTarget
        {
            OFF,
            KILLROT,
            NODE,
            SURFACE,
            PROGRADE,
            RETROGRADE,
            NORMAL_PLUS,
            NORMAL_MINUS,
            RADIAL_PLUS,
            RADIAL_MINUS,
            RELATIVE_PLUS,
            RELATIVE_MINUS,
            TARGET_PLUS,
            TARGET_MINUS,
            PARALLEL_PLUS,
            PARALLEL_MINUS,
            ADVANCED,
            AUTO,
            SURFACE_PROGRADE,
            SURFACE_RETROGRADE,
            HORIZONTAL_PLUS,
            HORIZONTAL_MINUS,
            VERTICAL_PLUS
        }
    }
}

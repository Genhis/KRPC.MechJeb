using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class AscentPEG : AscentBase {
		private readonly object pitchStartTime;
		private readonly object pitchRate;
		private readonly object pitchEndTime;
		private readonly object desiredApoapsis;
		private readonly object terminalGuidanceSecs;
		private readonly object stageLowDVLimit;
		private readonly object editNumStages;

		public AscentPEG() : base("AscentPEG") {
			this.pitchStartTime = this.type.GetField("pitchStartTime").GetValue(this.instance);
			this.pitchRate = this.type.GetField("pitchRate").GetValue(this.instance);
			this.pitchEndTime = this.type.GetField("pitchEndTime").GetValue(this.instance);
			this.desiredApoapsis = this.type.GetField("desiredApoapsis").GetValue(this.instance);
			this.terminalGuidanceSecs = this.type.GetField("terminalGuidanceSecs").GetValue(this.instance);
			this.stageLowDVLimit = this.type.GetField("stageLowDVLimit").GetValue(this.instance);
			this.editNumStages = this.type.GetField("edit_num_stages").GetValue(this.instance);
		}

		[KRPCProperty]
		public double PitchStartTime {
			get => EditableVariables.GetDouble(this.pitchStartTime);
			set => EditableVariables.SetDouble(this.pitchStartTime, value);
		}

		[KRPCProperty]
		public double PitchRate {
			get => EditableVariables.GetDouble(this.pitchRate);
			set => EditableVariables.SetDouble(this.pitchRate, value);
		}

		[KRPCProperty]
		public double PitchEndTime {
			get => EditableVariables.GetDouble(this.pitchEndTime);
			set => EditableVariables.SetDouble(this.pitchEndTime, value);
		}

		[KRPCProperty]
		public double DesiredApoapsis {
			get => EditableVariables.GetDouble(this.desiredApoapsis);
			set => EditableVariables.SetDouble(this.desiredApoapsis, value);
		}

		[KRPCProperty]
		public double TerminalGuidanceSecs {
			get => EditableVariables.GetDouble(this.terminalGuidanceSecs);
			set => EditableVariables.SetDouble(this.terminalGuidanceSecs, value);
		}

		[KRPCProperty]
		public double StageLowDVLimit {
			get => EditableVariables.GetDouble(this.stageLowDVLimit);
			set => EditableVariables.SetDouble(this.stageLowDVLimit, value);
		}

		[KRPCProperty]
		public int EditNumStages {
			get => EditableVariables.GetInt(this.editNumStages);
			set => EditableVariables.SetInt(this.editNumStages, value);
		}
	}
}

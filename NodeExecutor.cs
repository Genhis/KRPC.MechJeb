using System.Reflection;

using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class NodeExecutor : ComputerModule {
		private readonly FieldInfo autowarp;
		private readonly object leadTime;
		private readonly object tolerance;

		private readonly MethodInfo executeOneNode;
		private readonly MethodInfo executeAllNodes;
		private readonly MethodInfo abort;

		public NodeExecutor() : base("NodeExecutor") {
			this.autowarp = this.type.GetField("autowarp");
			this.leadTime = this.type.GetField("leadTime").GetValue(this.instance);
			this.tolerance = this.type.GetField("tolerance").GetValue(this.instance);

			this.executeOneNode = this.type.GetMethod("ExecuteOneNode");
			this.executeAllNodes = this.type.GetMethod("ExecuteAllNodes");
			this.abort = this.type.GetMethod("Abort");
		}

		[KRPCProperty]
		public bool Autowarp {
			get => (bool)this.autowarp.GetValue(this.instance);
			set => this.autowarp.SetValue(this.instance, value);
		}

		[KRPCProperty]
		public double LeadTime {
			get => EditableVariables.GetDouble(this.leadTime);
			set => EditableVariables.SetDouble(this.leadTime, value);
		}

		[KRPCProperty]
		public double Tolerance {
			get => EditableVariables.GetDouble(this.tolerance);
			set => EditableVariables.SetDouble(this.tolerance, value);
		}

		[KRPCMethod]
		public void ExecuteOneNode() {
			this.executeOneNode.Invoke(this.instance, new object[] { this });
		}

		[KRPCMethod]
		public void ExecuteAllNodes() {
			this.executeAllNodes.Invoke(this.instance, new object[] { this });
		}

		[KRPCMethod]
		public void Abort() {
			this.abort.Invoke(this.instance, null);
		}

		[KRPCMethod]
		public override void DisableAll() {
			this.Autowarp = false;
		}
	}
}

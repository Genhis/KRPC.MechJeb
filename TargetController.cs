using System.Reflection;

using KRPC.Service.Attributes;
using KRPC.SpaceCenter.ExtensionMethods;

using UnityEngine;

using Tuple3 = KRPC.Utils.Tuple<double, double, double>;

namespace KRPC.MechJeb {
	[KRPCClass(Service = "MechJeb")]
	public class TargetController : ComputerModule {
		private readonly MethodInfo setPositionTarget;
		private readonly MethodInfo getPositionTargetString;
		private readonly MethodInfo getPositionTargetPosition;
		private readonly MethodInfo setDirectionTarget;
		private readonly MethodInfo pickPositionTargetOnMap;
		private readonly MethodInfo updateDirectionTarget;

		private readonly PropertyInfo normalTargetExists;
		private readonly PropertyInfo positionTargetExists;
		private readonly PropertyInfo canAlign;
		//private readonly PropertyInfo target;
		private readonly PropertyInfo targetOrbit;
		private readonly PropertyInfo position;
		private readonly PropertyInfo distance;
		private readonly PropertyInfo relativeVelocity;
		private readonly PropertyInfo relativePosition;
		//private readonly PropertyInfo transform;
		private readonly PropertyInfo dockingAxis;

		public TargetController() : base("TargetController") {
			this.setPositionTarget = this.type.GetMethod("SetPositionTarget");
			this.getPositionTargetString = this.type.GetMethod("GetPositionTargetString");
			this.getPositionTargetPosition = this.type.GetMethod("GetPositionTargetPosition");
			this.setDirectionTarget = this.type.GetMethod("SetDirectionTarget");
			this.pickPositionTargetOnMap = this.type.GetMethod("PickPositionTargetOnMap");
			this.updateDirectionTarget = this.type.GetMethod("UpdateDirectionTarget");

			this.normalTargetExists = this.type.GetProperty("NormalTargetExists");
			this.positionTargetExists = this.type.GetProperty("PositionTargetExists");
			this.canAlign = this.type.GetProperty("CanAlign");
			//this.target = this.type.GetProperty("Target");
			this.targetOrbit = this.type.GetProperty("TargetOrbit");
			this.position = this.type.GetProperty("Position");
			this.distance = this.type.GetProperty("Distance");
			this.relativeVelocity = this.type.GetProperty("RelativeVelocity");
			this.relativePosition = this.type.GetProperty("RelativePosition");
			//this.transform = this.type.GetProperty("Transform");
			this.dockingAxis = this.type.GetProperty("DockingAxis");
		}

		[KRPCMethod]
		public void SetPositionTarget(SpaceCenter.Services.CelestialBody body, double latitude, double longitude) {
			this.setPositionTarget.Invoke(this.instance, new object[] { body.InternalBody, latitude, longitude });
		}

		[KRPCMethod]
		public string GetPositionTargetString() {
			return this.getPositionTargetString.Invoke(this.instance, null).ToString();
		}

		[KRPCMethod]
		public Tuple3 GetPositionTargetPosition() {
			return ((Vector3d)this.getPositionTargetPosition.Invoke(this.instance, null)).ToTuple();
		}

		[KRPCMethod]
		public void SetDirectionTarget(string name) {
			this.setDirectionTarget.Invoke(this.instance, new object[] { name });
		}

		[KRPCMethod]
		public void PickPositionTargetOnMap() {
			this.pickPositionTargetOnMap.Invoke(this.instance, null);
		}

		[KRPCMethod]
		public void UpdateDirectionTarget(Tuple3 direction) {
			this.updateDirectionTarget.Invoke(this.instance, new object[] { direction.ToVector() });
		}

		[KRPCProperty]
		public bool NormalTargetExists => (bool)this.normalTargetExists.GetValue(this.instance, null);

		[KRPCProperty]
		public bool PositionTargetExists => (bool)this.positionTargetExists.GetValue(this.instance, null);

		[KRPCProperty]
		public bool CanAlign => (bool)this.canAlign.GetValue(this.instance, null);

		[KRPCProperty]
		public SpaceCenter.Services.Orbit TargetOrbit => new SpaceCenter.Services.Orbit((Orbit)this.targetOrbit.GetValue(this.instance, null));

		[KRPCProperty]
		public Tuple3 Position => ((Vector3)this.position.GetValue(this.instance, null)).ToTuple();

		[KRPCProperty]
		public float Distance => (float)this.distance.GetValue(this.instance, null);

		[KRPCProperty]
		public Tuple3 RelativeVelocity => ((Vector3)this.relativeVelocity.GetValue(this.instance, null)).ToTuple();

		[KRPCProperty]
		public Tuple3 RelativePosition => ((Vector3)this.relativePosition.GetValue(this.instance, null)).ToTuple();

		[KRPCProperty]
		public Tuple3 DockingAxis => ((Vector3)this.dockingAxis.GetValue(this.instance, null)).ToTuple();
	}
}

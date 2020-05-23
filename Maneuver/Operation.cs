using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;
using KRPC.SpaceCenter.Services;

namespace KRPC.MechJeb.Maneuver {
	/// <summary>
	/// This exception is thrown when there is something wrong with the operation (e.g. the target is not set when the operation needs it).
	/// </summary>
	[KRPCException(Service = "MechJeb")]
	public class OperationException : Exception {
		internal const string MechJebType = "MuMech.OperationException";

		internal static Type type;

		public OperationException(string message) : base(message) { }

		internal static void InitType(Type t) {
			type = t;
		}
	}

	public abstract class Operation {
		internal const string MechJebType = "MuMech.Operation";

		// Fields and methods
		private static MethodInfo errorMessage;
		private static MethodInfo makeNodeImpl;

		// Instance objects
		protected internal object instance;

		internal static void InitType(Type type) {
			errorMessage = type.GetCheckedMethod("getErrorMessage");
			makeNodeImpl = type.GetCheckedMethod("MakeNodeImpl");
		}

		protected internal virtual void InitInstance(object instance) {
			this.instance = instance;
		}

		/// <summary>
		/// A warning may be stored there during MakeNode() call.
		/// </summary>
		[KRPCProperty]
		public string ErrorMessage => (string)errorMessage.Invoke(this.instance, null);

		/// <summary>
		/// Create a new maneuver node.
		/// A warning may be stored in ErrorMessage during this process; so it may be useful to check its value.
		/// 
		/// OperationException is thrown when there is something wrong with the operation.
		/// MJServiceException - Internal service error.
		/// </summary>
		/// <returns></returns>
		[KRPCMethod]
		public Node MakeNode() {
			try {
				Vessel vessel = FlightGlobals.ActiveVessel;
				object param = makeNodeImpl.Invoke(this.instance, new object[] { vessel.orbit, Planetarium.GetUniversalTime(), MechJeb.TargetController.instance });
				//a warning may be stored in ErrorMessage property (if it's an error, we will throw an exception)

				vessel.RemoveAllManeuverNodes(); //this implementation supports only one active ManeuverNode; removing the others to prevent bugs
				return new Node(vessel, vessel.PlaceManeuverNode(vessel.orbit, (Vector3d)ManeuverParameters.dV.GetValue(param), (double)ManeuverParameters.uT.GetValue(param)));
			}
			catch(Exception ex) {
				if(ex is TargetInvocationException)
					ex = ex.InnerException;

				if(ex.GetType() == OperationException.type)
					throw new OperationException(ex.Message);

				Logger.Severe("An error occured while creating a new ManeuverNode", ex);
				throw new MJServiceException(ex.Message);
			}
		}

		private static class ManeuverParameters {
			internal const string MechJebType = "MuMech.ManeuverParameters";

			// Fields and methods
			internal static FieldInfo dV;
			internal static FieldInfo uT;

			internal static void InitType(Type type) {
				dV = type.GetCheckedField("dV");
				uT = type.GetCheckedField("UT");
			}
		}
	}

	public abstract class TimedOperation : Operation {
		protected TimedOperation() {
			this.TimeSelector = new TimeSelector();
		}

		protected static FieldInfo GetTimeSelectorField(Type type) {
			// Need to do it this way because MechJeb does not have a separate TimedOperation class. Instead, the field is duplicated where needed.
			return type.GetCheckedField("timeSelector", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		protected void InitTimeSelector(FieldInfo timeSelector) {
			this.TimeSelector.InitInstance(timeSelector.GetInstanceValue(this.instance));
		}

		[KRPCProperty]
		public TimeSelector TimeSelector { get; }
	}
}

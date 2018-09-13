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
		public OperationException(string message) : base(message) { }
	}

	public abstract class Operation {
		private static Type operationException;
		private static MethodInfo errorMessage;
		private static MethodInfo makeNodeImpl;

		private static object[] operations;

		protected readonly Type type;
		protected internal readonly object instance;

		public Operation(string operationType) {
			string operationFullName = "MuMech." + operationType;
			foreach(object operation in operations)
				if(operation.GetType().FullName == operationFullName) {
					this.type = operation.GetType();
					this.instance = operation;
					break;
				}

			if(this.type == null)
				throw new MJServiceException("No such operation exists: " + operationType);
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

				if(ex.GetType() == operationException)
					throw new OperationException(ex.Message);

				Logger.Severe("An error occured while creating a new ManeuverNode", ex);
				throw new MJServiceException(ex.Message);
			}
		}

		internal static bool InitTypes(Type t) {
			switch(t.FullName) {
				case "MuMech.ManeuverParameters":
					ManeuverParameters.dV = t.GetField("dV");
					ManeuverParameters.uT = t.GetField("UT");
					return true;
				case "MuMech.Operation":
					errorMessage = t.GetMethod("getErrorMessage");
					makeNodeImpl = t.GetMethod("MakeNodeImpl");
					return true;
				case "MuMech.OperationException":
					operationException = t;
					return true;
				default:
					return false;
			}
		}

		internal static bool InitInstance(Type t) {
			switch(t.FullName) {
				case "MuMech.MechJebModuleManeuverPlanner":
					operations = (object[])t.GetField("operation", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(MechJeb.GetComputerModule("ManeuverPlanner"));
					return true;
				default:
					return false;
			}
		}

		private static class ManeuverParameters {
			internal static FieldInfo dV;
			internal static FieldInfo uT;
		}
	}

	public abstract class TimedOperation : Operation {
		public TimedOperation(string operationName) : base(operationName) {
			this.TimeSelector = new TimeSelector(this.type.GetField("timeSelector", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this.instance));
		}

		[KRPCProperty]
		public TimeSelector TimeSelector { get; }
	}
}

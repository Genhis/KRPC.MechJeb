using KRPC.Service.Attributes;

namespace KRPC.MechJeb.Maneuver {
	/// <summary>
	/// This mode creates a manevuer to match your apoapsis to periapsis.
	/// To match apoapsis to periapsis, set the time to <see cref="TimeReference.Periapsis" />; to match periapsis to apoapsis, set the time to <see cref="TimeReference.Apoapsis" />. Theese are the most efficient, but it can also create node at specific height or after specific time.
	/// </summary>
	[KRPCClass(Service = "MechJeb")]
	public class OperationCircularize : TimedOperation {
		public OperationCircularize() : base("OperationCircularize") {}
	}
}

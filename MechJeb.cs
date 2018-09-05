using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.MechJeb.Maneuver;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	[KRPCService(GameScene = Service.GameScene.Flight)]
	public static class MechJeb {
		private static Type type;
		private static MethodInfo getComputerModule;

		private static SimpleModule[] modules;

		internal static bool InitTypes() {
			AssemblyLoader.loadedAssemblies.TypeOperation(t => {
				switch(t.FullName) {
					case "MuMech.MechJebCore":
						type = t;
						getComputerModule = t.GetMethod("GetComputerModule", new Type[] { typeof(string) });
						break;
					default:
						bool unused = EditableVariables.InitTypes(t) || VesselExtensions.InitTypes(t) || ComputerModule.InitTypes(t) || Operation.InitTypes(t) || TimeSelector.InitTypes(t);
						break;
				}
			});

			return type != null;
		}

		internal static bool InitInstance() {
			//assume all MechJeb types are loaded

			APIReady = false;
			modules = new SimpleModule[10];

			Instance = FlightGlobals.ActiveVessel.GetMasterMechJeb();
			if(Instance == null)
				return false;

			AssemblyLoader.loadedAssemblies.TypeOperation(t => Operation.InitInstance(t));
			APIReady = true;
			return true;
		}

		internal static object GetComputerModule(string moduleType) {
			return getComputerModule.Invoke(Instance, new object[] { "MechJebModule" + moduleType });
		}

		internal static PartModule Instance { get; private set; }

		public static bool TypesLoaded => type != null;

		[KRPCProperty]
		public static bool APIReady { get; private set; }

		[KRPCProperty]
		public static AirplaneAutopilot AirplaneAutopilot => GetComputerModule<AirplaneAutopilot>(0);

		[KRPCProperty]
		public static AscentAutopilot AscentAutopilot => GetComputerModule<AscentAutopilot>(1);

		[KRPCProperty]
		public static DockingAutopilot DockingAutopilot => GetComputerModule<DockingAutopilot>(2);

		[KRPCProperty]
		public static LandingAutopilot LandingAutopilot => GetComputerModule<LandingAutopilot>(3);

		[KRPCProperty]
		public static ManeuverPlanner ManeuverPlanner => GetComputerModule<ManeuverPlanner>(4);

		[KRPCProperty]
		public static NodeExecutor NodeExecutor => GetComputerModule<NodeExecutor>(5);

		[KRPCProperty]
		public static RCSController RCSController => GetComputerModule<RCSController>(6);

		[KRPCProperty]
		public static RendezvousAutopilot RendezvousAutopilot => GetComputerModule<RendezvousAutopilot>(7);

		[KRPCProperty]
		public static StagingController StagingController => GetComputerModule<StagingController>(8);

		[KRPCProperty]
		public static TargetController TargetController => GetComputerModule<TargetController>(9);

		private static T GetComputerModule<T>(int id) where T : SimpleModule {
			if(modules[id] == null)
				modules[id] = typeof(T).CreateInstance<T>();
			return (T)modules[id];
		}
	}

	[KRPCException(Service = "MechJeb")]
	public class MJServiceException : Exception {
		public MJServiceException(string message) : base(message) { }
	}
}

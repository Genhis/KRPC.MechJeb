using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.MechJeb.Maneuver;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// This service provides functionality to interact with <a href="https://github.com/MuMech/MechJeb2">MechJeb 2</a>.
	/// </summary>
	[KRPCService(GameScene = Service.GameScene.Flight)]
	public static class MechJeb {
		private static Type type;
		private static MethodInfo getComputerModule;

		private static object[] modules;
		private static object[] windows;
		private static object[] controllers;

		internal static bool InitTypes() {
			AssemblyLoader.loadedAssemblies.TypeOperation(t => {
				switch(t.FullName) {
					case "MuMech.MechJebCore":
						type = t;
						getComputerModule = t.GetMethod("GetComputerModule", new Type[] { typeof(string) });
						break;
					default:
						bool unused = AscentAutopilot.InitTypes(t) || ComputerModule.InitTypes(t) || EditableVariables.InitTypes(t) || Operation.InitTypes(t) || TimeSelector.InitTypes(t) || VesselExtensions.InitTypes(t);
						break;
				}
			});

			return type != null;
		}

		internal static bool InitInstance() {
			//assume all MechJeb types are loaded

			APIReady = false;
			modules = new object[5];
			windows = new object[4];
			controllers = new object[7];

			Instance = FlightGlobals.ActiveVessel.GetMasterMechJeb();
			if(Instance == null)
				return false;

			AssemblyLoader.loadedAssemblies.TypeOperation(t => Operation.InitInstance(t));
			APIReady = true;
			return true;
		}

		internal static object GetComputerModule(string moduleType) {
			object module = getComputerModule.Invoke(Instance, new object[] { "MechJebModule" + moduleType });
			if(module == null)
				Logger.Severe("No instance of " + moduleType + " found");

			return module;
		}

		internal static PartModule Instance { get; private set; }

		public static bool TypesLoaded => type != null;

		/// <summary>
		/// A value indicating whether the service is available.
		/// </summary>
		[KRPCProperty]
		public static bool APIReady { get; private set; }

		// MODULES

		[KRPCProperty]
		public static AirplaneAutopilot AirplaneAutopilot => GetComputerModule<AirplaneAutopilot>(modules, 0);

		[KRPCProperty]
		public static AscentAutopilot AscentAutopilot => GetComputerModule<AscentAutopilot>(modules, 1);

		[KRPCProperty]
		public static DockingAutopilot DockingAutopilot => GetComputerModule<DockingAutopilot>(modules, 2);

		[KRPCProperty]
		public static LandingAutopilot LandingAutopilot => GetComputerModule<LandingAutopilot>(modules, 3);

		[KRPCProperty]
		public static RendezvousAutopilot RendezvousAutopilot => GetComputerModule<RendezvousAutopilot>(modules, 4);

		// WINDOWS

		[KRPCProperty]
		public static ManeuverPlanner ManeuverPlanner => GetComputerModule<ManeuverPlanner>(windows, 0);

		[KRPCProperty]
		public static SmartASS SmartASS => GetComputerModule<SmartASS>(windows, 2);

		[KRPCProperty]
		public static SmartRCS SmartRCS => GetComputerModule<SmartRCS>(windows, 1);

		[KRPCProperty]
		public static Translatron Translatron => GetComputerModule<Translatron>(windows, 3);

		// CONTROLLERS

		[KRPCProperty]
		public static DeployableController AntennaController => GetComputerModule<DeployableController>(controllers, 5, "DeployableAntennaController");

		[KRPCProperty]
		public static NodeExecutor NodeExecutor => GetComputerModule<NodeExecutor>(controllers, 0);

		[KRPCProperty]
		public static RCSController RCSController => GetComputerModule<RCSController>(controllers, 1);

		[KRPCProperty]
		public static StagingController StagingController => GetComputerModule<StagingController>(controllers, 2);

		[KRPCProperty]
		public static DeployableController SolarPanelController => GetComputerModule<DeployableController>(controllers, 6, "SolarPanelController");

		[KRPCProperty]
		public static TargetController TargetController => GetComputerModule<TargetController>(controllers, 3);

		[KRPCProperty]
		public static ThrustController ThrustController => GetComputerModule<ThrustController>(controllers, 4);

		private static T GetComputerModule<T>(object[] modules, int id) {
			return GetComputerModule<T>(modules, id, (object[])null);
		}

		private static T GetComputerModule<T>(object[] modules, int id, string module) {
			return GetComputerModule<T>(modules, id, new object[] { module });
		}

		private static T GetComputerModule<T>(object[] modules, int id, object[] args) {
			if(modules[id] == null)
				modules[id] = typeof(T).CreateInstance<T>(args);
			return (T)modules[id];
		}
	}

	/// <summary>
	/// General exception for errors in the service.
	/// </summary>
	[KRPCException(Service = "MechJeb")]
	public class MJServiceException : Exception {
		public MJServiceException(string message) : base(message) { }
	}
}

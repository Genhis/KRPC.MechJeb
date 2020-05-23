using System;
using System.Collections.Generic;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;
using KRPC.Service.Attributes;

namespace KRPC.MechJeb {
	/// <summary>
	/// This service provides functionality to interact with <a href="https://github.com/MuMech/MechJeb2">MechJeb 2</a>.
	/// </summary>
	[KRPCService(GameScene = Service.GameScene.Flight)]
	public static class MechJeb {
		internal const string MechJebType = "MuMech.MechJebCore";

		private static Type type;
		private static MethodInfo getComputerModule;

		private static readonly Dictionary<string, Module> modules = new Dictionary<string, Module>();

		internal static bool InitTypes() {
			// Scan the project assembly for MechJeb 2 reflection classes
			Dictionary<string, Type> mechjebTypes = new Dictionary<string, Type>();
			foreach(Type t in Assembly.GetExecutingAssembly().GetTypes()) {
				FieldInfo mechjebTypeField = t.GetField("MechJebType", BindingFlags.NonPublic | BindingFlags.Static);
				if(mechjebTypeField != null) {
					string mechjebType = (string)mechjebTypeField.GetValue(null);
					Logger.Info("Found class " + t.Name + " wanting to use " + mechjebType);
					mechjebTypes.Add(mechjebType, t);
				}
			}

			// Scan all assemblies to match kRPC classes to MechJeb 2
			AssemblyLoader.loadedAssemblies.TypeOperation(mechjebType => {
				if(mechjebTypes.TryGetValue(mechjebType.FullName, out Type internalType)) {
					Logger.Info("Loading class " + internalType.Name + " using " + mechjebType.FullName);
					internalType.GetMethod("InitType", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { mechjebType });
					mechjebTypes.Remove(mechjebType.FullName);
				}
			});

			// Check if all classes have been initialized
			foreach(KeyValuePair<string, Type> p in mechjebTypes)
				Logger.Severe("Cannot initialize class " + p.Value.Name + " because " + p.Key + " was not found");

			return type != null;
		}

		internal static void InitType(Type t) {
			type = t;
			getComputerModule = t.GetCheckedMethod("GetComputerModule", new Type[] { typeof(string) });

			// MechJeb found, create module instances
			modules.Add("AirplaneAutopilot", new AirplaneAutopilot());
			modules.Add("AscentAutopilot", new AscentAutopilot());
			modules.Add("DockingAutopilot", new DockingAutopilot());
			modules.Add("LandingAutopilot", new LandingAutopilot());
			modules.Add("RendezvousAutopilot", new RendezvousAutopilot());

			modules.Add("ManeuverPlanner", new ManeuverPlanner());
			modules.Add("SmartASS", new SmartASS());
			modules.Add("SmartRCS", new SmartRCS());
			modules.Add("Translatron", new Translatron());

			modules.Add("DeployableAntennaController", new DeployableController());
			modules.Add("NodeExecutor", new NodeExecutor());
			modules.Add("RCSController", new RCSController());
			modules.Add("StagingController", new StagingController());
			modules.Add("SolarPanelController", new DeployableController());
			modules.Add("TargetController", new TargetController());
			modules.Add("ThrustController", new ThrustController());
		}

		internal static bool InitInstance() {
			//assume all MechJeb types are loaded

			APIReady = false;
			Instance = FlightGlobals.ActiveVessel.GetMasterMechJeb();
			if(Instance == null)
				return false;

			// Set module instances to MechJeb objects
			foreach(KeyValuePair<string, Module> p in modules) {
				object moduleInstance = GetComputerModule(p.Key);
				if(moduleInstance != null)
					p.Value.InitInstance(moduleInstance);
			}

			APIReady = true;
			return true;
		}

		internal static object GetComputerModule(string moduleType) {
			object module = getComputerModule.Invoke(Instance, new object[] { "MechJebModule" + moduleType });
			if(module == null)
				Logger.Severe("MechJeb module " + moduleType + " not found");

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
		public static AirplaneAutopilot AirplaneAutopilot => (AirplaneAutopilot)modules["AirplaneAutopilot"];

		[KRPCProperty]
		public static AscentAutopilot AscentAutopilot => (AscentAutopilot)modules["AscentAutopilot"];

		[KRPCProperty]
		public static DockingAutopilot DockingAutopilot => (DockingAutopilot)modules["DockingAutopilot"];

		[KRPCProperty]
		public static LandingAutopilot LandingAutopilot => (LandingAutopilot)modules["LandingAutopilot"];

		[KRPCProperty]
		public static RendezvousAutopilot RendezvousAutopilot => (RendezvousAutopilot)modules["RendezvousAutopilot"];

		// WINDOWS

		[KRPCProperty]
		public static ManeuverPlanner ManeuverPlanner => (ManeuverPlanner)modules["ManeuverPlanner"];

		[KRPCProperty]
		public static SmartASS SmartASS => (SmartASS)modules["SmartASS"];

		[KRPCProperty]
		public static SmartRCS SmartRCS => (SmartRCS)modules["SmartRCS"];

		[KRPCProperty]
		public static Translatron Translatron => (Translatron)modules["Translatron"];

		// CONTROLLERS

		[KRPCProperty]
		public static DeployableController AntennaController => (DeployableController)modules["AntennaController"];

		[KRPCProperty]
		public static NodeExecutor NodeExecutor => (NodeExecutor)modules["NodeExecutor"];

		[KRPCProperty]
		public static RCSController RCSController => (RCSController)modules["RCSController"];

		[KRPCProperty]
		public static StagingController StagingController => (StagingController)modules["StagingController"];

		[KRPCProperty]
		public static DeployableController SolarPanelController => (DeployableController)modules["SolarPanelController"];

		[KRPCProperty]
		public static TargetController TargetController => (TargetController)modules["TargetController"];

		[KRPCProperty]
		public static ThrustController ThrustController => (ThrustController)modules["ThrustController"];
	}

	/// <summary>
	/// General exception for errors in the service.
	/// </summary>
	[KRPCException(Service = "MechJeb")]
	public class MJServiceException : Exception {
		public MJServiceException(string message) : base(message) { }
	}
}

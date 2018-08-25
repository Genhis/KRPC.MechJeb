using System;

namespace KRPC.MechJeb {
	public abstract class SimpleModule {
		protected Type type;
		protected internal readonly object instance;

		public SimpleModule(string moduleType) {
			string fullModuleName = "MuMech.MechJebModule" + moduleType;
			AssemblyLoader.loadedAssemblies.TypeOperation(t => {
				if(t.FullName == fullModuleName)
					this.type = t;
			});

			this.instance = MechJeb.GetComputerModule(moduleType);
		}
	}
}

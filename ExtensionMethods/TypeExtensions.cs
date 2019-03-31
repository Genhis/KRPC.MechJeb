using System;
using System.Reflection;

namespace KRPC.MechJeb.ExtensionMethods {
	public static class TypeExtensions {
		public static T CreateInstance<T>(this Type type, object[] args) {
			try {
				Type[] types = Type.EmptyTypes;
				if(args != null) {
					types = new Type[args.Length];
					for(int i = 0; i < args.Length; i++)
						types[i] = args[i].GetType();
				}

				return (T)type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null).Invoke(args);
			}
			catch(Exception ex) {
				Logger.Severe("Coudn't create an instance of " + type, ex);
				throw new MJServiceException(ex.ToString());
			}
		}
	}
}

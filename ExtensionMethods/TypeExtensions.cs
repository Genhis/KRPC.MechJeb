using System;
using System.Reflection;

namespace KRPC.MechJeb.ExtensionMethods {
	public static class TypeExtensions {
		public static T CreateInstance<T>(this Type type, object[] args) {
			try {
				return (T)type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null).Invoke(args);
			}
			catch(Exception ex) {
				Logger.Severe("Coudn't create an instance of " + type, ex);
				if(ex is TargetInvocationException)
					throw new MJServiceException(ex.Message + ": " + ex.InnerException.Message);
				throw new MJServiceException(ex.Message);
			}
		}
	}
}

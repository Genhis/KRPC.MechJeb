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

		public static FieldInfo GetCheckedField(this Type type, string name) {
			return type.GetField(name).CheckIfExists(type, name);
		}

		public static FieldInfo GetCheckedField(this Type type, string name, BindingFlags bindingAttr) {
			return type.GetField(name, bindingAttr).CheckIfExists(type, name);
		}

		public static PropertyInfo GetCheckedProperty(this Type type, string name) {
			return type.GetProperty(name).CheckIfExists(type, name);
		}

		public static MethodInfo GetCheckedMethod(this Type type, string name) {
			return type.GetMethod(name).CheckIfExists(type, name + "()");
		}

		public static MethodInfo GetCheckedMethod(this Type type, string name, Type[] types) {
			return type.GetMethod(name, types).CheckIfExists(type, name + "()");
		}

		public static MethodInfo GetCheckedMethod(this Type type, string name, BindingFlags bindingAttr) {
			return type.GetMethod(name, bindingAttr).CheckIfExists(type, name + "()");
		}

		private static T CheckIfExists<T>(this T obj, Type type, string name) {
			if(obj == null)
				Logger.Severe(type + "." + name + " not found");
			else
				Logger.Info(type + "." + name + " found");

			return obj;
		}
	}
}

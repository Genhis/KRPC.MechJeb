using System;
using System.Reflection;

using KRPC.MechJeb.ExtensionMethods;

namespace KRPC.MechJeb {
	public static class EditableDouble {
		internal const string MechJebType = "MuMech.EditableDoubleMult";

		// Fields and methods
		private static PropertyInfo value;

		internal static void InitType(Type type) {
			value = type.GetCheckedProperty("Val");
		}

		public static double Get(object instance) {
			return (double)value.GetValue(instance, null);
		}

		public static void Set(object instance, double value) {
			EditableDouble.value.SetValue(instance, value, null);
		}

		// Helper methods for fields which create a new object every time they are changed in GUI
		public static double Get(FieldInfo field, object instance) {
			return Get(field.GetValue(instance));
		}

		public static void Set(FieldInfo field, object instance, double value) {
			Set(field.GetValue(instance), value);
		}
	}

	public static class EditableInt {
		internal const string MechJebType = "MuMech.EditableInt";

		// Fields and methods (Val and Text are properties in MechJeb 2.15.x)
		private static PropertyInfo value;
		private static PropertyInfo text;

		internal static void InitType(Type type) {
			value = type.GetCheckedProperty("Val");
			text = type.GetCheckedProperty("Text");
		}

		public static int Get(object instance) {
			return (int)value.GetValue(instance, null);
		}

		public static void Set(object instance, int value) {
			EditableInt.value.SetValue(instance, value, null);
			text.SetValue(instance, value.ToString(), null);
		}
	}

	public static class MovingAverage {
		internal const string MechJebType = "MuMech.MovingAverage";

		// Fields and methods
		private static PropertyInfo value;

		internal static void InitType(Type type) {
			value = type.GetCheckedProperty("Value");
		}

		public static double Get(object instance) {
			return (double)value.GetValue(instance, null);
		}
	}
}

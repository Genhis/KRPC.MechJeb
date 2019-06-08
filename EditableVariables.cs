using System;
using System.Reflection;

namespace KRPC.MechJeb {
	public static class EditableVariables {
		private static PropertyInfo editableDoubleVal;
		private static FieldInfo editableIntVal;
		private static FieldInfo editableIntText;

		internal static bool InitTypes(Type t) {
			switch(t.FullName) {
				/*case "MuMech.EditableAngle":
					EditableAngle.degreesField = t.GetField("degrees");
					EditableAngle.minutesField = t.GetField("minutes");
					EditableAngle.secondsField = t.GetField("seconds");
					EditableAngle.negative = t.GetField("negative");
					return true;*/
				case "MuMech.EditableDoubleMult":
					editableDoubleVal = t.GetProperty("val");
					return true;
				case "MuMech.EditableInt":
					editableIntVal = t.GetField("val");
					editableIntText = t.GetField("_text");
					return true;
				default:
					return false;
			}
		}

		public static double GetDouble(object instance) {
			return (double)editableDoubleVal.GetValue(instance, null);
		}

		public static void SetDouble(object instance, double value) {
			editableDoubleVal.SetValue(instance, value, null);
		}

		public static int GetInt(object instance) {
			return (int)editableIntVal.GetValue(instance);
		}

		public static void SetInt(object instance, int value) {
			editableIntVal.SetValue(instance, value);
			editableIntText.SetValue(instance, value.ToString());
		}

		// Helper methods for fields which create a new object every time they are changed in GUI
		public static double GetDouble(FieldInfo field, object instance) {
			return GetDouble(field.GetValue(instance));
		}

		public static void SetDouble(FieldInfo field, object instance, double value) {
			SetDouble(field.GetValue(instance), value);
		}
	}
}

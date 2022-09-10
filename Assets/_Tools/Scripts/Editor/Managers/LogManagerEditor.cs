using UnityEditor;
using UnityEngine;

namespace _Tools.Managers
{
	[CustomEditor(typeof(LogManager))]
	public class LogManagerEditor : Editor
	{
		#region Variables

		private SerializedProperty _isLogEnabledProperty;
		private SerializedProperty _isWarningEnabledProperty;
		private SerializedProperty _isErrorEnabledProperty;
		private SerializedProperty _logColorProperty;
		private SerializedProperty _warningColorProperty;
		private SerializedProperty _errorColorProperty;

		private GUIStyle _logLabelStyle;
		private GUIStyle _logStatusStyle;
		private GUIStyle _warningLabelStyle;
		private GUIStyle _warningStatusStyle;
		private GUIStyle _errorLabelStyle;
		private GUIStyle _errorStatusStyle;

		#endregion

		#region Unity Methods

		private void OnEnable()
		{
			_isLogEnabledProperty = serializedObject.FindProperty("_isLogEnabled");
			_isWarningEnabledProperty = serializedObject.FindProperty("_isWarningEnabled");
			_isErrorEnabledProperty = serializedObject.FindProperty("_isErrorEnabled");
			_logColorProperty = serializedObject.FindProperty("_logColor");
			_warningColorProperty = serializedObject.FindProperty("_warningColor");
			_errorColorProperty = serializedObject.FindProperty("_errorColor");

			SetInitialLabelStyle(out _logLabelStyle);
			SetInitialStatusStyle(out _logStatusStyle);
			SetInitialLabelStyle(out _warningLabelStyle);
			SetInitialStatusStyle(out _warningStatusStyle);
			SetInitialLabelStyle(out _errorLabelStyle);
			SetInitialStatusStyle(out _errorStatusStyle);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			_isLogEnabledProperty.boolValue = CreateLogOptionGroup
			(
				out _logColorProperty,
				"_logColor",
				_isLogEnabledProperty.boolValue,
				"Log Status",
				_logLabelStyle,
				_logColorProperty.colorValue,
				_logStatusStyle
			);

			_isWarningEnabledProperty.boolValue = CreateLogOptionGroup
			(
				out _warningColorProperty,
				"_warningColor",
				_isWarningEnabledProperty.boolValue,
				"Warning Status",
				_warningLabelStyle,
				_warningColorProperty.colorValue,
				_warningStatusStyle
			);

			_isErrorEnabledProperty.boolValue = CreateLogOptionGroup
			(
				out _errorColorProperty,
				"_errorColor",
				_isErrorEnabledProperty.boolValue,
				"Error Status",
				_errorLabelStyle,
				_errorColorProperty.colorValue,
				_errorStatusStyle
			);

			serializedObject.ApplyModifiedProperties();
		}

		#endregion

		#region Custom Methods

		private static void SetInitialLabelStyle(out GUIStyle style)
		{
			style = new GUIStyle
			{
				alignment = TextAnchor.MiddleLeft
			};
		}

		private static void SetInitialStatusStyle(out GUIStyle style)
		{
			style = new GUIStyle
			{
				alignment = TextAnchor.MiddleLeft,
				fontStyle = FontStyle.Bold
			};
		}

		private bool CreateLogOptionGroup
		(
			out SerializedProperty colorProperty,
			string colorPropertyName,
			bool isEnabledProperty,
			string title,
			GUIStyle labelStyle,
			Color labelColor,
			GUIStyle statusStyle
		)
		{
			EditorGUILayout.BeginHorizontal();

			labelStyle.normal.textColor = labelColor;

			GUILayout.Label($"{title}: ", labelStyle, GUILayout.MinWidth(100), GUILayout.Height(25));

			var statusString = isEnabledProperty ? "Enabled" : "Disabled";

			statusStyle.normal.textColor = isEnabledProperty ? Color.green : Color.red;

			GUILayout.Label($"{statusString}", statusStyle, GUILayout.Height(25));

			colorProperty = serializedObject.FindProperty($"{colorPropertyName}");

			if (isEnabledProperty)
			{
				colorProperty.colorValue = EditorGUILayout.ColorField(colorProperty.colorValue, GUILayout.ExpandHeight(true));
			}

			var statusButtonString = isEnabledProperty ? "Disable" : "Enable";

			if (GUILayout.Button($"{statusButtonString}", GUILayout.ExpandHeight(true)))
			{
				return !isEnabledProperty;
			}

			EditorGUILayout.EndHorizontal();

			return isEnabledProperty;
		}

		#endregion
	}
}
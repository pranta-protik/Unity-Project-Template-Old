using UnityEngine;

namespace _Tools.Helpers
{
    public class DebugUtils : MonoBehaviour
    {
        #region Variables

        private static bool _IsLogEnabled;
        private static bool _IsWarningEnabled;
        private static bool _IsErrorEnabled;

        private static Color _LogColor;
        private static Color _WarningColor;
        private static Color _ErrorColor;

        #endregion

        #region Constructor

        static DebugUtils()
        {
            _IsLogEnabled = true;
            _IsWarningEnabled = true;
            _IsErrorEnabled = true;

            _LogColor = Color.white;
            _WarningColor = Color.yellow;
            _ErrorColor = Color.red;
        }

        #endregion

        #region Helper Methods

        public static void Log(string message)
        {
#if UNITY_EDITOR
            if (_IsLogEnabled) Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(_LogColor)}>Info: {message}</color>");
#endif
        }

        public static void LogWarning(string message)
        {
#if UNITY_EDITOR
            if (_IsWarningEnabled) Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(_WarningColor)}>Warning: {message}</color>");
#endif
        }

        public static void LogError(string message)
        {
#if UNITY_EDITOR
            if (_IsErrorEnabled) Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(_ErrorColor)}>Error: {message}</color>");
#endif
        }

        public static void UpdateLogStatus(bool isEnabled) => _IsLogEnabled = isEnabled;
        public static void UpdateWarningStatus(bool isEnabled) => _IsWarningEnabled = isEnabled;
        public static void UpdateErrorStatus(bool isEnabled) => _IsErrorEnabled = isEnabled;
        public static void UpdateLogColor(Color color) => _LogColor = color;
        public static void UpdateWarningColor(Color color) => _WarningColor = color;
        public static void UpdateErrorColor(Color color) => _ErrorColor = color;

        #endregion
    }
}
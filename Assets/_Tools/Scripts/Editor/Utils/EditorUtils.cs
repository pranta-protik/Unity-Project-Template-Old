using UnityEditor;

namespace _Tools.Utils
{
    public static class EditorUtils
    {
        public static void DisplayDialogBox(string title, string message)
        {
            EditorUtility.DisplayDialog(title, message, "OK");
        }

        public static bool DisplayDialogBoxWithOptions(string title, string message)
        {
            return EditorUtility.DisplayDialog(title, message, "Yes", "No");
        }
    }
}
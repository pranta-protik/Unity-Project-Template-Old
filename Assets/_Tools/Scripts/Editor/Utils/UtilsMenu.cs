using _Tools.Helpers;
using UnityEditor;

namespace _Tools.Utils
{
    public static class UtilsMenu
    {
        #region Scene Helpers

        [MenuItem("Tools/Scene Tools/Create Initial Scenes")]
        public static void CreateInitialScenes() => SceneHelpers.CreateInitialScenes();

        [MenuItem("Tools/Scene Tools/Manager Presets/Create Log Manager")]
        public static void CreateLogManager() => SceneHelpers.CreateLogManager();

        [MenuItem("Tools/Scene Tools/Manager Presets/Create Scene Load Manager")]
        public static void CreateSceneLoadManager() => SceneHelpers.CreateSceneLoadManager();

        [MenuItem("Tools/Scene Tools/UI Presets/Create Splash UI")]
        public static void CreateSplashUI() => SceneHelpers.CreateSplashUI();

        #endregion
    }
}
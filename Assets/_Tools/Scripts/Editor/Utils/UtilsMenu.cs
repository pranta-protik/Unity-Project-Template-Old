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

        [MenuItem("Tools/Scene Tools/Manager Presets/Create Manager Group")]
        public static void CreateManagerGroup() => SceneHelpers.CreateManagerGroup();

        [MenuItem("Tools/Scene Tools/Scene Presets/Create Persistent Scene")]
        public static void CreatePersistentScene() => SceneHelpers.CreatePersistentScene();
        
        [MenuItem("Tools/Scene Tools/Scene Presets/UI Scenes/Create Splash Scene")]
        public static void CreateSplashUI() => SceneHelpers.CreateSplashUI();
        
        [MenuItem("Tools/Scene Tools/UI Presets/Create Level UI")]
        public static void CreateLevelUI() => SceneHelpers.CreateLevelUI();
        
        [MenuItem("Tools/Scene Tools/Scene Presets/Game Scenes/Create Joystick Controller Scene")]
        public static void CreateJoystickControllerScene() => SceneHelpers.CreateJoystickControllerScene();
        
        [MenuItem("Tools/Scene Tools/Scene Presets/Game Scenes/Create Runner Controller Scene")]
        public static void CreateRunnerControllerScene() => SceneHelpers.CreateRunnerControllerScene();

        #endregion
    }
}
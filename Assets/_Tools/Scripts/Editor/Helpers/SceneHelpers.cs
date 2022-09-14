using System;
using System.Collections.Generic;
using System.IO;
using _Tools.Managers;
using _Tools.Utils;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace _Tools.Helpers
{
    public static class SceneHelpers
    {
        #region Variables

        private static readonly List<EditorBuildSettingsScene> _editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();

        #endregion

        #region Helper Methods

        public static void CreateInitialScenes()
        {
            var assetFolder = Application.dataPath;
            var rootPath = assetFolder + "/_Game";

            var sceneInfo = Directory.CreateDirectory(rootPath + "/Scenes");

            if (sceneInfo.Exists)
            {
                CreateScene(rootPath + "/Scenes", "Persistent", true);
                CreateScene(rootPath + "/Scenes", "Splash", true);
                CreateScene(rootPath + "/Scenes", "UI", true);
                CreateScene(rootPath + "/Scenes", "Game");
            }
            
            if (!EditorUtils.DisplayDialogBoxWithOptions("Warning", "Do you want to add scenes to the editor build settings?")) return;
            
            DebugUtils.Log("Adding scenes to editor build settings...");
            EditorBuildSettings.scenes = _editorBuildSettingsScenes.ToArray();

            AssetDatabase.Refresh();
        }
        
        private static void CreateScene(string scenePath, string sceneName, bool isEmpty = false)
        {
            var currentSceneSetup = NewSceneSetup.DefaultGameObjects;

            if (isEmpty)
            {
                currentSceneSetup = NewSceneSetup.EmptyScene;
            }

            var currentScene = EditorSceneManager.NewScene(currentSceneSetup, NewSceneMode.Single);
            EditorSceneManager.SaveScene(currentScene, scenePath + "/" + sceneName + ".unity", true);

            _editorBuildSettingsScenes.Add(new EditorBuildSettingsScene("Assets/_Game/Scenes/" + sceneName + ".unity", true));
        }
        
        public static void CreateLogManager()
        {
            var logManagerGO = new GameObject("LogManager");
            logManagerGO.AddComponent<LogManager>();

            Selection.activeGameObject = logManagerGO;
        }

        public static void CreatePersistentScene()
        {
            var initializerGO = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/Persistent/Initializer.prefab", typeof(GameObject)) as GameObject;

            var currentInitializerGO = InstantiateAsGameObject(initializerGO, "Initializer");
            
            Selection.activeGameObject = currentInitializerGO;
        }

        public static void CreateSplashUI()
        {
            var splashUIGroupGO = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/UI/SplashUI_GRP.prefab", typeof(GameObject)) as GameObject;

            if (!splashUIGroupGO)
            {
                EditorUtils.DisplayDialogBox("Error", "Unable to find the SplashUI_GRP prefab!");
                return;
            }

            var currentSplashUIGroupGO = Object.Instantiate(splashUIGroupGO);
            currentSplashUIGroupGO.transform.DetachChildren();
            Object.DestroyImmediate(currentSplashUIGroupGO);

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        public static void CreateLevelUI()
        {
            var levelUIGroupGO = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/UI/LevelUI_GRP.prefab", typeof(GameObject)) as GameObject;

            if (!levelUIGroupGO)
            {
                EditorUtils.DisplayDialogBox("Error", "Unable to find the LevelUI_GRP prefab!");
                return;
            }

            var currentLevelUIGroupGO = Object.Instantiate(levelUIGroupGO);
            currentLevelUIGroupGO.transform.DetachChildren();
            Object.DestroyImmediate(currentLevelUIGroupGO);

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        public static void CreateJoystickControllerScene()
        {
            var joystickSceneGO = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/DemoScenes/JoystickScene.prefab", typeof(GameObject)) as GameObject;

            var currentJoystickSceneGO = InstantiateAsPrefab(joystickSceneGO, "JoystickScene");
            PrefabUtility.UnpackPrefabInstance(currentJoystickSceneGO, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
            
            currentJoystickSceneGO.transform.DetachChildren();
            Object.DestroyImmediate(currentJoystickSceneGO);

            var playerGO = GameObject.Find("Player");
            PrefabUtility.UnpackPrefabInstance(playerGO, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        public static void CreateRunnerControllerScene()
        {
            var runnerSceneGO = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/DemoScenes/RunnerScene.prefab", typeof(GameObject)) as GameObject;

            var currentRunnerSceneGO = InstantiateAsPrefab(runnerSceneGO, "RunnerScene");
            PrefabUtility.UnpackPrefabInstance(currentRunnerSceneGO, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
            
            currentRunnerSceneGO.transform.DetachChildren();
            Object.DestroyImmediate(currentRunnerSceneGO);
            
            var playerGO = GameObject.Find("Player");
            PrefabUtility.UnpackPrefabInstance(playerGO, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        private static GameObject InstantiateAsPrefab(GameObject prefabGO, string prefabName)
        {
            if (prefabGO)
            {
                var currentPrefabGO = PrefabUtility.InstantiatePrefab(prefabGO) as GameObject;
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

                return currentPrefabGO;
            }
            
            EditorUtils.DisplayDialogBox("Error", $"Unable to find the {prefabName} prefab!");
            return null;
        }

        private static GameObject InstantiateAsGameObject(GameObject prefabGO, string prefabName)
        {
            if (prefabGO)
            {
                var currentPrefabGO = Object.Instantiate(prefabGO);
                currentPrefabGO.name = currentPrefabGO.name.Substring(0, currentPrefabGO.name.IndexOf("(Clone)", StringComparison.Ordinal));
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                return currentPrefabGO;
            }
            
            EditorUtils.DisplayDialogBox("Error", $"Unable to find the {prefabName} prefab!");
            return null;
        }
        
        #endregion
    }
}
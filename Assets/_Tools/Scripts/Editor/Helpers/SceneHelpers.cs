using System;
using System.Collections.Generic;
using System.IO;
using _Game.Controllers;
using _Game.Managers;
using _Tools.Managers;
using _Tools.Utils;
using Cinemachine;
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
                CreateScene(rootPath + "/Scenes", "Loading", true);
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

        public static void CreateSceneLoadManager()
        {
            var sceneLoadManagerGO = new GameObject("SceneLoadManager");
            sceneLoadManagerGO.AddComponent<SceneLoadManager>();

            Selection.activeGameObject = sceneLoadManagerGO;
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

        public static void CreateJoystickControllerScene()
        {
            CommonSceneSetup();

            var playerCamPrefab =
                AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/Camera/Joystick/PlayerCam.prefab", typeof(GameObject)) as GameObject;

            var playerCamGO = InstantiateAsPrefab(playerCamPrefab, "PlayerCam");
            
            var playerPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/Characters/Joystick/Player.prefab", typeof(GameObject)) as GameObject;

            var playerGO = InstantiateAsGameObject(playerPrefab, "Player");

            playerCamGO.GetComponent<CinemachineVirtualCamera>().m_Follow = playerGO.transform;

            var platformPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/Env/Platform.prefab", typeof(GameObject)) as GameObject;
            InstantiateAsPrefab(platformPrefab, "Platform");
        }

        public static void CreateRunnerControllerScene()
        {
            CommonSceneSetup();
            
            var pathPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/Env/Path.prefab", typeof(GameObject)) as GameObject;
            InstantiateAsPrefab(pathPrefab, "Path");
            
            var playerCamPrefab =
                AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/Camera/Runner/PlayerCam.prefab", typeof(GameObject)) as GameObject;

            var playerCamGO = InstantiateAsPrefab(playerCamPrefab, "PlayerCam");
            
            var playerPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/Characters/Runner/Player.prefab", typeof(GameObject)) as GameObject;

            var playerGO = InstantiateAsGameObject(playerPrefab, "Player");

            playerCamGO.GetComponent<CinemachineVirtualCamera>().m_Follow = playerGO.transform.GetChild(0);
            
            playerGO.GetComponent<PathFollower>().FindPathFromScene();
        }
        
        private static void CommonSceneSetup()
        {
            var directionalLightPrefab =
                AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/Env/Lighting/Directional Light.prefab", typeof(GameObject)) as GameObject;
            InstantiateAsPrefab(directionalLightPrefab, "Directional Light");

            var mainCameraPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Game/Prefabs/Camera/Main Camera.prefab", typeof(GameObject)) as GameObject;
            InstantiateAsPrefab(mainCameraPrefab, "Main Prefab");

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
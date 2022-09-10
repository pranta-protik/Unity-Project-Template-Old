using UnityEditor;
using UnityEngine;

namespace _Game.Managers
{
    [CustomEditor(typeof(SceneLoadManager))]
    public class SceneLoadManagerEditor : Editor
    {
        #region Variables

        private SerializedProperty _dontDestroyOnLoadProperty;
        private SerializedProperty _totalSceneCountProperty;
        private SerializedProperty _firstLevelSceneIndexProperty;
        private SerializedProperty _isTestEnabledProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _dontDestroyOnLoadProperty = serializedObject.FindProperty("_dontDestroyOnLoad");
            _totalSceneCountProperty = serializedObject.FindProperty("_totalSceneCount");
            _firstLevelSceneIndexProperty = serializedObject.FindProperty("_firstLevelSceneIndex");
            _isTestEnabledProperty = serializedObject.FindProperty("_isTestEnabled");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _dontDestroyOnLoadProperty.boolValue = !_isTestEnabledProperty.boolValue;

            _totalSceneCountProperty.intValue = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes).Length;

            EditorGUILayout.PropertyField(_totalSceneCountProperty);
            EditorGUILayout.PropertyField(_firstLevelSceneIndexProperty);
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Toggle("Enable Test Mode", _isTestEnabledProperty.boolValue);
            EditorGUILayout.Toggle("Don't Destroy On Load", _dontDestroyOnLoadProperty.boolValue);
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
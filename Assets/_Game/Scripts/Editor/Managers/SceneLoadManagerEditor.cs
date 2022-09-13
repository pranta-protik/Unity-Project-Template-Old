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

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _dontDestroyOnLoadProperty = serializedObject.FindProperty("_dontDestroyOnLoad");
            _totalSceneCountProperty = serializedObject.FindProperty("_totalSceneCount");
            _firstLevelSceneIndexProperty = serializedObject.FindProperty("_firstLevelSceneIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _totalSceneCountProperty.intValue = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes).Length;

            EditorGUILayout.PropertyField(_totalSceneCountProperty);
            EditorGUILayout.PropertyField(_firstLevelSceneIndexProperty);
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.PropertyField(_dontDestroyOnLoadProperty);

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
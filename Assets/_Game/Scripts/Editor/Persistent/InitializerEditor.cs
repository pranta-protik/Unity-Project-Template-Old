using UnityEditor;
using UnityEngine;

namespace _Game.Persistent
{
    [CustomEditor(typeof(Initializer))]
    public class InitializerEditor : Editor
    {
        #region Variables

        private SerializedProperty _totalSceneCountProperty;
        private SerializedProperty _firstLevelSceneIndexProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _totalSceneCountProperty = serializedObject.FindProperty("_totalSceneCount");
            _firstLevelSceneIndexProperty = serializedObject.FindProperty("_firstLevelSceneIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _totalSceneCountProperty.intValue = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes).Length;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_totalSceneCountProperty);
            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.PropertyField(_firstLevelSceneIndexProperty);

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
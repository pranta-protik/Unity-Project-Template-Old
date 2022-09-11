using UnityEditor;
using UnityEngine;

namespace _Tools.Helpers
{
    [CustomEditor(typeof(MaterialStyler))]
    public class MaterialStylerEditor : Editor
    {
        #region Variables
        
        private SerializedProperty _materialColorProperty;
        private SerializedProperty _materialTextureProperty;
        private SerializedProperty _materialMetallicProperty;
        private SerializedProperty _materialSmoothnessProperty;
        private SerializedProperty _materialNormalProperty;
        private SerializedProperty _isTextureEnabledProperty;
        private SerializedProperty _isNormalEnabledProperty;
        private SerializedProperty _materialIndexProperty;
        private SerializedProperty _rendererProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _materialColorProperty = serializedObject.FindProperty("_materialColor");
            _materialTextureProperty = serializedObject.FindProperty("_materialTexture");
            _materialMetallicProperty = serializedObject.FindProperty("_materialMetallic");
            _materialSmoothnessProperty = serializedObject.FindProperty("_materialSmoothness");
            _materialNormalProperty = serializedObject.FindProperty("_materialNormal");
            _isTextureEnabledProperty = serializedObject.FindProperty("_isTextureEnabled");
            _isNormalEnabledProperty = serializedObject.FindProperty("_isNormalEnabled");
            _rendererProperty = serializedObject.FindProperty("_renderer");
            _materialIndexProperty = serializedObject.FindProperty("_materialIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_materialColorProperty);
            if (_isTextureEnabledProperty.boolValue) EditorGUILayout.PropertyField(_materialTextureProperty);
            
            EditorGUILayout.PropertyField(_materialMetallicProperty);
            EditorGUILayout.PropertyField(_materialSmoothnessProperty);
            
            if (_isNormalEnabledProperty.boolValue) EditorGUILayout.PropertyField(_materialNormalProperty);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Texture");
            EditorGUILayout.PropertyField(_isTextureEnabledProperty, GUIContent.none);
            GUILayout.Label("Normal");
            EditorGUILayout.PropertyField(_isNormalEnabledProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorGUILayout.PropertyField(_materialIndexProperty);
            EditorGUILayout.PropertyField(_rendererProperty);

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
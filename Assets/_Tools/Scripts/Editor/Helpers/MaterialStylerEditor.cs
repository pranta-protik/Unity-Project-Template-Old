using UnityEditor;
using UnityEngine;

namespace _Tools.Helpers
{
    [CustomEditor(typeof(MaterialStyler))]
    public class MaterialStylerEditor : Editor
    {
        #region Variables

        private SerializedProperty _colorProperty;
        private SerializedProperty _textureProperty;
        private SerializedProperty _metallicProperty;
        private SerializedProperty _smoothnessProperty;
        private SerializedProperty _normalProperty;
        private SerializedProperty _isTextureEnabledProperty;
        private SerializedProperty _isNormalEnabledProperty;
        private SerializedProperty _materialIndexProperty;
        private SerializedProperty _rendererProperty;

        #endregion

        #region Unity Methods

        private void OnEnable() => Init();

        protected virtual void Init()
        {
            _colorProperty = serializedObject.FindProperty("_color");
            _textureProperty = serializedObject.FindProperty("_texture");
            _metallicProperty = serializedObject.FindProperty("_metallic");
            _smoothnessProperty = serializedObject.FindProperty("_smoothness");
            _normalProperty = serializedObject.FindProperty("_normal");
            _isTextureEnabledProperty = serializedObject.FindProperty("_isTextureEnabled");
            _isNormalEnabledProperty = serializedObject.FindProperty("_isNormalEnabled");
            _rendererProperty = serializedObject.FindProperty("_renderer");
            _materialIndexProperty = serializedObject.FindProperty("_materialIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            CustomInspectorTop();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            CustomInspectorMid();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            CustomInspectorBottom();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void CustomInspectorTop()
        {
            EditorGUILayout.PropertyField(_colorProperty);
            if (_isTextureEnabledProperty.boolValue) EditorGUILayout.PropertyField(_textureProperty);

            EditorGUILayout.PropertyField(_metallicProperty);
            EditorGUILayout.PropertyField(_smoothnessProperty);

            if (_isNormalEnabledProperty.boolValue) EditorGUILayout.PropertyField(_normalProperty);
        }

        protected virtual void CustomInspectorMid()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Texture");
            EditorGUILayout.PropertyField(_isTextureEnabledProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Normal");
            EditorGUILayout.PropertyField(_isNormalEnabledProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        private void CustomInspectorBottom()
        {
            EditorGUILayout.PropertyField(_materialIndexProperty);
            EditorGUILayout.PropertyField(_rendererProperty);
        }

        #endregion
    }
}
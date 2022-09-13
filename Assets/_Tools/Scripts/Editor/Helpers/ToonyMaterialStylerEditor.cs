using UnityEditor;
using UnityEngine;

namespace _Tools.Helpers
{
    [CustomEditor(typeof(ToonyMaterialStyler))]
    public class ToonyMaterialStylerEditor : MaterialStylerEditor
    {
        #region Variables

        private SerializedProperty _hasOutlineProperty;
        private SerializedProperty _outlineColorProperty;
        private SerializedProperty _outlineWidthProperty;

        #endregion

        #region Override Methods

        protected override void Init()
        {
            base.Init();

            _hasOutlineProperty = serializedObject.FindProperty("_hasOutline");
            _outlineColorProperty = serializedObject.FindProperty("_outlineColor");
            _outlineWidthProperty = serializedObject.FindProperty("_outlineWidth");
        }

        protected override void CustomInspectorTop()
        {
            base.CustomInspectorTop();

            if (!_hasOutlineProperty.boolValue) return;
            
            EditorGUILayout.PropertyField(_outlineColorProperty);
            EditorGUILayout.PropertyField(_outlineWidthProperty);
        }

        protected override void CustomInspectorMid()
        {
            base.CustomInspectorMid();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Outline");
            EditorGUILayout.PropertyField(_hasOutlineProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}
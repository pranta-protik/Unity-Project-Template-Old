using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace _Tools.Helpers
{
    [CustomEditor(typeof(MaterialPropertyOverrideAsset))]
    public class MaterialPropertyOverrideAssetEditor : Editor
    {
        private bool _canShowAll;

        public override void OnInspectorGUI()
        {
            var materialPropertyOverrideAsset = target as MaterialPropertyOverrideAsset;
            if (!materialPropertyOverrideAsset) return;

            EditorGUILayout.Space();

            var headStyle = new GUIStyle("ShurikenModuleTitle")
            {
                fixedHeight = 20.0f,
                contentOffset = new Vector2(5, -2),
                font = EditorStyles.boldFont
            };

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_shader"));

            serializedObject.ApplyModifiedProperties();

            if (!materialPropertyOverrideAsset.Shader)
            {
                EditorGUILayout.HelpBox("No shader selected!", MessageType.Error);
                return;
            }

            EditorGUILayout.Space();

            var rect = GUILayoutUtility.GetRect(16f, 22f, headStyle);

            GUI.Box(rect, "Properties:", headStyle);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            _canShowAll = GUILayout.Toggle(_canShowAll, "Show All", "Button");

            GUILayout.EndHorizontal();
            EditorGUILayout.Space();

            var hasChanged = MaterialPropertyOverrideEditor.DrawOverrideGUI(
                materialPropertyOverrideAsset.Shader,
                materialPropertyOverrideAsset.PropertyOverrides,
                _canShowAll,
                materialPropertyOverrideAsset);

            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Select all affected objects in scene", GUILayout.Height(30), GUILayout.Width(250)))
            {
                Selection.activeObject = null;

                var gameObjects = new List<Object>();

                foreach (var materialPropertyOverride in FindObjectsOfType<MaterialPropertyOverride>())
                {
                    foreach (var materialOverride in materialPropertyOverride.MaterialOverrides)
                    {
                        if (materialOverride.propertyOverrideAsset != materialPropertyOverrideAsset) continue;

                        gameObjects.Add(materialPropertyOverride.gameObject);
                        break;
                    }
                }

                Selection.objects = gameObjects.ToArray();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (hasChanged)
            {
                foreach (var materialPropertyOverride in FindObjectsOfType<MaterialPropertyOverride>())
                {
                    foreach (var materialOverride in materialPropertyOverride.MaterialOverrides)
                    {
                        if (materialOverride.propertyOverrideAsset != materialPropertyOverrideAsset) continue;

                        materialPropertyOverride.ClearMaterialProperties();
                        materialPropertyOverride.ApplyMaterialProperties();
                        break;
                    }
                }

                SceneView.RepaintAll();
            }

            if (hasChanged) EditorUtility.SetDirty(materialPropertyOverrideAsset);
        }
    }
}
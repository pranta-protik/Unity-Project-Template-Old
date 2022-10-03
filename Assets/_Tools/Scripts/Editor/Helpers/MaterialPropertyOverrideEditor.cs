using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Tools.Helpers
{
    [CustomEditor(typeof(MaterialPropertyOverride))]
    [CanEditMultipleObjects]
    public class MaterialPropertyOverrideEditor : Editor
    {
        private struct ShaderPropertyInfo
        {
            public string property;
            public ShaderPropertyType type;
            public string description;
            public float rangeMin;
            public float rangeMax;

        }

        private static readonly Dictionary<int, List<ShaderPropertyInfo>> _shaderPropertyDict = new();

        private static List<ShaderPropertyInfo> GetShaderProperties(Shader shader)
        {
            if (_shaderPropertyDict.ContainsKey(shader.GetInstanceID())) return _shaderPropertyDict[shader.GetInstanceID()];

            var propertyInfo = new List<ShaderPropertyInfo>();
            var propertyCount = ShaderUtil.GetPropertyCount(shader);

            for (var i = 0; i < propertyCount; i++)
            {
                var shaderProperty = new ShaderPropertyInfo
                {
                    property = ShaderUtil.GetPropertyName(shader, i),
                    type = (ShaderPropertyType)ShaderUtil.GetPropertyType(shader, i),
                    description = ShaderUtil.GetPropertyDescription(shader, i)
                };
                if (shaderProperty.type == ShaderPropertyType.Range)
                {
                    shaderProperty.rangeMin = ShaderUtil.GetRangeLimits(shader, i, 1);
                    shaderProperty.rangeMax = ShaderUtil.GetRangeLimits(shader, i, 2);
                }

                propertyInfo.Add(shaderProperty);
            }

            return _shaderPropertyDict[shader.GetInstanceID()] = propertyInfo;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var materialPropertyOverride = target as MaterialPropertyOverride;
            if (materialPropertyOverride == null) return;

            EditorGUILayout.Space();

            var headerStyle = new GUIStyle("ShurikenModuleTitle")
            {
                fixedHeight = 20f,
                contentOffset = new Vector2(5f, -2f),
                font = EditorStyles.boldFont
            };

            var renderedHeader = GUILayoutUtility.GetRect(20f, 22f, headerStyle);
            GUI.Box(renderedHeader, "Affected Renderers", headerStyle);

            EditorGUI.indentLevel += 1;
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_renderer"), true);

            if (EditorGUI.EndChangeCheck())
            {
                materialPropertyOverride.ClearMaterialProperties();

                serializedObject.ApplyModifiedProperties();

                materialPropertyOverride.ClearMaterialProperties();
                materialPropertyOverride.ApplyMaterialProperties();
            }

            EditorGUI.indentLevel -= 1;

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Find Renderer in GameObject", "Button"))
            {
                Undo.RecordObject(materialPropertyOverride, "Find");
                materialPropertyOverride.FindRenderer();
            }

            GUILayout.EndHorizontal();

            if (materialPropertyOverride.Renderer == null)
            {
                EditorGUILayout.HelpBox("No renderers affected!", MessageType.Error);
                return;
            }

            var materials = new List<Material>();

            foreach (var sharedMaterial in materialPropertyOverride.Renderer.sharedMaterials)
            {
                if (sharedMaterial == null) continue;
                if (materials.Contains(sharedMaterial)) continue;
                materials.Add(sharedMaterial);
            }

            foreach (var mat in materials)
            {
                if (materialPropertyOverride.MaterialOverrides.Find(x => x.material == mat) != null) continue;

                var materialOverride = new MaterialPropertyOverride.MaterialOverride
                {
                    material = mat
                };
                materialPropertyOverride.MaterialOverrides.Add(materialOverride);
            }

            materialPropertyOverride.MaterialOverrides.RemoveAll(x => !materials.Contains(x.material));

            var hasChanged = false;

            for (var i = 0; i < materialPropertyOverride.MaterialOverrides.Count; i++)
            {
                var materialOverride = materialPropertyOverride.MaterialOverrides[i];

                EditorGUILayout.Space();
                renderedHeader = GUILayoutUtility.GetRect(16f, 22f, headerStyle);
                GUI.Box(renderedHeader, "Material: " + materialOverride.material.name + " (" + materialOverride.material.shader.name + ")",
                    headerStyle);

                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                var activeProperty = serializedObject.FindProperty("_materialOverrides.Array.data[" + i + "].isActive");
                if (activeProperty != null) EditorGUILayout.PropertyField(activeProperty, new GUIContent("Active"));

                if (EditorGUI.EndChangeCheck())
                {
                    hasChanged = true;
                    serializedObject.ApplyModifiedProperties();
                }

                GUILayout.FlexibleSpace();

                if (materialOverride.isActive)
                {
                    materialOverride.canShowAll = GUILayout.Toggle(materialOverride.canShowAll, "Show All", "Button", GUILayout.Width(70f));
                }

                EditorGUILayout.EndHorizontal();

                if (materialOverride.isActive)
                {
                    EditorGUILayout.Space();
                
                    EditorGUI.BeginChangeCheck();
                    var property = serializedObject.FindProperty("_materialOverrides.Array.data[" + i + "].propertyOverrideAsset");
                    EditorGUILayout.PropertyField(property, new GUIContent("Property override: asset"));
                    if (EditorGUI.EndChangeCheck())
                    {
                        serializedObject.ApplyModifiedProperties();
                    }

                    if (materialOverride.propertyOverrideAsset && materialOverride.propertyOverrideAsset.Shader != materialOverride.material.shader)
                    {
                        EditorGUILayout.HelpBox("Shader mismatch. The selected override asset does not match this material's shader.", MessageType.Error);
                    }
                    
                    if (Selection.gameObjects.Length > 1)
                    {
                        EditorGUILayout.HelpBox("Multi editing not supported", MessageType.Info);
                    }
                    else
                    {
                        hasChanged |= DrawOverrideGUI(materialOverride.material.shader, materialOverride.propertyOverrides,
                            materialOverride.canShowAll, materialPropertyOverride);
                    }
                }
            }

            if (!hasChanged) return;

            materialPropertyOverride.ClearMaterialProperties();
            materialPropertyOverride.ApplyMaterialProperties();
        }

        public static bool DrawOverrideGUI(
            Shader shader,
            List<MaterialPropertyOverride.ShaderPropertyValue> propertyOverrides,
            bool canShowAll,
            Object target)
        {
            var shaderProperties = GetShaderProperties(shader);

            foreach (var shaderProperty in shaderProperties)
            {
                var propertyOverride = propertyOverrides.Find(x => x.propertyName == shaderProperty.property);
                bool hasPropertyOverride = propertyOverride != null;
                if (!hasPropertyOverride && !canShowAll) continue;

                GUILayout.BeginHorizontal();
                var isButtonPressed = false;

                if (canShowAll) isButtonPressed = GUILayout.Button(hasPropertyOverride ? "-" : "+", GUILayout.Width(20f));
                var description = new GUIContent(shaderProperty.description, shaderProperty.property);

                if (!hasPropertyOverride)
                {
                    GUILayout.Label(description);
                    if (isButtonPressed)
                    {
                        var shaderPropertyValue = new MaterialPropertyOverride.ShaderPropertyValue
                        {
                            type = shaderProperty.type,
                            propertyName = shaderProperty.property,
                        };

                        Undo.RecordObject(target, "Override");
                        propertyOverrides.Add(shaderPropertyValue);
                    }
                }
                else
                {
                    Undo.RecordObject(target, "Override");
                    switch (shaderProperty.type)
                    {
                        case ShaderPropertyType.Color:
                            propertyOverride.colorValue = EditorGUILayout.ColorField(description, propertyOverride.colorValue, false, true, true);
                            break;
                        case ShaderPropertyType.Float:
                            propertyOverride.floatValue = EditorGUILayout.FloatField(description, propertyOverride.floatValue);
                            break;
                        case ShaderPropertyType.Range:
                            propertyOverride.floatValue = EditorGUILayout.Slider(description, propertyOverride.floatValue, shaderProperty.rangeMin,
                                shaderProperty.rangeMax);
                            break;
                        case ShaderPropertyType.Vector:
                            propertyOverride.vectorValue = EditorGUILayout.Vector4Field(description, propertyOverride.vectorValue);
                            break;
                        case ShaderPropertyType.TexEnv:
                            propertyOverride.textureValue =
                                (Texture)EditorGUILayout.ObjectField(description, propertyOverride.textureValue, typeof(Texture), false);
                            break;
                        default:
                            Debug.Log("No property field found");
                            break;
                    }

                    if (isButtonPressed) propertyOverrides.Remove(propertyOverride);
                }

                GUILayout.EndHorizontal();
            }

            return GUI.changed;
        }
    }
}
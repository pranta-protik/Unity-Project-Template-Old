using System.Collections.Generic;
using UnityEngine;

namespace _Tools.Helpers
{
    [CreateAssetMenu(fileName = "MaterialPropertyOverridePreset", menuName = "Scriptable Objects/Presets/MaterialPropertyOverridePreset")]
    public class MaterialPropertyOverridePreset : ScriptableObject
    {
        [SerializeField] private Shader _shader;
        [SerializeField] private List<MaterialPropertyOverride.ShaderPropertyValue> _propertyOverrides = new();

        public Shader Shader => _shader;
        public List<MaterialPropertyOverride.ShaderPropertyValue> PropertyOverrides => _propertyOverrides;
    }
}
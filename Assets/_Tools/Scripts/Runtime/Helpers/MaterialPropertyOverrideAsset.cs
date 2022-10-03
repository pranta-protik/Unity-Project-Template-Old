using System.Collections.Generic;
using UnityEngine;

namespace _Tools.Helpers
{
    [CreateAssetMenu(fileName = "MaterialPropertyOverrideAsset", menuName = "Scriptable Objects/Assets/MaterialPropertyOverrideAsset")]
    public class MaterialPropertyOverrideAsset : ScriptableObject
    {
        [SerializeField] private Shader _shader;
        [SerializeField] private List<MaterialPropertyOverride.ShaderPropertyValue> _propertyOverrides = new();

        public Shader Shader => _shader;
        public List<MaterialPropertyOverride.ShaderPropertyValue> PropertyOverrides => _propertyOverrides;
    }
}
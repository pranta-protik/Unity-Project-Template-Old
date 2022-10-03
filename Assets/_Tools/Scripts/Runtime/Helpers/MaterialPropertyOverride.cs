using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Tools.Helpers
{
    public enum ShaderPropertyType
    {
        Color = 0,
        Vector = 1,
        Float = 2,
        Range = 3,
        TexEnv = 4
    }
    
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class MaterialPropertyOverride : MonoBehaviour
    {
        [Serializable]
        public class ShaderPropertyValue
        {
            public string propertyName;
            public ShaderPropertyType type;
            public Color colorValue = Color.white;
            public float floatValue;
            public Vector4 vectorValue;
            public Texture textureValue;
        }
        
        [Serializable]
        public class MaterialOverride
        {
            public bool isActive;
            [NonSerialized] public bool canShowAll;
            public Material material;
            public List<ShaderPropertyValue> propertyOverrides = new();
            [NonSerialized] public Dictionary<string, ShaderPropertyValue> propertyValueDict = new();
            public MaterialPropertyOverrideAsset propertyOverrideAsset;
        }

        #region Variables

        [SerializeField] private List<MaterialOverride> _materialOverrides = new();
        [SerializeField] private Renderer _renderer;

        private MaterialPropertyBlock _mpb;

        #endregion

        #region Properties

        private MaterialPropertyBlock Mpb => _mpb ??= new MaterialPropertyBlock();
        public List<MaterialOverride> MaterialOverrides => _materialOverrides;
        public Renderer Renderer => _renderer;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            foreach (var materialOverride in _materialOverrides)
            {
                foreach (var propertyOverride in materialOverride.propertyOverrides)
                {
                    materialOverride.propertyValueDict[propertyOverride.propertyName] = propertyOverride;
                }
            }

            ApplyMaterialProperties();
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            if (Application.isPlaying) return;
            ApplyMaterialProperties();
        }

        private void OnDisable()
        {
            if (Application.isPlaying) return;
            ClearMaterialProperties();
        }

        private void OnValidate()
        {
            if (Application.isPlaying) return;
            ClearMaterialProperties();
            ApplyMaterialProperties();
        }
#endif

        #endregion

        #region Custom Methods

#if UNITY_EDITOR
        public void FindRenderer() => _renderer = GetComponent<Renderer>();
#endif

        public void ClearMaterialProperties()
        {
            if (!_renderer) return;
            _renderer.SetPropertyBlock(null);
            for (int i = 0, c = _renderer.sharedMaterials.Length; i < c; i++)
            {
                _renderer.SetPropertyBlock(null, i);
            }
        }

        public void ApplyMaterialProperties()
        {
            if (!_renderer) return;
            if (_renderer.sharedMaterials.Length == 0) return;

            var allMatSame = true;

            for (int i = 1, c = _renderer.sharedMaterials.Length; i < c; i++)
            {
                if (_renderer.sharedMaterials[i] == _renderer.sharedMaterials[0]) continue;

                allMatSame = false;
                break;
            }

            if (allMatSame)
            {
                Mpb.Clear();
                var materialOverride = _materialOverrides.Find(x => x.material == _renderer.sharedMaterials[0]);
                if (materialOverride == null || materialOverride.isActive == false)
                {
                    _renderer.SetPropertyBlock(null);
                }
                else
                {
                    if (materialOverride.propertyOverrideAsset) ApplyOverrides(Mpb, materialOverride.propertyOverrideAsset.PropertyOverrides);
                    
                    ApplyOverrides(Mpb, materialOverride.propertyOverrides);
                    _renderer.SetPropertyBlock(Mpb);
                }
            }
            else
            {
                for (int i = 0, c = _renderer.sharedMaterials.Length; i < c; i++)
                {
                    var materialOverride = _materialOverrides.Find(x => x.material == _renderer.sharedMaterials[i]);
                    if (materialOverride == null || materialOverride.isActive == false)
                    {
                        _renderer.SetPropertyBlock(null, i);
                    }

                    else
                    {
                        Mpb.Clear();
                        
                        if (materialOverride.propertyOverrideAsset) ApplyOverrides(Mpb, materialOverride.propertyOverrideAsset.PropertyOverrides);
                        
                        ApplyOverrides(Mpb, materialOverride.propertyOverrides);
                        _renderer.SetPropertyBlock(Mpb, i);
                    }
                }
            }
        }

        private void ApplyOverrides(MaterialPropertyBlock mpb, List<ShaderPropertyValue> shaderPropertyValues)
        {
            foreach (var shaderPropertyValue in shaderPropertyValues)
            {
                switch (shaderPropertyValue.type)
                {
                    case ShaderPropertyType.Color:
                        mpb.SetColor(shaderPropertyValue.propertyName, shaderPropertyValue.colorValue);
                        break;
                    case ShaderPropertyType.Float:
                    case ShaderPropertyType.Range:
                        mpb.SetFloat(shaderPropertyValue.propertyName, shaderPropertyValue.floatValue);
                        break;
                    case ShaderPropertyType.Vector:
                        mpb.SetVector(shaderPropertyValue.propertyName, shaderPropertyValue.vectorValue);
                        break;
                    case ShaderPropertyType.TexEnv:
                        if (shaderPropertyValue.textureValue) mpb.SetTexture(shaderPropertyValue.propertyName, shaderPropertyValue.textureValue);
                        break;
                    default:
#if UNITY_EDITOR
                        Debug.Log("Shader property type not found");
#endif

                        break;
                }
            }
        }

        #endregion

        #region Helper Methods

        public Color GetColorValue(string propertyName, int materialIndex)
        {
            return !_materialOverrides[materialIndex].propertyValueDict.ContainsKey(propertyName)
                ? Color.clear
                : _materialOverrides[materialIndex].propertyValueDict[propertyName].colorValue;
        }

        public void SetColorProperty(string propertyName, Color color, int materialIndex)
        {
            if (!_materialOverrides[materialIndex].propertyValueDict.ContainsKey(propertyName)) return;

            _materialOverrides[materialIndex].propertyValueDict[propertyName].colorValue = color;
            ApplyMaterialProperties();
        }

        public Vector4 GetVectorValue(string propertyName, int materialIndex)
        {
            return !_materialOverrides[materialIndex].propertyValueDict.ContainsKey(propertyName)
                ? Vector4.zero
                : _materialOverrides[materialIndex].propertyValueDict[propertyName].vectorValue;
        }

        public void SetVectorProperty(string propertyName, Vector4 vector, int materialIndex)
        {
            if (!_materialOverrides[materialIndex].propertyValueDict.ContainsKey(propertyName)) return;

            _materialOverrides[materialIndex].propertyValueDict[propertyName].vectorValue = vector;
            ApplyMaterialProperties();
        }

        public float GetFloatValue(string propertyName, int materialIndex)
        {
            return !_materialOverrides[materialIndex].propertyValueDict.ContainsKey(propertyName)
                ? 0f
                : _materialOverrides[materialIndex].propertyValueDict[propertyName].floatValue;
        }

        public void SetFloatProperty(string propertyName, float value, int materialIndex)
        {
            if (!_materialOverrides[materialIndex].propertyValueDict.ContainsKey(propertyName)) return;

            _materialOverrides[materialIndex].propertyValueDict[propertyName].floatValue = value;
            ApplyMaterialProperties();
        }

        public Texture GetTextureValue(string propertyName, int materialIndex)
        {
            return !_materialOverrides[materialIndex].propertyValueDict.ContainsKey(propertyName)
                ? null
                : _materialOverrides[materialIndex].propertyValueDict[propertyName].textureValue;
        }

        public void SetTextureProperty(string propertyName, Texture texture, int materialIndex)
        {
            if (!_materialOverrides[materialIndex].propertyValueDict.ContainsKey(propertyName)) return;

            _materialOverrides[materialIndex].propertyValueDict[propertyName].textureValue = texture;
            ApplyMaterialProperties();
        }

        #endregion
    }
}
using UnityEngine;

namespace _Tools.Helpers
{
    [ExecuteAlways]
    public class MaterialStyler : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Color _materialColor = Color.white;
        [SerializeField] private Texture2D _materialTexture;
        [SerializeField] [Range(0f, 1f)] private float _materialMetallic;
        [SerializeField] [Range(0f, 1f)] private float _materialSmoothness = 0.5f;
        [SerializeField] private Texture2D _materialNormal;
        [SerializeField] private bool _isTextureEnabled;
        [SerializeField] private bool _isNormalEnabled;
        [SerializeField] private int _materialIndex;
        [SerializeField] private Renderer _renderer;

        private MaterialPropertyBlock _mpb;
        private static readonly int MatColor = Shader.PropertyToID("_Color");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int Metallic = Shader.PropertyToID("_Metallic");
        private static readonly int Glossiness = Shader.PropertyToID("_Glossiness");
        private static readonly int BumpMap = Shader.PropertyToID("_BumpMap");

        #endregion

        #region Properties

        private MaterialPropertyBlock Mpb => _mpb ??= new MaterialPropertyBlock();

        #endregion

        #region Unity Methods

        private void Awake() => FindRenderer();

#if UNITY_EDITOR
        private void OnEnable()
        {
            FindRenderer();
            TryApplyStyle();
        }

        private void OnValidate()
        {
            FindRenderer();
            TryApplyStyle();
        }
#endif

        #endregion

        #region Custom Methods

        private void TryApplyStyle()
        {
            if (!_isTextureEnabled) _materialTexture = null;
            if (!_isNormalEnabled) _materialNormal = null;
            
            if (_materialTexture) Mpb.SetTexture(MainTex, _materialTexture);

            if (_materialNormal)
            {
                _renderer.sharedMaterials[_materialIndex].EnableKeyword("NORMALMAP");
                Mpb.SetTexture(BumpMap, _materialNormal);
            }

            if (!_materialTexture && !_materialNormal) Mpb.Clear();
            
            Mpb.SetColor(MatColor, _materialColor);
            Mpb.SetFloat(Metallic, _materialMetallic);
            Mpb.SetFloat(Glossiness, _materialSmoothness);
            if(_renderer) _renderer.SetPropertyBlock(Mpb, _materialIndex);
        }

        public void SetColor(Color matColor)
        {
            _materialColor = matColor;
            TryApplyStyle();
        }

        private void FindRenderer() => _renderer = GetComponent<Renderer>();

        #endregion
    }
}
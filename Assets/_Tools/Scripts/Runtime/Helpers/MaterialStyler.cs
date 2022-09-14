using UnityEngine;

namespace _Tools.Helpers
{
    [ExecuteAlways]
    public abstract class MaterialStyler : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Color _color = Color.white;
        [SerializeField] private Texture2D _texture;
        [SerializeField] [Range(0f, 1f)] private float _metallic;
        [SerializeField] [Range(0f, 1f)] private float _smoothness = 0.5f;
        [SerializeField] private Texture2D _normal;
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

        protected MaterialPropertyBlock Mpb => _mpb ??= new MaterialPropertyBlock();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            FindRenderer();
            TryApplyStyle();
        }

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
            ConfigureStyle();
            if(_renderer) _renderer.SetPropertyBlock(Mpb, _materialIndex);
        }

        protected virtual void ConfigureStyle()
        {
            if (!_isTextureEnabled) _texture = null;
            if (!_isNormalEnabled) _normal = null;
            
            if (_texture) Mpb.SetTexture(MainTex, _texture);

            if (_normal)
            {
                _renderer.sharedMaterials[_materialIndex].EnableKeyword("NORMALMAP");
                Mpb.SetTexture(BumpMap, _normal);
            }

            if (!_texture && !_normal) Mpb.Clear();
            
            Mpb.SetColor(MatColor, _color);
            Mpb.SetFloat(Metallic, _metallic);
            Mpb.SetFloat(Glossiness, _smoothness);
        }

        public void SetColor(Color matColor)
        {
            _color = matColor;
            TryApplyStyle();
        }

        private void FindRenderer() => _renderer = GetComponent<Renderer>();

        #endregion
    }
}
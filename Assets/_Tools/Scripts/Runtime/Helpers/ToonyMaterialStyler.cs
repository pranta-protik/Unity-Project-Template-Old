using UnityEngine;

namespace _Tools.Helpers
{
    public class ToonyMaterialStyler : MaterialStyler
    {
        #region Variables

        [SerializeField] private bool _hasOutline;
        [SerializeField] private Color _outlineColor = Color.black;
        [SerializeField, Range(0f, 50f)] private float _outlineWidth = 1f;
        
        private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
        private static readonly int Outline = Shader.PropertyToID("_Outline");

        #endregion

        #region Override Methods

        protected override void ConfigureStyle()
        {
            base.ConfigureStyle();

            if (!_hasOutline) return;
            
            Mpb.SetColor(OutlineColor, _outlineColor);
            Mpb.SetFloat(Outline, _outlineWidth);
        }

        #endregion
    }
}
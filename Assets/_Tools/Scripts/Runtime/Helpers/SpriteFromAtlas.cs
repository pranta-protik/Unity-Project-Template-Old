using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace _Tools.Helpers
{
    public class SpriteFromAtlas : MonoBehaviour
    {
        #region Variables

        [SerializeField] private SpriteAtlas _spriteAtlas;
        [SerializeField] private string _spriteName;
        [SerializeField] private Image _image;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        #endregion

        #region Properties

        public Image Image => _image;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        #endregion
        
        #region Unity Methods

        private void Awake() => TryAssignSprite();

        #endregion

        #region Custom Methods

        public void TryAssignSprite()
        {
            if(!_spriteAtlas) return;
            
            if (_image) _image.sprite = _spriteAtlas.GetSprite(_spriteName);
            if (_spriteRenderer) _spriteRenderer.sprite = _spriteAtlas.GetSprite(_spriteName);
        }

        #endregion
    }
}
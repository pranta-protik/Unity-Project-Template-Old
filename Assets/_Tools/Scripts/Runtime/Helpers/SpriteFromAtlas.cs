using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace _Tools.Helpers
{
    public class SpriteFromAtlas : MonoBehaviour
    {
        #region Variables

        [SerializeField] private string _spriteName;
        [SerializeField] private SpriteAtlas _spriteAtlas;

        #endregion
        
        #region Unity Methods

        private void Awake() => TryAssignSprite();

        #endregion

        #region Custom Methods

        public void TryAssignSprite()
        {
            if(!_spriteAtlas) return;
            if (TryGetComponent(out Image _image)) _image.sprite = _spriteAtlas.GetSprite(_spriteName);
        }

        #endregion
    }
}
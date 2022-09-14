using _Game.Managers;
using _Tools.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI
{
    public class HurtScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image _hurtImage;
        
        private bool _isInTransition;

        #endregion

        #region Unity Methods

        private void Awake() => DisableHurtScreen();

        private void Start()
        {
            if(UIManager.Instance.IsNotNull(nameof(UIManager))) UIManager.Instance.OnHurt += UIManager_OnHurt;
        }
        
        private void OnDestroy()
        {
            _hurtImage.DOKill();
            if (UIManager.Instance) UIManager.Instance.OnHurt -= UIManager_OnHurt;
        }

        #endregion
        
        #region Custom Methods

        private void UIManager_OnHurt(float duration) => FlashHurtScreen(duration);
        
        private void FlashHurtScreen(float duration)
        {
            if(_isInTransition) return;

            _isInTransition = true;
            
            EnableHurtScreen();
            _hurtImage.DOFade(1f, duration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear).OnComplete(() =>
            {
                DisableHurtScreen();
                _isInTransition = false;
            });
        }

        private void EnableHurtScreen() => _hurtImage.gameObject.SetActive(true);
        private void DisableHurtScreen() => _hurtImage.gameObject.SetActive(false);

        #endregion
    }
}
using System;
using _Game.Managers;
using _Tools.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI
{
    public class LevelFailPanel : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image _transitionImage;

        #endregion

        #region Unity Methods

        private void Awake() => DisablePanel();

        private void Start()
        {
            if(UIManager.Instance.IsNotNull(nameof(UIManager))) UIManager.Instance.OnLevelFail += UIManager_OnLevelFail;
        }

        private void OnDestroy()
        {
            _transitionImage.DOKill();
            if (UIManager.Instance) UIManager.Instance.OnLevelFail -= UIManager_OnLevelFail;
        }

        #endregion

        #region Custom Methods

        private void UIManager_OnLevelFail(float duration, Action onTransitionComplete) => LevelReloadTransition(duration, onTransitionComplete);
        
        private void LevelReloadTransition(float duration, Action onTransitionComplete)
        {
            EnablePanel();
            _transitionImage.DOFade(1f, duration).SetEase(Ease.Linear).OnComplete(() => { onTransitionComplete(); });
        }

        private void EnablePanel() => _transitionImage.gameObject.SetActive(true);
        private void DisablePanel() => _transitionImage.gameObject.SetActive(false);

        #endregion
    }
}
using DG.Tweening;
using UnityEngine;

namespace _Game.UI
{
    public class HintText : MonoBehaviour
    {
        #region Variables

        [SerializeField, Range(1f, 5f)] private float _targetScaleFactor = 1.2f;
        [SerializeField, Min(0f)] private float _scaleDuration = 0.5f;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            transform.DOScale(transform.localScale * _targetScaleFactor, _scaleDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

        private void OnDisable() => transform.DOKill();

        #endregion
    }
}
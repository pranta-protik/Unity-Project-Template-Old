using DG.Tweening;
using UnityEngine;

namespace _Game.UI
{
    public class HintHand : MonoBehaviour
    {
        #region Variables

        [SerializeField, Min(0f)] private float _targetPositionOnX;
        [SerializeField, Min(0f)] private float _moveDuration = 1f;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            transform.DOLocalMoveX(_targetPositionOnX, _moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

        private void OnDisable() => transform.DOKill();

        #endregion
    }
}
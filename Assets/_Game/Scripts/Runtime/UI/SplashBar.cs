using _Game.Helpers;
using _Tools.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI
{
    public class SplashBar : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image _fillMask;
        [SerializeField, Tooltip("Loading Time In Seconds"), Range(1f, 10f)] private float _loadingTime = 3f;

        private float _startTime;
        private bool _isLoading;
		
        #endregion
		
        #region Unity Methods
		
        private void Awake()
        {
            _startTime = 0f;
            _isLoading = true;
            _fillMask.fillAmount = 0f;
        }

        private void Update()
        {
            if (_startTime >= _loadingTime && _isLoading)
            {
                _isLoading = false;
                SceneUtils.LoadSpecificScene((int) SceneIndexes.GAME);
            }
            else
            {
                _startTime += Time.deltaTime;
                _fillMask.fillAmount = _startTime / _loadingTime;
            }
        }
		
        #endregion
    }
}
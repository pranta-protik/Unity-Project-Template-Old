using _Game.Helpers;
using _Game.Managers;
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
        
#if UNITY_EDITOR
        private void Start()
        {
            if (SceneLoadManager.Instance) return;
            
            var sceneLoadManagerGO = new GameObject("SceneLoadManager");
            sceneLoadManagerGO.SetActive(false);
            sceneLoadManagerGO.AddComponent<SceneLoadManager>().EnableTestMode();
            sceneLoadManagerGO.SetActive(true);
        }
#endif

        private void Update()
        {
            if (_startTime >= _loadingTime && _isLoading)
            {
                _isLoading = false;
                SceneLoadManager.Instance.LoadSpecificScene((int) SceneIndexes.GAME);
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
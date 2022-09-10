using _Game.Helpers;
using _Tools.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Managers
{
    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
        #region Variables

        [SerializeField, Min(0)] private int _totalSceneCount;
        [SerializeField, Min(0)] private int _firstLevelSceneIndex = (int)SceneIndexes.GAME;
        [SerializeField] private bool _isTestEnabled;

        #endregion

        #region Properites

        public int TotalSceneCount => _totalSceneCount;
        public int FirstLevelSceneIndex => _firstLevelSceneIndex;

        #endregion

        #region Unity Methods

        protected override void OnAwake()
        {
            base.OnAwake();

            if (_isTestEnabled) return;

            if (PlayerPrefs.GetInt(ConstUtils.FIRST_TIME_PLAYING, 0) == 0)
            {
                SceneManager.LoadSceneAsync((int)SceneIndexes.SPLASH);
                PlayerPrefs.SetInt(ConstUtils.FIRST_TIME_PLAYING, 1);
            }
            else
            {
                LoadLastPlayedScene();
            }
        }

        #endregion

        #region Custom Methods

        private static void LoadLastPlayedScene()
        {
            SceneManager.LoadSceneAsync(PlayerPrefs.GetInt(ConstUtils.LAST_PLAYED_SCENE_INDEX, (int)SceneIndexes.GAME));
        }

        public void LoadSpecificScene(int sceneIndex) => SceneManager.LoadSceneAsync(sceneIndex);

        public void ReloadScene() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        public void LoadUIScene() => SceneManager.LoadSceneAsync((int)SceneIndexes.UI, LoadSceneMode.Additive);

        public void EnableTestMode() => _isTestEnabled = true;

        #endregion
    }
}
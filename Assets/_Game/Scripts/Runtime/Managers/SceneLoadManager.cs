using _Game.Helpers;
using _Tools.Helpers;
using UnityEngine;

namespace _Game.Managers
{
    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
        #region Variables

        [SerializeField, Min(0)] private int _totalSceneCount;
        [SerializeField, Min(0)] private int _firstLevelSceneIndex = (int)SceneIndex.GAME;

        #endregion

        #region Properites

        public int TotalSceneCount => _totalSceneCount;
        public int FirstLevelSceneIndex => _firstLevelSceneIndex;

        #endregion

        #region Unity Methods

        protected override void OnAwake()
        {
            base.OnAwake();
            
            if (PlayerPrefs.GetInt(ConstUtils.FIRST_TIME_PLAYING, 0) == 0)
            {
                SceneUtils.LoadSpecificScene((int)SceneIndex.SPLASH);
                PlayerPrefs.SetInt(ConstUtils.FIRST_TIME_PLAYING, 1);
            }
            else
            {
                SceneUtils.LoadSpecificScene(PlayerPrefs.GetInt(ConstUtils.LAST_PLAYED_SCENE_INDEX, (int)SceneIndex.GAME));
            }
        }
        
        #endregion

        #region Custom Methods

        public int GetNextSceneIndex()
        {
            var sceneIndex = PlayerPrefs.GetInt(ConstUtils.LAST_PLAYED_SCENE_INDEX, (int)SceneIndex.GAME);
            
            var inGameLevelCount = PlayerPrefs.GetInt(ConstUtils.IN_GAME_LEVEL_COUNT, 1);

            if (sceneIndex >= _totalSceneCount - 1)
            {
                sceneIndex = _firstLevelSceneIndex;
            }
            else
            {
                sceneIndex += 1;
            }
            
            PlayerPrefs.SetInt(ConstUtils.LAST_PLAYED_SCENE_INDEX, sceneIndex);
            PlayerPrefs.SetInt(ConstUtils.IN_GAME_LEVEL_COUNT, inGameLevelCount + 1);

            return sceneIndex;
        }

        #endregion
    }
}
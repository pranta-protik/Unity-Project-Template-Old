using _Game.Helpers;
using _Tools.Helpers;
using UnityEngine;

namespace _Game.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        #region Variables

        private int _totalSceneCount;
        private int _firstLevelSceneIndex;

        #endregion

        #region Unity Methods

        protected override void OnAwake()
        {
            base.OnAwake();

            _totalSceneCount = PlayerPrefs.GetInt(ConstUtils.TOTAL_SCENE_COUNT, 3);
            _firstLevelSceneIndex = PlayerPrefs.GetInt(ConstUtils.FIRST_LEVEL_SCENE_INDEX, (int)SceneIndex.GAME);
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
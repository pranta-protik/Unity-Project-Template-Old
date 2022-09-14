using _Game.Helpers;
using _Tools.Helpers;
using UnityEngine;

namespace _Game.Persistent
{
    public class Initializer : MonoBehaviour
    {
        #region Variables
        
        [SerializeField, Min(0)]private int _totalSceneCount;
        [SerializeField, Min(0)] private int _firstLevelSceneIndex = (int)SceneIndex.GAME;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            DontDestroyOnLoad(this);

            PlayerPrefs.SetInt(ConstUtils.TOTAL_SCENE_COUNT, _totalSceneCount);
            PlayerPrefs.SetInt(ConstUtils.FIRST_LEVEL_SCENE_INDEX, _firstLevelSceneIndex);
            
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
    }
}

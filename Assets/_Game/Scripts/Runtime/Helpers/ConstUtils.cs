using UnityEngine;

namespace _Game.Helpers
{
    public static class ConstUtils
    {
        #region PlayerPrefs Keys

        public const string FIRST_TIME_PLAYING = "FirstTimePlaying";
        public const string LAST_PLAYED_SCENE_INDEX = "LastPlayedSceneIndex";

        #endregion
        
        #region Numerical Values

        public const float KEYBOARD_ROTATION_THRESHOLD = 0.1f;
        public const float MOBILE_ROTATION_THRESHOLD = 5f;

        #endregion
    }
}
using System;
using _Tools.Helpers;

namespace _Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        #region Events

        public event Action OnLevelStart;

        #endregion

        #region Custom Methods

        public void LevelStart() => OnLevelStart?.Invoke();

        #endregion
    }
}
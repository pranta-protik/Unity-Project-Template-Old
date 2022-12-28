using System;
using System.Collections;
using _Tools.Helpers;
using UnityEngine;

namespace _Game.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        #region Events

        public event Action<float> OnHurt;
        public event Action<Action> OnLevelComplete; 
        public event Action<float, Action> OnLevelFail; 

        #endregion
        
        #region Custom Methods

        public void FlashHurtScreen(float duration) => OnHurt?.Invoke(duration);
        public void LevelComplete(float delay, Action onLoadScene) => StartCoroutine(LevelCompleteUIRoutine(delay, onLoadScene));
        
        private IEnumerator LevelCompleteUIRoutine(float delay, Action onLoadScene)
        {
            yield return new WaitForSeconds(delay);
            OnLevelComplete?.Invoke(onLoadScene);
        }
        
        public void LevelReloadTransition(float duration, Action onTransitionComplete) => OnLevelFail?.Invoke(duration, onTransitionComplete);

        #endregion
    }
}
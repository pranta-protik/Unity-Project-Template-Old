using System;
using _Game.Managers;
using _Tools.Extensions;
using UnityEngine;

namespace _Game.UI
{
    public class LevelCompletePanel : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private GameObject _uiPanel;

        private Action OnLoadScene;

        #endregion

        #region Unity Methods

        private void Awake() => DisableUIGroup();

        private void Start()
        {
            if(UIManager.Instance.IsNotNull(nameof(UIManager))) UIManager.Instance.OnLevelComplete += UIManager_OnLevelComplete;
        }

        private void OnDestroy()
        {
            if (UIManager.Instance) UIManager.Instance.OnLevelComplete -= UIManager_OnLevelComplete;
        }

        #endregion

        #region Custom Mehtods

        private void UIManager_OnLevelComplete(Action onLoadScene)
        {
            EnableUIGroup();
            OnLoadScene = onLoadScene;
        }

        private void EnableUIGroup() => _uiPanel.SetActive(true);
        private void DisableUIGroup() => _uiPanel.SetActive(false);

        public void LoadNextScene() => OnLoadScene();

        #endregion
    }
}
using _Tools.Extensions;
using _Game.Managers;
using UnityEngine;

namespace _Game.UI
{
    public class LevelStartPanel : MonoBehaviour
    {
        #region Custom Methods

        public void LevelStart()
        {
            if (GameManager.Instance.IsNotNull(nameof(GameManager))) GameManager.Instance.LevelStart();
            DisablePanel();
        }

        private void DisablePanel() => gameObject.SetActive(false);

        #endregion
    }
}
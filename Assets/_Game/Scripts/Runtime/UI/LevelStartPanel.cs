using UnityEngine;

namespace _Game.UI
{
    public class LevelStartPanel : MonoBehaviour
    {
        #region Custom Methods

        public void DisablePanel() => gameObject.SetActive(false);

        #endregion
    }
}
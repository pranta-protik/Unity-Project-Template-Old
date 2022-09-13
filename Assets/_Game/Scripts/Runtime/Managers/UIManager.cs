using _Game.Helpers;
using _Tools.Helpers;
using UnityEngine.SceneManagement;

namespace _Game.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        #region Unity Methods

        private void Start() => SceneUtils.LoadSpecificScene((int)SceneIndex.UI, LoadSceneMode.Additive);

        #endregion
    }
}
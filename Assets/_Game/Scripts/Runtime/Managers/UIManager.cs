using _Tools.Helpers;
using UnityEngine;

namespace _Game.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        #region Unity Methods

        private void Start()
        {
#if UNITY_EDITOR
            if (!SceneLoadManager.Instance)
            {
                var sceneLoadManagerGO = new GameObject("SceneLoadManager");
                sceneLoadManagerGO.SetActive(false);
                sceneLoadManagerGO.AddComponent<SceneLoadManager>().EnableTestMode();
                sceneLoadManagerGO.SetActive(true);   
            }
#endif
            SceneLoadManager.Instance.LoadUIScene();
        }

        #endregion
    }
}
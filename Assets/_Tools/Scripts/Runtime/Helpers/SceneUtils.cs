using UnityEngine.SceneManagement;

namespace _Tools.Helpers
{
    public static class SceneUtils
    {
        public static void LoadSpecificScene(int sceneIndex) => SceneManager.LoadSceneAsync(sceneIndex);
        public static void LoadSpecificScene(int sceneIndex, LoadSceneMode loadSceneMode) => SceneManager.LoadSceneAsync(sceneIndex, loadSceneMode);

        public static void ReloadScene() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
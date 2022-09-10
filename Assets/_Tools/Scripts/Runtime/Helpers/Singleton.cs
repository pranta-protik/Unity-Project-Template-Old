using UnityEngine;

namespace _Tools.Helpers
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        #region Variables

        [SerializeField] protected bool _dontDestroyOnLoad;

        #endregion

        #region Properties

        public static T Instance { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                DebugUtils.LogWarning($"There's more than one instance of {typeof(T).Name} in the scene!");
                Destroy(this.gameObject);
                return;
            }

            Instance = this as T;

            if (_dontDestroyOnLoad)
            {
                transform.parent = null;
                DontDestroyOnLoad(this);
            }
         
            OnAwake();
        }

        #endregion

        #region Custom Methods

        protected virtual void OnAwake()
        {
        }

        #endregion
    }
}
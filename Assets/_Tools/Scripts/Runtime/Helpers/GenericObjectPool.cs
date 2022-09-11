using System.Collections.Generic;
using UnityEngine;

namespace _Tools.Helpers
{
    public abstract class GenericObjectPool<T> : Singleton<GenericObjectPool<T>> where T: Component 
    {
        #region Variables

        [SerializeField] private int _initialPoolSize = 1;
        [SerializeField] private T _objectPrefab;

        private readonly Queue<T> _poolObjects = new Queue<T>();

        #endregion

        #region Custom Methods

        public T Get()
        {
            if (_poolObjects.Count == 0)
            {
                AddObjects(_initialPoolSize);
            }

            return _poolObjects.Dequeue();
        }

        public void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            _poolObjects.Enqueue(objectToReturn);
        }

        private void AddObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newObject = Instantiate(_objectPrefab);
                newObject.gameObject.SetActive(false);
                _poolObjects.Enqueue(newObject);
            }
        }

        #endregion
    }
}
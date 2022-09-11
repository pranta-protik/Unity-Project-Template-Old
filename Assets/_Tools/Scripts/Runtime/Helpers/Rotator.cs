using UnityEngine;

namespace _Tools.Helpers
{
    public class Rotator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Vector3 _rotationAxis = Vector3.forward;
        [SerializeField] [Range(1f, 100f)] private float _rotationSpeed = 50f;

        #endregion

        #region Unity Methods

        private void Update() => transform.Rotate(_rotationAxis * (_rotationSpeed * Time.deltaTime));

        #endregion
    }
}
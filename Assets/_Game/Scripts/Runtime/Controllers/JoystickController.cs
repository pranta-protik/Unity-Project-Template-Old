using _Tools.Helpers;
using UnityEngine;

namespace _Game.Controllers
{
    public class JoystickController : MonoBehaviour
    {
        #region Variables

        [Header("Controls")]
        [SerializeField] [Min(0f)] private float _speed = 5f;
        [SerializeField] [Range(0f, 1f)] private float _positionSmoothingFactor = 0.5f;
        [SerializeField] [Range(0f, 1f)] private float _rotationSmoothingFactor = 0.25f;
        [SerializeField] [Min(0f)] private float _turnThreshold = 10f;
        [SerializeField] [Min(0f)] private float _sensitivity = 1000f;

        [Header("Validity Check")] 
        [SerializeField] private Transform _rayOrigin;
        [SerializeField] [Min(0f)] private float _rayDistance = 10f;
        [SerializeField] private Color _rayColor = Color.red;
        [SerializeField] private LayerMask _groundLayerMask;

        private Vector3 _currentPosition;
        private Vector3 _startPosition;
        private Vector3 _direction;
        private Vector3 _currentDirection;
        private bool _canMove;
        private bool _canControl;

        #endregion

        #region Unity Methods

        private void Update()
        {
            if(_canControl) HandleControl();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_rayOrigin)
            {
                Gizmos.color = _rayColor;
                Gizmos.DrawRay(_rayOrigin.position, Vector3.down * _rayDistance);
                return;
            }
            
            DebugUtils.LogError($"Ray origin not found in {transform}!");
        }
#endif

        #endregion

        #region Custom Methods

        private void HandleControl()
        {
            _currentPosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(0)) _startPosition = _currentPosition;

            if (Input.GetMouseButton(0))
            {
                var distance = (_currentPosition - _startPosition).magnitude;

                if (distance > _turnThreshold)
                {
                    _canMove = true;

                    if (distance > _sensitivity) _startPosition = _currentPosition - _direction * _sensitivity;

                    _currentDirection = -(_startPosition - _currentPosition).normalized;
                    _direction.x = Mathf.Lerp(_direction.x, _currentDirection.x, _positionSmoothingFactor);
                    _direction.z = Mathf.Lerp(_direction.z, _currentDirection.y, _positionSmoothingFactor);

                    _direction.y = 0f;
                }
                else
                {
                    _canMove = false;
                }

                if (_direction != Vector3.zero)
                {
                    var targetRotation = Quaternion.LookRotation(_direction);

                    if (_canMove && transform.rotation != targetRotation)
                    {
                        var deltaRotation = Quaternion.Euler(new Vector3(0f, 360f, 0f) * Time.smoothDeltaTime);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * deltaRotation, _rotationSmoothingFactor);
                    }
                }

                if (_canMove)
                {
                    if (Physics.Raycast(_rayOrigin.position, Vector3.down, _rayDistance, _groundLayerMask))
                    {
                        var objectTransform = transform;
                        objectTransform.position += objectTransform.forward * _speed * Time.deltaTime;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0)) _canMove = false;
        }

#if UNITY_EDITOR
        [Sirenix.OdinInspector.Button, Sirenix.OdinInspector.PropertySpace]
        public void EnableControl() => _canControl = true;

        [Sirenix.OdinInspector.Button]
        public void DisableControl() => _canControl = false;
#endif

        #endregion
    }
}
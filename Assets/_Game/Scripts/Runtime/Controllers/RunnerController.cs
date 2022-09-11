using System;
using _Game.Helpers;
using _Tools.Helpers;
using DG.Tweening;
using UnityEngine;

namespace _Game.Controllers
{
    public class RunnerController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Transform _modelToRotate;

        [Header("Common Settings")] 
        [SerializeField] [Range(0f, 10f)] private float _horizontalRange = 2f;

        [SerializeField] [Range(0f, 360f)] private float _rotationRange = 7f;
        [SerializeField] [Min(0f)] private float _rotationDuration = 0.1f;

        [Header("Windows Settings")] 
        [SerializeField] [Min(0f)] private float _keyboardSpeed = 3f;

        [Header("Android/IOS Settings")] 
        [SerializeField] [Min(0f)] private float _touchSpeed = 0.1f;

        private float _xPosition;
        private bool _isTouchActive;
        private bool _canControl;

        #endregion

        #region Unity Methods

        private void Awake() => EnableControl();

        private void Update()
        {
            if (_canControl) HandleControl();
        }

        #endregion

        #region Custom Methods

        private void HandleControl()
        {
            HandleKeyboardControl();
            HandleTouchControl();
        }

        private void HandleKeyboardControl()
        {
            var horizontalInput = Input.GetAxis("Horizontal") * _keyboardSpeed * Time.deltaTime;
            var newPosition = transform.localPosition + Vector3.right * horizontalInput;

            newPosition.x = Mathf.Clamp(newPosition.x, -_horizontalRange, _horizontalRange);
            transform.localPosition = newPosition;

            HandleRotation(Input.GetAxis("Horizontal"), ConstUtils.KEYBOARD_ROTATION_THRESHOLD);
        }

        private void HandleTouchControl()
        {
            foreach (var touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (!_isTouchActive) _isTouchActive = true;
                        break;
                    case TouchPhase.Moved:
                        var deltaX = touch.deltaPosition.x;
                        _xPosition += (deltaX / Screen.width) * Time.deltaTime / _touchSpeed;
                        _xPosition = Mathf.Clamp(_xPosition, -_horizontalRange, _horizontalRange);
                        var objectTransform = transform;
                        var localPosition = objectTransform.localPosition;
                        objectTransform.localPosition = new Vector3(_xPosition, localPosition.y, localPosition.z);
                        HandleRotation(deltaX, ConstUtils.MOBILE_ROTATION_THRESHOLD);
                        break;
                    case TouchPhase.Ended:
                        _isTouchActive = false;
                        _modelToRotate.DOLocalRotate(Vector3.zero, _rotationDuration);
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        DebugUtils.LogError("Unknown touch state");
                        break;
                }
            }
        }

        private void HandleRotation(float inputValue, float threshold)
        {
            if (inputValue > threshold) SetRotation(new Vector3(0f, _rotationRange, 0f));
            if (inputValue < -threshold) SetRotation(new Vector3(0f, -_rotationRange, 0f));
            if (inputValue == 0) SetRotation(Vector3.zero);
        }

        private void SetRotation(Vector3 rotation) => _modelToRotate.DOLocalRotate(rotation, _rotationDuration);

#if UNITY_EDITOR
        [Sirenix.OdinInspector.Button, Sirenix.OdinInspector.PropertySpace]
        public void AssignFirstChildAsRotationModel() => _modelToRotate = transform.GetChild(0);
        [Sirenix.OdinInspector.Button]
#endif
        public void EnableControl() => _canControl = true;
#if UNITY_EDITOR
        [Sirenix.OdinInspector.Button]
#endif
        public void DisableControl() => _canControl = false;
        
        #endregion
    }
}
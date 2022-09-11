using _Tools.Extensions;
using PathCreation;
using UnityEngine;

namespace _Game.Controllers
{
    public class PathFollower : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PathCreator _path;
        [SerializeField] private EndOfPathInstruction _endOfPathInstruction = EndOfPathInstruction.Stop;
        [SerializeField] private float _speed = 5f;

        [SerializeField] [Tooltip("How fast full speed is achieved")] [Range(1f, 10f)]
        private float _speedMultiplier = 5f;

        private float _currentSpeed;
        private float _distanceTravelled;
        private bool _canFollow;

        #endregion

        #region Unity Methods

        private void Start()
        {
            StartFollowing();
            
            if (_path.IsNotNull(nameof(PathCreator), transform)) _path.pathUpdated += OnPathChanged;
        }

        private void Update()
        {
            if (_canFollow) FollowPath();
        }

        private void OnDestroy()
        {
            if (_path) _path.pathUpdated -= OnPathChanged;
        }

        #endregion

        #region Custom Methods

        private void FollowPath()
        {
            if (_currentSpeed < _speed)
            {
                _currentSpeed += _speedMultiplier * Time.deltaTime;
                _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, _speed);
            }

            _distanceTravelled += _currentSpeed * Time.deltaTime;
            transform.position = _path.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
            transform.rotation = _path.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
        }

        private void OnPathChanged() => _distanceTravelled = _path.path.GetClosestDistanceAlongPath(transform.position);

#if UNITY_EDITOR
        public void FindPathFromScene() => _path = FindObjectOfType<PathCreator>();
        
        [Sirenix.OdinInspector.Button, Sirenix.OdinInspector.PropertySpace]
#endif
        public void StartFollowing() => _canFollow = true;
        
#if UNITY_EDITOR
        [Sirenix.OdinInspector.Button]
#endif
        public void StopFollowing() => _canFollow = false;
        
        #endregion
    }
}
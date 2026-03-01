using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Asteroids.CodeBase.Gameplay.Asteroid
{
    public class AsteroidMovement : MonoBehaviour
    {
        [SerializeField, Required] private CircleCollider2D _circleCollider;
        [SerializeField, Required] private Rigidbody2D _rigidbody;

        private bool _isEnteredToMap;
        private Vector3 _velocity;
        private float _rotationSpeed;
        private GameMapService _gameMapService;

        public void Initialize(
            Vector2 startPosition,
            float startRotation,
            Vector2 moveDirection,
            float moveSpeed,
            float rotationSpeed,
            GameMapService gameMapService)
        {
            _gameMapService = gameMapService;

            transform.position = startPosition;
            transform.rotation = Quaternion.Euler(0, 0, startRotation);

            _velocity = moveDirection * moveSpeed;
            _rotationSpeed = rotationSpeed;
        }

        private void FixedUpdate()
        {
            var newPosition = transform.position + _velocity * Time.fixedDeltaTime;

            if (!_isEnteredToMap)
            {
                if (_gameMapService.IsInsideMap(newPosition, _circleCollider.radius))
                {
                    _isEnteredToMap = true;
                }
            }

            if (_isEnteredToMap)
            {
                newPosition = _gameMapService.WrapPosition(newPosition, _circleCollider.radius);
            }

            _rigidbody.MovePosition(newPosition);
            _rigidbody.MoveRotation(transform.rotation.eulerAngles.z + _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}

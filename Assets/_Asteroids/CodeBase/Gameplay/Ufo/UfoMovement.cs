using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Asteroids.CodeBase.Gameplay.Ufo
{
    public class UfoMovement : MonoBehaviour
    {
        [SerializeField, Required] private Rigidbody2D _rigidbody;
        [SerializeField, Required] private CircleCollider2D _collider;

        private bool _isEnteredToMap;
        private float _speed;
        private Vector2 _direction;
        private Transform _target;
        private GameMapService _mapService;

        public void Initialize(Vector2 startPosition, Transform target, float speed, GameMapService mapService)
        {
            transform.position = startPosition;
            _target = target;
            _speed = speed;
            _mapService = mapService;
        }

        private void FixedUpdate()
        {
            if (_target == null)
            {
                return;
            }

            _direction = (_target.position - transform.position).normalized;

            var newPosition = (Vector2)transform.position + _direction * _speed * Time.fixedDeltaTime;

            if (!_isEnteredToMap && _mapService.IsInsideMap(newPosition, _collider.radius))
            {
                _isEnteredToMap = true;
            }

            if (_isEnteredToMap)
            {
                newPosition = _mapService.WrapPosition(newPosition, _collider.radius);
            }

            _rigidbody.MovePosition(newPosition);
        }
    }
}

using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Entities.Asteroid
{
    public partial class Asteroid : MonoBehaviour
    {
        [SerializeField, Required] private SpriteRenderer _spriteRenderer;
        [SerializeField, Required] private CircleCollider2D _circleCollider;

        [SerializeField, Required] private Rigidbody2D _rigidbody;

        private GameMapService _gameMapService;
        private Vector3 _velocity;
        private float _rotationSpeed;
        private float _size;

        [Inject]
        public void Construct(AsteroidSpawnPayload spawnPayload, GameMapService gameMapService)
        {
            _gameMapService = gameMapService;

            transform.position = spawnPayload.Position;
            transform.rotation = Quaternion.Euler(0, 0, spawnPayload.Rotation);

            _velocity = spawnPayload.MoveDirection * 1;
            _rotationSpeed = 30;
            _size = 1;

            _circleCollider.radius = _size;
        }

        private void FixedUpdate()
        {
            const float objectRadius = 0.5f;
            var newPosition = transform.position + _velocity * Time.fixedDeltaTime;
            var wrappedPosition = _gameMapService.WrapPosition(newPosition, _size);

            _rigidbody.MovePosition(wrappedPosition);
            _rigidbody.MoveRotation(transform.rotation.eulerAngles.z + _rotationSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
        }
    }
}

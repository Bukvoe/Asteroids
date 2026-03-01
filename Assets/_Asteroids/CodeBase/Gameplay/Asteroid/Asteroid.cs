using System;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Common;
using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Asteroid
{
    public partial class Asteroid : MonoBehaviour
    {
        public event Action<Asteroid> OnDestroyed;

        [SerializeField, Required] private Destroyable _destroyable;
        [SerializeField, Required] private AsteroidDamageable _damageable;

        [SerializeField, Required] private SpriteRenderer _spriteRenderer;
        [SerializeField, Required] private CircleCollider2D _circleCollider;

        [SerializeField, Required] private Rigidbody2D _rigidbody;

        private float _rotationSpeed;
        private Vector3 _velocity;
        private GameMapService _gameMapService;

        public AsteroidSize Size { get; private set; }

        [Inject]
        public void Construct(AsteroidSpawnPayload spawnPayload, GameMapService gameMapService)
        {
            _gameMapService = gameMapService;

            transform.position = spawnPayload.Position;
            transform.rotation = Quaternion.Euler(0, 0, spawnPayload.Rotation);

            _velocity = spawnPayload.MoveDirection * spawnPayload.MoveSpeed;
            _rotationSpeed = spawnPayload.RotationSpeed;
            _spriteRenderer.sprite = spawnPayload.Sprite;
            _circleCollider.radius = spawnPayload.Radius;

            Size = spawnPayload.Size;
        }

        private void Start()
        {
            _destroyable.OnDestroyed += NotifyDestroyed;
            _damageable.OnDamaged += _destroyable.DestroySelf;
        }

        private void OnDestroy()
        {
            _destroyable.OnDestroyed -= NotifyDestroyed;
            _damageable.OnDamaged -= _destroyable.DestroySelf;
        }

        private void FixedUpdate()
        {
            var newPosition = transform.position + _velocity * Time.fixedDeltaTime;
            var wrappedPosition = _gameMapService.WrapPosition(newPosition, _circleCollider.radius);

            _rigidbody.MovePosition(wrappedPosition);
            _rigidbody.MoveRotation(transform.rotation.eulerAngles.z + _rotationSpeed * Time.fixedDeltaTime);
        }

        private void NotifyDestroyed()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}

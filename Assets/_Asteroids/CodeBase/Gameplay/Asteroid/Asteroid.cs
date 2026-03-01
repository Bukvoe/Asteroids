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
        [SerializeField, Required] private AsteroidMovement _movement;
        [SerializeField, Required] private SpriteRenderer _spriteRenderer;
        [SerializeField, Required] private CircleCollider2D _circleCollider;

        public AsteroidSize Size { get; private set; }

        [Inject]
        public void Construct(AsteroidSpawnPayload spawnPayload, GameMapService gameMapService)
        {
            _spriteRenderer.sprite = spawnPayload.Sprite;
            _circleCollider.radius = spawnPayload.Radius;

            Size = spawnPayload.Size;

            _movement.Initialize(
                spawnPayload.Position,
                spawnPayload.Rotation,
                spawnPayload.MoveDirection,
                spawnPayload.MoveSpeed,
                spawnPayload.RotationSpeed,
                gameMapService);
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

        private void NotifyDestroyed()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}

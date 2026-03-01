using System;
using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Starship
{
    public class StarshipCollision : MonoBehaviour
    {
        public event Action CollisionDetected;

        [SerializeField, Required] private CircleCollider2D _collider;
        [SerializeField, Required] private Rigidbody2D _rigidbody;

        [Inject]
        private void Construct(GameConfigService gameConfigService)
        {
            var starshipConfig = gameConfigService.StarshipConfig;

            _collider.radius = starshipConfig.Radius;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Asteroid.Asteroid _) || other.TryGetComponent(out Ufo.Ufo _))
            {
                CollisionDetected?.Invoke();
            }
        }
    }
}

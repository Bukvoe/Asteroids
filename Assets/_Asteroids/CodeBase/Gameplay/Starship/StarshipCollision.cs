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
            var starshipConfig = gameConfigService.GetStarshipConfig();

            _collider.radius = starshipConfig.Size;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Asteroid.Asteroid asteroid))
            {
                CollisionDetected?.Invoke();
            }
        }
    }
}

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Asteroids.CodeBase.Gameplay.Starship
{
    public class StarshipCollision : MonoBehaviour
    {
        public event Action CollisionDetected;

        [SerializeField, Required] private Collider2D _collider;
        [SerializeField, Required] private Rigidbody2D _rigidbody;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Asteroid.Asteroid asteroid))
            {
                CollisionDetected?.Invoke();
            }
        }
    }
}

using System;
using _Asteroids.CodeBase.Gameplay.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Asteroids.CodeBase.Gameplay.Starship
{
    public class Starship : MonoBehaviour
    {
        public event Action OnDestroyed;

        [SerializeField, Required] private Destroyable _destroyable;
        [SerializeField, Required] private StarshipCollision _collision;
        [SerializeField, Required] private StarshipDamageable _damageable;
        [field: SerializeField, Required] public StarshipMovement Movement { get; private set; }
        [field: SerializeField, Required] public StarshipWeapon Weapon { get; private set; }

        private void Start()
        {
            _collision.CollisionDetected += OnCollisionDetected;
            _damageable.OnDamaged += _destroyable.DestroySelf;
            _destroyable.OnDestroyed += NotifyDestroyed;
        }

        private void OnDestroy()
        {
            _collision.CollisionDetected -= OnCollisionDetected;
            _damageable.OnDamaged -= _destroyable.DestroySelf;
            _destroyable.OnDestroyed -= NotifyDestroyed;
        }

        private void OnCollisionDetected()
        {
            _destroyable.DestroySelf();
        }

        private void NotifyDestroyed()
        {
            OnDestroyed?.Invoke();
        }
    }
}

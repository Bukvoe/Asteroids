using System;
using _Asteroids.CodeBase.Factories;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Common;
using _Asteroids.CodeBase.Gameplay.Weapons;
using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Ufo
{
    public class Ufo : MonoBehaviour
    {
        public event Action<Ufo> OnDestroyed;

        [SerializeField, Required] private Destroyable _destroyable;
        [SerializeField, Required] private UfoDamageable _damageable;
        [SerializeField, Required] private UfoCollision _collision;
        [SerializeField, Required] private UfoMovement _movement;
        [SerializeField, Required] private UfoWeapon _weapon;

        [Inject]
        public void Construct(
            UfoSpawnPayload spawnPayload,
            StarshipService starshipService,
            MonoFactory<BulletWeapon, BulletWeaponSpawnPayload> bulletWeaponFactory,
            GameMapService mapBorderService)
        {
            _movement.Initialize(spawnPayload.Position, starshipService.Starship.transform, spawnPayload.MovementSpeed, mapBorderService);
            _weapon.Initialize(bulletWeaponFactory, spawnPayload.BulletWeaponCooldown, spawnPayload.BulletSpeed, starshipService.Starship.transform);
        }

        private void Start()
        {
            _collision.CollisionDetected += _destroyable.DestroySelf;
            _damageable.OnDamaged += _destroyable.DestroySelf;
            _destroyable.OnDestroyed += NotifyDestroyed;
        }

        private void OnDestroy()
        {
            _collision.CollisionDetected -= _destroyable.DestroySelf;
            _damageable.OnDamaged -= _destroyable.DestroySelf;
            _destroyable.OnDestroyed -= NotifyDestroyed;
        }

        private void NotifyDestroyed()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}

using _Asteroids.CodeBase.Factories;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Common;
using _Asteroids.CodeBase.Gameplay.Weapons;
using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Asteroids.CodeBase.Gameplay.Ufo
{
    public class UfoWeapon : MonoBehaviour
    {
        [SerializeField, Required] private Transform _weaponsRoot;

        private BulletWeapon _bulletWeapon;
        private StarshipService _starshipService;

        public void Initialize(
            MonoFactory<BulletWeapon, BulletWeaponSpawnPayload> factory,
            float cooldown,
            float bulletSpeed,
            StarshipService starshipService)
        {
            var bulletWeaponSpawnPayload = new BulletWeaponSpawnPayload(
                parent: _weaponsRoot,
                cooldown: cooldown,
                bulletSpeed: bulletSpeed);

            _bulletWeapon = factory.Create(bulletWeaponSpawnPayload);

            _starshipService = starshipService;
        }

        private void FixedUpdate()
        {
            var starship = _starshipService.Starship;

            if (starship == null || !_bulletWeapon.CanShoot())
            {
                return;
            }

            Vector2 direction = (starship.transform.position - transform.position).normalized;
            _bulletWeapon.Shoot(new ShootIntent(transform.position, direction, EntityTag.Enemy));
        }
    }
}

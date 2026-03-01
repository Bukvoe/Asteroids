using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Common;
using _Asteroids.CodeBase.Gameplay.Weapons;
using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Starship
{
    public class StarshipWeapon : MonoBehaviour
    {
        [SerializeField, Required] private Transform _weaponsRoot;

        public IWeapon Primary { get; private set; }
        public IWeapon Secondary { get; private set; }

        [Inject]
        private void Construct(
            GameConfigService gameConfigService,
            BulletWeapon.Factory bulletWeaponFactory,
            LaserWeapon.Factory laserWeaponFactory)
        {
            var starshipConfig = gameConfigService.StarshipConfig;

            var bulletWeaponSpawnPayload = new BulletWeaponSpawnPayload(
                parent: _weaponsRoot,
                cooldown: starshipConfig.BulletWeaponCooldown,
                bulletSpeed: starshipConfig.BulletSpeed);

            Primary = bulletWeaponFactory.Create(bulletWeaponSpawnPayload);

            var laserWeaponSpawnPayload = new LaserWeaponSpawnPayload(
                parent: _weaponsRoot,
                chargeCooldown: starshipConfig.LaserWeaponCooldown,
                maxCharges: starshipConfig.LaserWeaponCharges,
                distance: starshipConfig.LaserDistance,
                duration: starshipConfig.LaserDuration);

            Secondary = laserWeaponFactory.Create(laserWeaponSpawnPayload);
        }

        public void ShootPrimary()
        {
            Shoot(Primary);
        }

        public void ReleasePrimary()
        {
            Release(Primary);
        }

        public void ShootSecondary()
        {
            Shoot(Secondary);
        }

        public void ReleaseSecondary()
        {
            Release(Secondary);
        }

        private void Shoot(IWeapon weapon)
        {
            if (weapon != null && weapon.CanShoot())
            {
                weapon.Shoot(new ShootIntent(_weaponsRoot.position, _weaponsRoot.up, EntityTag.Player));
            }
        }

        private void Release(IWeapon weapon)
        {
            if (weapon != null)
            {
                weapon.ReleaseShoot();
            }
        }
    }
}

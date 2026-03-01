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
        private void Construct(GameConfigService gameConfigService, BulletWeapon.Factory bulletWeaponFactory)
        {
            var starshipConfig = gameConfigService.GetStarshipConfig();

            var bulletWeaponSpawnPayload = new BulletWeaponSpawnPayload(
                parent: _weaponsRoot,
                cooldown: starshipConfig.BulletWeaponCooldown,
                bulletSpeed: starshipConfig.BulletSpeed);

            Primary = bulletWeaponFactory.Create(bulletWeaponSpawnPayload);
            Secondary = bulletWeaponFactory.Create(bulletWeaponSpawnPayload);
        }

        public void ShootPrimary()
        {
            Shoot(Primary);
        }

        public void ShootSecondary()
        {
            Shoot(Secondary);
        }

        private void Shoot(IWeapon weapon)
        {
            if (weapon != null && weapon.CanShoot())
            {
                weapon.Shoot(new ShootIntent(_weaponsRoot.position, _weaponsRoot.up, EntityTag.Player));
            }
        }
    }
}

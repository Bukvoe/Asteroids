using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Common;
using _Asteroids.CodeBase.Gameplay.Weapons;
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
        public void Construct(BulletWeapon.Factory bulletWeaponFactory)
        {
            Primary = bulletWeaponFactory.Create(new BulletWeaponSpawnPayload(_weaponsRoot));
            Secondary = bulletWeaponFactory.Create(new BulletWeaponSpawnPayload(_weaponsRoot));
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

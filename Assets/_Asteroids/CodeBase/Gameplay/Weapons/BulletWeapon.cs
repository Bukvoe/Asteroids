using _Asteroids.CodeBase.Factories.Payloads;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public partial class BulletWeapon : MonoBehaviour, IWeapon
    {
        private BulletWeaponState _state;
        private Bullet.Factory _bulletFactory;
        private float _bulletSpeed;

        public IWeaponState State => _state;

        [Inject]
        public void Construct(BulletWeaponSpawnPayload spawnPayload, Bullet.Factory bulletFactory)
        {
            _state = new BulletWeaponState(spawnPayload.Cooldown);
            _bulletFactory = bulletFactory;
            _bulletSpeed = spawnPayload.BulletSpeed;

            transform.SetParent(spawnPayload.Parent, worldPositionStays: false);
        }

        public void Update()
        {
            _state.Tick(Time.deltaTime);
        }

        public bool CanShoot()
        {
            return _state.CurrentCooldown <= 0;
        }

        public void Shoot(ShootIntent shootIntent)
        {
            if (!CanShoot())
            {
                return;
            }

            _state.StartCooldown();

            var payload = new BulletSpawnPayload(shootIntent.From, shootIntent.Direction, _bulletSpeed, shootIntent.EntityTag);
            _bulletFactory.Create(payload);
        }

        public void ReleaseShoot()
        {
        }
    }
}

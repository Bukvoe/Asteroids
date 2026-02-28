using _Asteroids.CodeBase.Factories.Payloads;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Components
{
    public partial class BulletWeapon : MonoBehaviour, IWeapon
    {
        private BulletWeaponState _state;

        public IWeaponState State => _state;

        [Inject]
        public void Construct(BulletWeaponSpawnPayload spawnPayload)
        {
            transform.SetParent(spawnPayload.Parent, worldPositionStays: false);
            _state = new BulletWeaponState(1);
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
            if (CanShoot())
            {
                _state.StartCooldown();
            }
        }
    }
}

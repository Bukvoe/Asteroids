using _Asteroids.CodeBase.Factories.Payloads;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public partial class LaserWeapon : MonoBehaviour, IWeapon
    {
        private Laser.Factory _laserFactory;
        private LaserWeaponState _state;
        private float _distance;
        private float _duration;

        private Laser _currentLaser;

        public IWeaponState State => _state;

        [Inject]
        public void Construct(LaserWeaponSpawnPayload spawnPayload, Laser.Factory laserFactory)
        {
            _laserFactory = laserFactory;
            _state = new LaserWeaponState(spawnPayload.MaxCharges, spawnPayload.ChargeCooldown);

            transform.SetParent(spawnPayload.Parent, worldPositionStays: false);

            _distance = spawnPayload.Distance;
            _duration = spawnPayload.Duration;
        }

        private void Update()
        {
            _state.Tick(Time.deltaTime);
        }

        public bool CanShoot()
        {
            return _currentLaser == null && _state.CurrentCharges > 0;
        }

        public void Shoot(ShootIntent shootIntent)
        {
            if (!CanShoot())
            {
                return;
            }

            _state.StartCooldown();
            var spawnPayload = new LaserSpawnPayload(transform, _distance, _duration, shootIntent.EntityTag);
            _currentLaser = _laserFactory.Create(spawnPayload);
        }

        public void ReleaseShoot()
        {
            if (_currentLaser == null)
            {
                return;
            }

            Destroy(_currentLaser.gameObject);
            _currentLaser = null;
        }
    }
}

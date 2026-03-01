using System;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Common;
using _Asteroids.CodeBase.Gameplay.Weapons;
using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Ufo
{
    public partial class Ufo : MonoBehaviour
    {
        public event Action<Ufo> OnDestroyed;

        [SerializeField, Required] private Destroyable _destroyable;
        [SerializeField, Required] private UfoDamageable _damageable;
        [SerializeField, Required] private UfoCollision _collision;

        [SerializeField, Required] private CircleCollider2D _circleCollider;

        private Vector2 _direction;
        private float _movementSpeed;
        private float _size;
        private bool _isEnteredToMap;

        private float _speed;
        private BulletWeapon _bulletWeapon;
        [SerializeField, Required] private Rigidbody2D _rigidbody;

        private Starship.Starship _starship;
        [SerializeField] private Transform _weaponsRoot;

        private GameMapService _gameMapService;

        [Inject]
        public void Construct(UfoSpawnPayload spawnPayload, Starship.Starship starship, BulletWeapon.Factory bulletWeaponFactory, GameMapService mapBorderService)
        {
            transform.position = spawnPayload.Position;
            _movementSpeed = spawnPayload.MovementSpeed;
            _starship = starship;

            var bulletWeaponSpawnPayload = new BulletWeaponSpawnPayload(
                parent: _weaponsRoot,
                cooldown: spawnPayload.BulletWeaponCooldown,
                bulletSpeed: spawnPayload.BulletSpeed);

            _bulletWeapon = bulletWeaponFactory.Create(bulletWeaponSpawnPayload);

            _gameMapService = mapBorderService;
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

        private void FixedUpdate()
        {
            MoveToPlayer();

            var newPosition = transform.position + (Vector3)(_direction * (_movementSpeed * Time.fixedDeltaTime));

            if (!_isEnteredToMap)
            {
                if (_gameMapService.IsInsideMap(newPosition, _circleCollider.radius))
                {
                    _isEnteredToMap = true;
                }
            }

            if (_isEnteredToMap)
            {
                newPosition = _gameMapService.WrapPosition(newPosition, _circleCollider.radius);
            }

            _rigidbody.MovePosition(newPosition);

            if (_starship != null && _bulletWeapon.CanShoot())
            {
                _bulletWeapon.Shoot(new ShootIntent(transform.position, _direction, EntityTag.Enemy));
            }
        }

        private void MoveToPlayer()
        {
            if (_starship == null)
            {
                _direction = Vector2.zero;
                return;
            }

            _direction = (_starship.transform.position - transform.position).normalized;
        }


        public void TakeDamage()
        {
            _destroyable.DestroySelf();
        }

        private void NotifyDestroyed()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}

using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public class Bullet : MonoBehaviour, IDamageSource
    {
        private const float LIFETIME = 5f;

        [SerializeField, Required] private Rigidbody2D _rigidbody;
        [SerializeField, Required] private Collider2D _collider;

        private Vector3 _velocity;

        public EntityTag EntityTag { get; private set; }

        [Inject]
        public void Construct(BulletSpawnPayload spawnPayload)
        {
            transform.position = spawnPayload.Position;
            _velocity = spawnPayload.Direction * spawnPayload.Speed;

            EntityTag = spawnPayload.EntityTag;
        }

        private void Start()
        {
            Destroy(gameObject, LIFETIME);
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + _velocity * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && damageable.CanBeDamagedBy(this))
            {
                damageable.TakeDamage();
                _collider.enabled = false;
                Destroy(gameObject);
            }
        }
    }
}

using _Asteroids.CodeBase.Entities;
using _Asteroids.CodeBase.Factories.Payloads;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Components
{
    public partial class Bullet : MonoBehaviour, IDamageSource
    {
        private const float Lifetime = 5f;

        [SerializeField, Required] private Rigidbody2D _rigidbody;

        private Vector3 _velocity;

        public EntityTag EntityTag { get; private set; }

        [Inject]
        public void Construct(BulletSpawnPayload spawnPayload)
        {
            EntityTag = spawnPayload.EntityTag;

            transform.position = spawnPayload.Position;
            _velocity = spawnPayload.Direction * spawnPayload.Speed;
        }

        private void Start()
        {
            Destroy(gameObject, Lifetime);
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + _velocity * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
        }
    }
}

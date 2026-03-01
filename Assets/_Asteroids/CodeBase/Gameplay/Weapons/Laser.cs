using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public partial class Laser : MonoBehaviour, IDamageSource
    {
        private const int MaxLaserHits = 10;

        private readonly RaycastHit2D[] _hits = new RaycastHit2D[MaxLaserHits];
        private readonly ContactFilter2D _contactFilter = new()
        {
            useTriggers = true,
        };

        [SerializeField, Required] private LineRenderer _lineRenderer;

        private float _distance;
        private float _duration;

        public EntityTag EntityTag { get; private set; }

        [Inject]
        public void Construct(LaserSpawnPayload spawnPayload)
        {
            transform.SetParent(spawnPayload.Parent, worldPositionStays: false);
            _distance = spawnPayload.Distance;
            _duration = spawnPayload.Duration;

            EntityTag = spawnPayload.EntityTag;
        }

        private void Start()
        {
            Destroy(gameObject, _duration);
        }

        private void Update()
        {
            Vector2 start = transform.position;
            var end = start + (Vector2)transform.up * _distance;

            var size = Physics2D.Raycast(start, transform.up, _contactFilter, _hits);

            if (size > 0)
            {
                for (var index = 0; index < size; index++)
                {
                    var hit = _hits[index];
                    if (hit.transform.TryGetComponent(out IDamageable damageable) && damageable.CanBeDamagedBy(this))
                    {
                        damageable.TakeDamage();
                    }
                }
            }

            _lineRenderer.SetPosition(0, start);
            _lineRenderer.SetPosition(1, end);
        }
    }
}

using _Asteroids.CodeBase.Entities;
using UnityEngine;

namespace _Asteroids.CodeBase.Factories.Payloads
{
    public struct BulletSpawnPayload
    {
        public Vector2 Position;
        public Vector2 Direction;
        public float Speed;
        public EntityTag EntityTag;

        public BulletSpawnPayload(Vector2 position, Vector2 direction, float speed, EntityTag etityTag)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            EntityTag = etityTag;
        }
    }
}

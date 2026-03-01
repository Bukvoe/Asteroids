using _Asteroids.CodeBase.Gameplay.Common;
using UnityEngine;

namespace _Asteroids.CodeBase.Factories.Payloads
{
    public struct BulletSpawnPayload
    {
        public readonly Vector2 Position;
        public readonly Vector2 Direction;
        public readonly float Speed;
        public readonly EntityTag EntityTag;

        public BulletSpawnPayload(Vector2 position, Vector2 direction, float speed, EntityTag entityTag)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            EntityTag = entityTag;
        }
    }
}

using _Asteroids.CodeBase.Gameplay.Common;
using UnityEngine;

namespace _Asteroids.CodeBase.Factories.Payloads
{
    public struct LaserSpawnPayload
    {
        public readonly Transform Parent;
        public readonly float Distance;
        public readonly float Duration;
        public readonly EntityTag EntityTag;

        public LaserSpawnPayload(Transform parent, float distance, float duration, EntityTag entityTag)
        {
            Parent = parent;
            Distance = distance;
            Duration = duration;
            EntityTag = entityTag;
        }
    }
}

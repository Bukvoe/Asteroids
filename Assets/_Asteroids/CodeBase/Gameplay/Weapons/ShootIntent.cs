using _Asteroids.CodeBase.Gameplay.Common;
using UnityEngine;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public struct ShootIntent
    {
        public readonly Vector2 From;
        public readonly Vector2 Direction;
        public readonly EntityTag EntityTag;

        public ShootIntent(Vector2 from, Vector2 direction, EntityTag entityTag)
        {
            From = from;
            Direction = direction;
            EntityTag = entityTag;
        }
    }
}

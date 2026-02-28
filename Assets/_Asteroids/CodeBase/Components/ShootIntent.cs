using System.Numerics;

namespace _Asteroids.CodeBase.Components
{
    public struct ShootIntent
    {
        public readonly Vector2 From;
        public readonly Vector2 Direction;

        public ShootIntent(Vector2 from, Vector2 direction)
        {
            From = from;
            Direction = direction;
        }
    }
}

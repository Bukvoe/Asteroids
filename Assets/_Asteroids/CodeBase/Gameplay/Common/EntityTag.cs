using System;

namespace _Asteroids.CodeBase.Gameplay.Common
{
    [Flags]
    public enum EntityTag
    {
        None = 0,
        Player = 1 << 1,
        Neutral = 1 << 2,
        Enemy = 1 << 3,
    }
}

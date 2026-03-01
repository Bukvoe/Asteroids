using System;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public interface IWeaponState
    {
        event Action Changed;
    }
}

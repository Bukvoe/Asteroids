using System;
using _Asteroids.CodeBase.Gameplay.Common;
using _Asteroids.CodeBase.Gameplay.Weapons;
using UnityEngine;

namespace _Asteroids.CodeBase.Gameplay.Asteroid
{
    public class AsteroidDamageable : MonoBehaviour, IDamageable
    {
        public event Action OnDamaged;

        public bool CanBeDamagedBy(IDamageSource damageSource)
        {
            return damageSource.EntityTag == EntityTag.Player;
        }

        public void TakeDamage()
        {
            OnDamaged?.Invoke();
        }
    }
}

using System;
using _Asteroids.CodeBase.Gameplay.Common;
using _Asteroids.CodeBase.Gameplay.Weapons;
using UnityEngine;

namespace _Asteroids.CodeBase.Gameplay.Starship
{
    public class StarshipDamageable : MonoBehaviour, IDamageable
    {
        public event Action OnDamaged;

        public bool CanBeDamagedBy(IDamageSource damageSource)
        {
            return damageSource.EntityTag == EntityTag.Enemy;
        }

        public void TakeDamage()
        {
            OnDamaged?.Invoke();
        }
    }
}

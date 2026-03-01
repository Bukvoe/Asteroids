using _Asteroids.CodeBase.Gameplay.Weapons;

namespace _Asteroids.CodeBase.Gameplay.Common
{
    public interface IDamageable
    {
        public bool CanBeDamagedBy(IDamageSource damageSource);

        public void TakeDamage();
    }
}

using System;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public class BulletWeaponState : IWeaponState, ICooldownState
    {
        public event Action Changed;

        private readonly float _cooldown;

        private float _cooldownTimer;

        public float CurrentCooldown { get; private set; }

        public BulletWeaponState(float cooldown)
        {
            _cooldown = cooldown;
            CurrentCooldown = 0f;
        }

        public void StartCooldown()
        {
            CurrentCooldown = _cooldown;
            Changed?.Invoke();
        }

        public void Tick(float deltaTime)
        {
            if (CurrentCooldown <= 0f)
            {
                return;
            }

            CurrentCooldown -= deltaTime;

            if (CurrentCooldown < 0f)
            {
                CurrentCooldown = 0f;
            }

            Changed?.Invoke();
        }
    }
}

using System;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public class LaserWeaponState : IWeaponState, ICooldownState, IChargesState
    {
        public event Action Changed;

        public float CurrentCooldown { get; private set; }
        public float MaxCooldown { get; }

        public int CurrentCharges { get; private set; }
        public int MaxCharges { get; }

        private bool _isReloading;

        private float _updateTimer = 0f;

        public LaserWeaponState(int maxCharges, float maxCooldown)
        {
            MaxCharges = maxCharges;
            CurrentCharges = maxCharges;
            MaxCooldown = maxCooldown;
        }

        public void StartCooldown()
        {
            CurrentCharges--;
            Changed?.Invoke();

            if (_isReloading)
            {
                return;
            }

            _isReloading = true;
            CurrentCooldown = MaxCooldown;
        }

        public void Tick(float deltaTime)
        {
            if (!_isReloading)
            {
                return;
            }

            _updateTimer += deltaTime;

            if (_updateTimer < ICooldownState.CooldownUpdateInterval)
            {
                return;
            }

            _updateTimer = 0f;

            CurrentCooldown -= ICooldownState.CooldownUpdateInterval;

            if (CurrentCooldown > 0f)
            {
                Changed?.Invoke();
                return;
            }

            CurrentCharges = Math.Min(CurrentCharges + 1, MaxCharges);
            Changed?.Invoke();

            if (CurrentCharges >= MaxCharges)
            {
                _isReloading = false;
                CurrentCooldown = 0f;
            }
            else
            {
                CurrentCooldown = MaxCooldown;
            }
        }
    }
}

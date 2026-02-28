using System;
using _Asteroids.CodeBase.Components;
using _Asteroids.CodeBase.Entities.Starship;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.UI
{
    public class HudPresenter : IInitializable, IDisposable
    {
        private readonly Starship _starship;
        private readonly HudView _hudView;

        public HudPresenter(HudView hudView, Starship starship)
        {
            _hudView = hudView;
            _starship = starship;
        }

        public void Initialize()
        {
            _starship.Movement.OnPositionChanged += UpdatePosition;
            _starship.Movement.OnRotationChanged += UpdateAngle;
            _starship.Movement.OnSpeedChanged += UpdateSpeed;
            _starship.Weapon.Secondary.State.Changed += UpdateWeaponState;

            UpdateWeaponState();
        }

        public void Dispose()
        {
            _starship.Movement.OnPositionChanged -= UpdatePosition;
            _starship.Movement.OnRotationChanged -= UpdateAngle;
            _starship.Movement.OnSpeedChanged -= UpdateSpeed;
            _starship.Weapon.Secondary.State.Changed -= UpdateWeaponState;
        }

        private void UpdatePosition(Vector2 position)
        {
            _hudView.UpdatePosition(position);
        }

        private void UpdateAngle(float angle)
        {
            _hudView.UpdateAngle(angle);
        }

        private void UpdateSpeed(float speed)
        {
            _hudView.UpdateSpeed(speed);
        }

        private void UpdateWeaponState()
        {
            var weaponState = _starship.Weapon.Secondary.State;

            if (weaponState is IChargesState chargesState)
            {
                _hudView.ShowCharges();
                _hudView.UpdateCharges(chargesState.CurrentCharges, chargesState.MaxCharges);
            }
            else
            {
                _hudView.HideCharges();
            }

            if (weaponState is ICooldownState cooldownState)
            {
                if (cooldownState.CurrentCooldown > 0)
                {
                    _hudView.ShowCooldown();
                    _hudView.UpdateCooldown(cooldownState.CurrentCooldown);
                }
                else
                {
                    _hudView.HideCooldown();
                }

            }
            else
            {
                _hudView.HideCooldown();
            }
        }
    }
}

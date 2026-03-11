using System;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Gameplay.Weapons;
using _Asteroids.CodeBase.Services;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.UI
{
    public class HudPresenter : IInitializable, IDisposable
    {
        private readonly HudView _hudView;
        private readonly StarshipService _starshipService;
        private readonly CurrentRunService _currentRunService;

        private Starship _starship;

        public HudPresenter(
            HudView hudView,
            StarshipService starshipService,
            CurrentRunService currentRunService)
        {
            _hudView = hudView;
            _starshipService = starshipService;
            _currentRunService = currentRunService;
        }

        public void Initialize()
        {
            _starshipService.StarshipSpawned += OnStarshipSpawned;
            _starshipService.StarshipDestroyed += OnStarshipDestroyed;

            _currentRunService.ScoreChanged += UpdateScore;

            if (_starshipService.Starship != null)
            {
                OnStarshipSpawned();
            }
        }

        private void OnStarshipSpawned()
        {
            if (_starship != null)
            {
                UnsubscribeFromStarship(_starship);
            }

            _starship = _starshipService.Starship;
            SubscribeToStarship(_starship);

            UpdateWeaponState();
            UpdateScore();
        }

        private void OnStarshipDestroyed()
        {
            UnsubscribeFromStarship(_starship);
            _starship = null;
        }

        public void Dispose()
        {
            UnsubscribeFromStarship(_starship);

            _starshipService.StarshipSpawned -= OnStarshipSpawned;
            _starshipService.StarshipDestroyed -= OnStarshipDestroyed;

            _currentRunService.ScoreChanged -= UpdateScore;
        }

        private void SubscribeToStarship(Starship starship)
        {
            if (starship == null)
            {
                return;
            }

            starship.Movement.OnPositionChanged += UpdatePosition;
            starship.Movement.OnRotationChanged += UpdateAngle;
            starship.Movement.OnSpeedChanged += UpdateSpeed;
            starship.Weapon.Secondary.State.Changed += UpdateWeaponState;
        }

        private void UnsubscribeFromStarship(Starship starship)
        {
            if (starship == null)
            {
                return;
            }

            starship.Movement.OnPositionChanged -= UpdatePosition;
            starship.Movement.OnRotationChanged -= UpdateAngle;
            starship.Movement.OnSpeedChanged -= UpdateSpeed;
            starship.Weapon.Secondary.State.Changed -= UpdateWeaponState;
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
            if (_starship == null)
            {
                return;
            }

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

        private void UpdateScore()
        {
            _hudView.UpdateScore(_currentRunService.Score);
        }
    }
}

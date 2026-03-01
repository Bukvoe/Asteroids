using System;
using _Asteroids.CodeBase.Gameplay.Starship;
using UnityEngine.InputSystem;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class InputService : ITickable, IDisposable
    {
        private readonly Starship _starship;
        private readonly PlayerInput _playerInput;

        private readonly InputAction _accelerationAction;
        private readonly InputAction _rotationAction;
        private readonly InputAction _primaryAttackAction;
        private readonly InputAction _secondaryAttackAction;

        public InputService(Starship starship)
        {
            _starship = starship;
            _playerInput = new PlayerInput();

            _accelerationAction = _playerInput.Starship.Acceleration;
            _rotationAction = _playerInput.Starship.Rotation;
            _primaryAttackAction = _playerInput.Starship.PrimaryAttack;
            _secondaryAttackAction = _playerInput.Starship.SecondaryAttack;

            _playerInput.Enable();
            _playerInput.Starship.Enable();
        }

        public void Tick()
        {
            if (_starship == null)
            {
                return;
            }

            var isAccelerating = _accelerationAction.IsPressed();
            var rotationValue = _rotationAction.ReadValue<float>();

            _starship.Movement.SetMoveIntent(new MoveIntent(isAccelerating, rotationValue));

            if (_primaryAttackAction.WasPressedThisFrame())
            {
                _starship.Weapon.ShootPrimary();
            }

            if (_primaryAttackAction.WasReleasedThisFrame())
            {
                _starship.Weapon.ReleasePrimary();
            }

            if (_secondaryAttackAction.WasPressedThisFrame())
            {
                _starship.Weapon.ShootSecondary();
            }

            if (_secondaryAttackAction.WasReleasedThisFrame())
            {
                _starship.Weapon.ReleaseSecondary();
            }
        }

        public void Dispose()
        {
            _playerInput?.Disable();
            _playerInput?.Dispose();
        }
    }
}

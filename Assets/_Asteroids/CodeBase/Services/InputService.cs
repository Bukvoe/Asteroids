using System;
using _Asteroids.CodeBase.Entities.Starship;
using UnityEngine;
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

            var isAccelerating = Mathf.Approximately(_accelerationAction.ReadValue<float>(), 1f);
            var rotationValue = _rotationAction.ReadValue<float>();

            _starship.Movement.SetMoveIntent(new MoveIntent(isAccelerating, rotationValue));
        }

        public void Dispose()
        {
            _playerInput?.Starship.Disable();
            _playerInput?.Disable();
        }
    }
}

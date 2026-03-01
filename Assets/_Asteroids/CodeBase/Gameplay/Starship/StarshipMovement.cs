using System;
using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Starship
{
    public class StarshipMovement : MonoBehaviour
    {
        private const float MinTimeToMaxSpeed = 0.01f;
        public event Action<Vector2> OnPositionChanged;
        public event Action<float> OnRotationChanged;
        public event Action<float> OnSpeedChanged;

        [SerializeField, Required] private Rigidbody2D _rigidbody;

        [SerializeField, SuffixLabel("unit/sec")]
        private float _maxSpeed;

        [SerializeField, MinValue(MinTimeToMaxSpeed), SuffixLabel("sec")]
        private float _timeToMaxSpeed;

        [SerializeField] private AnimationCurve _speedCurve;

        [SerializeField, SuffixLabel("degree/sec")]
        private float _rotationSpeed;

        private float _thrustTime;
        private Vector2 _velocity;
        private MoveIntent _moveIntent;
        private GameMapService _gameMapService;

        [Inject]
        private void Construct(GameConfigService gameConfigService, GameMapService gameMapService)
        {
            var starshipConfig = gameConfigService.GetStarshipConfig();

            _gameMapService = gameMapService;

            _maxSpeed = starshipConfig.MaxSpeed;
            _timeToMaxSpeed = starshipConfig.TimeToMaxSpeed;
            _speedCurve = starshipConfig.SpeedCurve;
            _rotationSpeed = starshipConfig.RotationSpeed;
        }

        public void SetMoveIntent(MoveIntent moveIntent)
        {
            _moveIntent = moveIntent;
        }

        private void FixedUpdate()
        {
            UpdateThrustTime();
            UpdateVelocity();
            Move();
            Rotate();
        }

        private void UpdateThrustTime()
        {
            if (_moveIntent.Thrust)
            {
                _thrustTime += Time.fixedDeltaTime;
            }
            else
            {
                _thrustTime -= Time.fixedDeltaTime;
            }

            _thrustTime = Mathf.Clamp(_thrustTime, 0, _timeToMaxSpeed);
        }

        private void UpdateVelocity()
        {
            var progress = Mathf.Clamp01(_thrustTime / _timeToMaxSpeed);
            var speedMultiplier = _speedCurve.Evaluate(progress);

            _velocity = (Vector2)transform.up * (_maxSpeed * speedMultiplier);
            OnSpeedChanged?.Invoke(_velocity.magnitude);
        }

        private void Move()
        {
            const float objectRadius = 0.5f;
            Vector3 newPosition = _rigidbody.position + _velocity * Time.fixedDeltaTime;
            var wrappedPosition = _gameMapService.WrapPosition(newPosition, objectRadius);

            _rigidbody.MovePosition(wrappedPosition);
            OnPositionChanged?.Invoke(wrappedPosition);
        }

        private void Rotate()
        {
            var rotationDelta = _moveIntent.Rotation * _rotationSpeed * Time.fixedDeltaTime;
            var newRotation = _rigidbody.rotation + rotationDelta;

            _rigidbody.MoveRotation(newRotation);

            var normalizedRotation = Mathf.Repeat(newRotation, 360f);
            OnRotationChanged?.Invoke(normalizedRotation);
        }
    }
}

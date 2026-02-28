using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Asteroids.CodeBase.Entities.Starship
{
 public class StarshipMovement : MonoBehaviour
    {
        public event Action<Vector2> OnPositionChanged;
        public event Action<float> OnRotationChanged;
        public event Action<float> OnSpeedChanged;

        [SerializeField, Required] private Rigidbody2D _rigidbody;

        [SerializeField, SuffixLabel("unit/sec")]
        private float _maxSpeed;

        [SerializeField, SuffixLabel("sec")]
        private float _timeToMaxSpeed;

        [SerializeField] private AnimationCurve _speedCurve;

        [SerializeField, SuffixLabel("degree/sec")]
        private float _rotationSpeed;

        private float _thrustTime;
        private Vector2 _velocity;
        private MoveIntent _moveIntent;

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
            _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
            OnPositionChanged?.Invoke(transform.position);
        }

        private void Rotate()
        {
            var rotationDelta = _moveIntent.Rotation * _rotationSpeed * Time.fixedDeltaTime;
            _rigidbody.MoveRotation(_rigidbody.rotation + rotationDelta);

            OnRotationChanged?.Invoke(transform.eulerAngles.z);
        }
    }
}

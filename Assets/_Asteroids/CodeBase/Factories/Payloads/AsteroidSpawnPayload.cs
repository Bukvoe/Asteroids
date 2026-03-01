using _Asteroids.CodeBase.Gameplay.Asteroid;
using UnityEngine;

namespace _Asteroids.CodeBase.Factories.Payloads
{
    public struct AsteroidSpawnPayload
    {
        public readonly AsteroidSize Size;
        public readonly Vector2 Position;
        public readonly float Rotation;
        public readonly Vector2 MoveDirection;
        public readonly float MoveSpeed;
        public readonly float RotationSpeed;
        public readonly float Radius;
        public readonly Sprite Sprite;

        public AsteroidSpawnPayload(
            AsteroidSize size,
            Vector2 position,
            float rotation,
            Vector2 moveDirection,
            float moveSpeed,
            float rotationSpeed,
            float radius,
            Sprite sprite)
        {
            Size = size;
            Position = position;
            Rotation = rotation;
            MoveDirection = moveDirection;
            MoveSpeed = moveSpeed;
            RotationSpeed = rotationSpeed;
            Radius = radius;
            Sprite = sprite;
        }
    }
}

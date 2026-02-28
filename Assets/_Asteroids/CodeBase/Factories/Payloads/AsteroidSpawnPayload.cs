using UnityEngine;

namespace _Asteroids.CodeBase.Factories.Payloads
{
    public struct AsteroidSpawnPayload
    {
        public Vector2 Position;
        public float Rotation;
        public Vector2 MoveDirection;
        public bool IsRotatesClockwise;

        public AsteroidSpawnPayload(Vector2 position, float rotation, Vector2 moveDirection, bool isRotatesClockwise)
        {
            Position = position;
            Rotation = rotation;
            MoveDirection = moveDirection;
            IsRotatesClockwise = isRotatesClockwise;
        }
    }
}

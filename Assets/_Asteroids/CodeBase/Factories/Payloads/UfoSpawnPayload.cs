using UnityEngine;

namespace _Asteroids.CodeBase.Factories.Payloads
{
    public struct UfoSpawnPayload
    {
        public readonly Vector2 Position;
        public readonly float MovementSpeed;
        public readonly float BulletWeaponCooldown;
        public readonly float BulletSpeed;

        public UfoSpawnPayload(Vector2 position, float movementSpeed, float bulletWeaponCooldown, float bulletSpeed)
        {
            Position = position;
            MovementSpeed = movementSpeed;
            BulletWeaponCooldown = bulletWeaponCooldown;
            BulletSpeed = bulletSpeed;
        }
    }
}

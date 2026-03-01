using UnityEngine;

namespace _Asteroids.CodeBase.Factories.Payloads
{
    public struct BulletWeaponSpawnPayload
    {
        public readonly Transform Parent;
        public readonly float Cooldown;
        public readonly float BulletSpeed;

        public BulletWeaponSpawnPayload(Transform parent, float cooldown, float bulletSpeed)
        {
            Parent = parent;
            Cooldown = cooldown;
            BulletSpeed = bulletSpeed;
        }
    }
}

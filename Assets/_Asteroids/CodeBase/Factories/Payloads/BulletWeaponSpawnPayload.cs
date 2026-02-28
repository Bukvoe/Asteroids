using UnityEngine;

namespace _Asteroids.CodeBase.Factories.Payloads
{
    public struct BulletWeaponSpawnPayload
    {
        public Transform Parent;

        public BulletWeaponSpawnPayload(Transform parent)
        {
            Parent = parent;
        }
    }
}

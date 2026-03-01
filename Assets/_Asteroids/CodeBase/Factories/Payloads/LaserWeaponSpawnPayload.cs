using UnityEngine;

namespace _Asteroids.CodeBase.Factories.Payloads
{
    public struct LaserWeaponSpawnPayload
    {
        public readonly Transform Parent;
        public readonly float ChargeCooldown;
        public readonly int MaxCharges;
        public readonly float Distance;
        public readonly float Duration;

        public LaserWeaponSpawnPayload(Transform parent, float chargeCooldown, int maxCharges, float distance, float duration)
        {
            Parent = parent;
            ChargeCooldown = chargeCooldown;
            MaxCharges = maxCharges;
            Distance = distance;
            Duration = duration;
        }
    }
}

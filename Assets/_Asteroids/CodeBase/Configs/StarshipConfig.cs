using System;
using UnityEngine;

namespace _Asteroids.CodeBase.Configs
{
    [Serializable]
    public class StarshipConfig
    {
        public float Radius;

        public float MaxSpeed;
        public float TimeToMaxSpeed;
        public AnimationCurve SpeedCurve;
        public float RotationSpeed;

        public float BulletWeaponCooldown;
        public float BulletSpeed;

        public int LaserWeaponCharges;
        public float LaserWeaponCooldown;
        public float LaserDistance;
        public float LaserDuration;
    }
}

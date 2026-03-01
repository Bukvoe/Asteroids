using System;
using UnityEngine;
using UnityEngine.Serialization;

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
    }
}

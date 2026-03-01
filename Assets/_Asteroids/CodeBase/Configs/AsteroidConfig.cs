using System;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using UnityEngine;

namespace _Asteroids.CodeBase.Configs
{
    [Serializable]
    public class AsteroidConfig
    {
        public AsteroidSize Size;
        public float MoveSpeed;
        public float RotationSpeed;
        public float Radius;
        public Sprite Sprite;

        public AsteroidSize NextSize;
        public int Fragments;
    }
}

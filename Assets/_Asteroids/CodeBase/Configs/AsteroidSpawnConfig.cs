using System;
using _Asteroids.CodeBase.Gameplay.Asteroid;

namespace _Asteroids.CodeBase.Configs
{
    [Serializable]
    public class AsteroidSpawnConfig
    {
        public AsteroidSize AsteroidToSpawn;
        public int MaxAsteroids;
        public float SpawnCooldown;
    }
}

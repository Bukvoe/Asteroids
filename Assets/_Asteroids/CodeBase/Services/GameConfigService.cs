using System.Collections.Generic;
using System.Linq;
using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Gameplay.Asteroid;

namespace _Asteroids.CodeBase.Services
{
    public class GameConfigService
    {
        private readonly Dictionary<AsteroidSize, AsteroidConfig> _asteroidConfigs;

        public StarshipConfig StarshipConfig { get; private set; }
        public AsteroidSpawnConfig AsteroidSpawnConfig { get; private set; }
        public EnemySpawnConfig EnemySpawnConfig { get; private set; }
        public UfoConfig UfoConfig { get; private set; }
        public ScoreConfig ScoreConfig { get; private set; }

        public GameConfigService(GameConfig gameConfig)
        {
            StarshipConfig = gameConfig.Starship;

            AsteroidSpawnConfig = gameConfig.AsteroidSpawn;
            _asteroidConfigs = gameConfig.Asteroids.ToDictionary(c => c.Size);

            EnemySpawnConfig = gameConfig.EnemySpawn;
            UfoConfig = gameConfig.Ufo;

            ScoreConfig = gameConfig.Score;
        }

        public AsteroidConfig GetAsteroidConfigBySize(AsteroidSize size)
        {
            if (_asteroidConfigs.TryGetValue(size, out var asteroidConfig))
            {
                return asteroidConfig;
            }

            return new AsteroidConfig();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Gameplay.Asteroid;

namespace _Asteroids.CodeBase.Services
{
    public class GameConfigService
    {
        private readonly StarshipConfig _starshipConfig;
        private readonly Dictionary<AsteroidSize, AsteroidConfig> _asteroidConfigs;

        public GameConfigService(GameConfig gameConfig)
        {
            _starshipConfig = gameConfig.Starship;
            _asteroidConfigs = gameConfig.Asteroids.ToDictionary(c => c.Size);
        }

        public StarshipConfig GetStarshipConfig()
        {
            return _starshipConfig;
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

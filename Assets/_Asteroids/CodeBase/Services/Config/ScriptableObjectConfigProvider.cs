using System.Collections.Generic;
using _Asteroids.CodeBase.Configs;
using Cysharp.Threading.Tasks;

namespace _Asteroids.CodeBase.Services.Config
{
    public class ScriptableObjectConfigProvider : IConfigProvider
    {
        private readonly GameConfigScriptableObject _config;

        public ScriptableObjectConfigProvider(GameConfigScriptableObject config)
        {
            _config = config;
        }

        public UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }

        public GameConfig GetConfig()
        {
            if (_config == null)
            {
                return null;
            }

            return new GameConfig
            {
                Starship = _config.Starship,
                AsteroidSpawn = _config.AsteroidSpawn,
                Asteroids = new List<AsteroidConfig>(_config.Asteroids),
                EnemySpawn = _config.EnemySpawn,
                Ufo = _config.Ufo,
                Score = _config.Score,
            };
        }
    }
}

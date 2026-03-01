using System;
using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using _Asteroids.CodeBase.Gameplay.Ufo;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class GameSessionService : IInitializable, IDisposable
    {
        public event Action ScoreChanged;

        private readonly AsteroidService _asteroidService;
        private readonly EnemyService _enemyService;
        private readonly ScoreConfig _scoreConfig;

        public int Score { get; private set; }

        public GameSessionService(AsteroidService asteroidService, EnemyService enemyService, GameConfigService gameConfigService)
        {
            _asteroidService = asteroidService;
            _enemyService = enemyService;

            _scoreConfig = gameConfigService.ScoreConfig;
        }

        public void Initialize()
        {
            _asteroidService.AsteroidDestroyed += OnAsteroidDestroyed;
            _enemyService.UfoDestroyed += OnUfoDestroyed;
        }

        public void Dispose()
        {
            _asteroidService.AsteroidDestroyed -= OnAsteroidDestroyed;
            _enemyService.UfoDestroyed -= OnUfoDestroyed;
        }

        private void OnAsteroidDestroyed(Asteroid asteroid)
        {
            switch (asteroid.Size)
            {
                case AsteroidSize.Small:
                    AddScore(_scoreConfig.SmallAsteroidScore);
                    break;

                case AsteroidSize.Medium:
                    AddScore(_scoreConfig.MediumAsteroidScore);
                    break;

                case AsteroidSize.Large:
                    AddScore(_scoreConfig.LargeAsteroidScore);
                    break;

                default:
                case AsteroidSize.None:
                    return;
            }
        }

        private void OnUfoDestroyed(Ufo ufo)
        {
            AddScore(_scoreConfig.UfoScore);
        }

        private void AddScore(int score)
        {
            Score += score;
            ScoreChanged?.Invoke();
        }
    }
}

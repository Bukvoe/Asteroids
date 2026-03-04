using System;
using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Data;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Gameplay.Ufo;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class CurrentRunService : IInitializable, IDisposable
    {
        public event Action ScoreChanged;

        private readonly AsteroidService _asteroidService;
        private readonly EnemyService _enemyService;
        private readonly ScoreConfig _scoreConfig;
        private readonly PlayerProgressService _playerProgressService;
        private readonly Starship _starship;
        private readonly RunResult _runResult;

        public int Score => _runResult.Score;

        public CurrentRunService(
            AsteroidService asteroidService,
            EnemyService enemyService,
            GameConfigService gameConfigService,
            PlayerProgressService playerProgressService,
            Starship starship)
        {
            _runResult = new RunResult();

            _asteroidService = asteroidService;
            _enemyService = enemyService;
            _playerProgressService = playerProgressService;
            _starship = starship;

            _scoreConfig = gameConfigService.ScoreConfig;
        }

        public void Initialize()
        {
            _asteroidService.AsteroidDestroyed += OnAsteroidDestroyed;
            _enemyService.UfoDestroyed += OnUfoDestroyed;
            _starship.OnDestroyed += OnStarshipDestroyed;
        }

        public void Dispose()
        {
            _asteroidService.AsteroidDestroyed -= OnAsteroidDestroyed;
            _enemyService.UfoDestroyed -= OnUfoDestroyed;
            _starship.OnDestroyed -= OnStarshipDestroyed;
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
            _runResult.UfoDestroyed++;
            AddScore(_scoreConfig.UfoScore);
        }

        private void OnStarshipDestroyed()
        {
        }

        private void AddScore(int score)
        {
            _runResult.Score += score;
            ScoreChanged?.Invoke();
        }
    }
}

using System;
using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Data;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Gameplay.Ufo;
using _Asteroids.CodeBase.Services.Save;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class CurrentRunService : IInitializable, IDisposable
    {
        public event Action ScoreChanged;
        public event Action RunEnded;

        private readonly AsteroidService _asteroidService;
        private readonly EnemyService _enemyService;
        private readonly ScoreConfig _scoreConfig;
        private readonly Starship _starship;
        private readonly PlayerProgress _playerProgress;
        private readonly ISaveService _saveService;
        private readonly RunResult _runResult;

        public int Score => _runResult.Score;

        public CurrentRunService(
            AsteroidService asteroidService,
            EnemyService enemyService,
            GameConfigService gameConfigService,
            Starship starship,
            PlayerProgress playerProgress,
            ISaveService saveService)
        {
            _runResult = new RunResult();

            _asteroidService = asteroidService;
            _enemyService = enemyService;
            _starship = starship;
            _playerProgress = playerProgress;
            _saveService = saveService;

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
            UpdateProgress(_runResult);
            RunEnded?.Invoke();
        }

        private void UpdateProgress(RunResult runResult)
        {
            if (runResult.Score > _playerProgress.BestScore)
            {
                _playerProgress.BestScore = runResult.Score;
            }

            _playerProgress.UfoDestroyed += runResult.UfoDestroyed;
            _playerProgress.Runs++;

            _saveService.Save(_playerProgress);
        }

        private void AddScore(int score)
        {
            _runResult.Score += score;
            ScoreChanged?.Invoke();
        }
    }
}

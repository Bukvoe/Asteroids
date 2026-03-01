using System;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class GameSessionService : IInitializable, IDisposable
    {
        public event Action ScoreChanged;

        private readonly AsteroidService _asteroidService;

        public int Score { get; private set; }

        public GameSessionService(AsteroidService asteroidService)
        {
            _asteroidService = asteroidService;
        }

        public void Initialize()
        {
            _asteroidService.AsteroidDestroyed += OnAsteroidDestroyed;
        }

        public void Dispose()
        {
            _asteroidService.AsteroidDestroyed -= OnAsteroidDestroyed;
        }

        private void OnAsteroidDestroyed()
        {
            AddScore(10);
        }

        private void AddScore(int score)
        {
            Score += score;
            ScoreChanged?.Invoke();
        }
    }
}

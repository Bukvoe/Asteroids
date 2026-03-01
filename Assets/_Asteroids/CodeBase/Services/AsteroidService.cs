using System;
using System.Collections.Generic;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class AsteroidService : ITickable, IDisposable
    {
        public event Action AsteroidDestroyed;

        private readonly Asteroid.Factory _asteroidFactory;
        private readonly GameMapService _gameMapService;
        private readonly GameConfigService _gameConfigService;
        private readonly RandomService _randomService;

        private readonly List<Asteroid> _spawnedAsteroids = new();
        private readonly int _maxLargeAsteroids;
        private readonly float _maxSpawnCooldown;

        private float _spawnCooldown;

        public AsteroidService(
            Asteroid.Factory asteroidFactory,
            GameMapService gameMapService,
            GameConfigService gameConfigService,
            RandomService randomService)
        {
            _asteroidFactory = asteroidFactory;
            _gameMapService = gameMapService;
            _gameConfigService = gameConfigService;
            _randomService = randomService;

            _maxLargeAsteroids = 5;
            _maxSpawnCooldown = 5;
        }

        public void Tick()
        {
            _spawnCooldown -= Time.deltaTime;

            if (_spawnCooldown <= 0f && CountAsteroidsBySize(AsteroidSize.Large) < _maxLargeAsteroids)
            {
                SpawnAsteroid(AsteroidSize.Large, _gameMapService.GetSpawnRandomPoint());
                _spawnCooldown += _maxSpawnCooldown;
            }
        }

        private void SpawnAsteroid(AsteroidSize size, Vector2 spawnPosition)
        {
            var spawnRotation = _randomService.RandomAngle();
            var moveDirection = (_gameMapService.GetMapRandomPoint() - spawnPosition).normalized;
            var asteroidConfig = _gameConfigService.GetAsteroidConfigBySize(size);
            var rotationSpeed = _randomService.ApplyRandomSign(asteroidConfig.RotationSpeed);

            var spawnPayload = new AsteroidSpawnPayload(
                size: asteroidConfig.Size,
                position: spawnPosition,
                rotation: spawnRotation,
                moveDirection: moveDirection,
                moveSpeed: asteroidConfig.MoveSpeed,
                rotationSpeed: rotationSpeed,
                radius: asteroidConfig.Radius,
                sprite: asteroidConfig.Sprite);

            var asteroid = _asteroidFactory.Create(spawnPayload);

            asteroid.OnDestroyed += OnAsteroidDestroyed;
            _spawnedAsteroids.Add(asteroid);
        }

        private void OnAsteroidDestroyed(Asteroid asteroid)
        {
            asteroid.OnDestroyed -= OnAsteroidDestroyed;
            _spawnedAsteroids.Remove(asteroid);

            AsteroidDestroyed?.Invoke();

            SplitAsteroid(asteroid);
        }

        private void SplitAsteroid(Asteroid asteroid)
        {
            var asteroidConfig = _gameConfigService.GetAsteroidConfigBySize(asteroid.Size);

            if (asteroidConfig.NextSize == AsteroidSize.None)
            {
                return;
            }

            for (var i = 0; i < asteroidConfig.Fragments; i++)
            {
                SpawnAsteroid(asteroidConfig.NextSize, asteroid.transform.position);
            }
        }

        private int CountAsteroidsBySize(AsteroidSize size)
        {
            var count = 0;

            for (var i = 0; i < _spawnedAsteroids.Count; i++)
            {
                var asteroid = _spawnedAsteroids[i];

                if (asteroid.Size == size)
                {
                    count++;
                }
            }

            return count;
        }

        public void Dispose()
        {
            foreach (var asteroid in _spawnedAsteroids)
            {
                if (asteroid != null)
                {
                    asteroid.OnDestroyed -= OnAsteroidDestroyed;
                }
            }
        }
    }
}

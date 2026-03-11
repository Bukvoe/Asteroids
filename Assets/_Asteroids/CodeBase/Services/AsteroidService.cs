using System;
using System.Collections.Generic;
using _Asteroids.CodeBase.Factories;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class AsteroidService : ITickable, IDisposable
    {
        public event Action<Asteroid> AsteroidDestroyed;

        private readonly GenericFactory<Asteroid, AsteroidSpawnPayload> _asteroidFactory;
        private readonly GameMapService _gameMapService;
        private readonly GameConfigService _gameConfigService;
        private readonly RandomService _randomService;

        private readonly List<Asteroid> _spawnedAsteroids = new();
        private readonly AsteroidSize _asteroidToSpawn;
        private readonly int _maxAsteroids;
        private readonly float _maxSpawnCooldown;

        private bool _canSpawn;
        private float _spawnCooldown;

        public AsteroidService(
            GenericFactory<Asteroid, AsteroidSpawnPayload> asteroidFactory,
            GameMapService gameMapService,
            GameConfigService gameConfigService,
            RandomService randomService)
        {
            _asteroidFactory = asteroidFactory;
            _gameMapService = gameMapService;
            _gameConfigService = gameConfigService;
            _randomService = randomService;

            var asteroidSpawnConfig = gameConfigService.AsteroidSpawnConfig;

            _asteroidToSpawn = asteroidSpawnConfig.AsteroidToSpawn;
            _maxAsteroids = asteroidSpawnConfig.MaxAsteroids;
            _maxSpawnCooldown = asteroidSpawnConfig.SpawnCooldown;
        }

        public void Tick()
        {
            if (!_canSpawn)
            {
                return;
            }

            _spawnCooldown = Mathf.Max(_spawnCooldown - Time.deltaTime, 0);

            if (_spawnCooldown <= 0f && CountAsteroidsBySize(_asteroidToSpawn) < _maxAsteroids)
            {
                SpawnAsteroid(_asteroidToSpawn, _gameMapService.GetSpawnRandomPoint());
                _spawnCooldown += _maxSpawnCooldown;
            }
        }

        public void StartSpawning()
        {
            _canSpawn = true;
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

            AsteroidDestroyed?.Invoke(asteroid);

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

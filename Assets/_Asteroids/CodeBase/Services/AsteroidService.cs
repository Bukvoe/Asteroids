using System;
using System.Collections.Generic;
using _Asteroids.CodeBase.Entities.Asteroid;
using _Asteroids.CodeBase.Factories.Payloads;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class AsteroidService : ITickable, IDisposable
    {
        private readonly Asteroid.Factory _asteroidFactory;
        private readonly GameMapService _gameMapService;
        private readonly RandomService _randomService;

        private readonly List<Asteroid> _spawnedAsteroids = new();

        private readonly int _maxAsteroids;

        private readonly float _maxSpawnCooldown;
        private float _spawnCooldown;

        public AsteroidService(
            Asteroid.Factory asteroidFactory,
            GameMapService gameMapService,
            RandomService randomService)
        {
            _asteroidFactory = asteroidFactory;
            _gameMapService = gameMapService;
            _randomService = randomService;

            _maxAsteroids = 5;
            _maxSpawnCooldown = 5;
        }

        public void Tick()
        {
            _spawnCooldown -= Time.deltaTime;

            if (_spawnCooldown <= 0f && _spawnedAsteroids.Count < _maxAsteroids)
            {
                SpawnAsteroid();
                _spawnCooldown += _maxSpawnCooldown;
            }
        }

        private void SpawnAsteroid()
        {
            var spawnPosition = _gameMapService.GetSpawnRandomPoint();
            var rotationAngle = _randomService.RandomAngle();
            var moveDirection = (_gameMapService.GetMapRandomPoint() - spawnPosition).normalized;
            var rotatesClockwise = _randomService.RandomBool();

            var spawnPayload = new AsteroidSpawnPayload(spawnPosition, rotationAngle, moveDirection, rotatesClockwise);
            var asteroid = _asteroidFactory.Create(spawnPayload);

            _spawnedAsteroids.Add(asteroid);
        }

        public void Dispose()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using _Asteroids.CodeBase.Factories;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Gameplay.Ufo;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class EnemyService : ITickable, IDisposable
    {
        public event Action<Ufo> UfoDestroyed;

        private readonly GenericFactory<Ufo, UfoSpawnPayload> _ufoFactory;
        private readonly GameMapService _gameMapService;
        private readonly GameConfigService _gameConfigService;
        private readonly Starship _starship;

        private readonly List<Ufo> _spawnedUfos = new();
        private readonly int _maxUfos;
        private readonly float _maxSpawnCooldown;

        private float _spawnCooldown;

        public EnemyService(
            GenericFactory<Ufo, UfoSpawnPayload> ufoFactory,
            GameMapService gameMapService,
            GameConfigService gameConfigService,
            Starship starship)
        {
            _ufoFactory = ufoFactory;
            _gameMapService = gameMapService;
            _gameConfigService = gameConfigService;
            _starship = starship;

            var enemySpawnConfig = gameConfigService.EnemySpawnConfig;

            _maxUfos = enemySpawnConfig.MaxUfos;
            _maxSpawnCooldown = enemySpawnConfig.SpawnUfoCooldown;
        }

        public void Tick()
        {
            _spawnCooldown = Mathf.Max(_spawnCooldown - Time.deltaTime, 0);

            if (_starship == null)
            {
                return;
            }

            if (_spawnCooldown <= 0f && _spawnedUfos.Count < _maxUfos)
            {
                SpawnUfo(_gameMapService.GetSpawnRandomPoint());
                _spawnCooldown = _maxSpawnCooldown;
            }
        }

        private void SpawnUfo(Vector2 spawnPosition)
        {
            var ufoConfig = _gameConfigService.UfoConfig;

            var spawnPayload = new UfoSpawnPayload(
                spawnPosition,
                ufoConfig.MovementSpeed,
                ufoConfig.BulletWeaponCooldown,
                ufoConfig.BulletSpeed);

            var ufo = _ufoFactory.Create(spawnPayload);

            ufo.OnDestroyed += OnUfoDestroyed;
            _spawnedUfos.Add(ufo);
        }

        private void OnUfoDestroyed(Ufo ufo)
        {
            ufo.OnDestroyed -= OnUfoDestroyed;
            _spawnedUfos.Remove(ufo);

            UfoDestroyed?.Invoke(ufo);
        }

        public void Dispose()
        {
            foreach (var ufo in _spawnedUfos)
            {
                if (ufo != null)
                {
                    ufo.OnDestroyed -= OnUfoDestroyed;
                }
            }
        }
    }
}

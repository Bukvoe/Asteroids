using System;
using System.Collections.Generic;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Ufo;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class EnemyService : ITickable, IDisposable
    {
        public event Action<Ufo> UfoDestroyed;

        private readonly Ufo.Factory _ufoFactory;
        private readonly GameMapService _gameMapService;
        private readonly GameConfigService _gameConfigService;

        private readonly List<Ufo> _spawnedUfos = new();
        private readonly int _maxUfos;
        private readonly float _maxSpawnCooldown;

        private float _spawnCooldown;

        public EnemyService(
            Ufo.Factory ufoFactory,
            GameMapService gameMapService,
            GameConfigService gameConfigService)
        {
            _ufoFactory = ufoFactory;
            _gameMapService = gameMapService;
            _gameConfigService = gameConfigService;

            var enemySpawnConfig = gameConfigService.EnemySpawnConfig;

            _maxUfos = enemySpawnConfig.MaxUfos;
            _maxSpawnCooldown = enemySpawnConfig.SpawnUfoCooldown;
        }

        public void Tick()
        {
            _spawnCooldown -= Time.deltaTime;

            if (_spawnCooldown <= 0f && _spawnedUfos.Count < _maxUfos)
            {
                SpawnUfo(_gameMapService.GetSpawnRandomPoint());
                _spawnCooldown += _maxSpawnCooldown;
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

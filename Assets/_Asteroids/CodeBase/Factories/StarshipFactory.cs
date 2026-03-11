using System;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Services.Asset;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Factories
{
    public class StarshipFactory : IAsyncInitializableFactory, IDisposable
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetService _assetService;

        private Starship _starshipPrefab;

        public StarshipFactory(IInstantiator instantiator, IAssetService assetService)
        {
            _instantiator = instantiator;
            _assetService = assetService;
        }

        public async UniTask InitializeAsync()
        {
            var prefab = await _assetService.LoadAsync<GameObject>(AssetId.STARSHIP);
            _starshipPrefab = prefab.GetComponent<Starship>();
        }

        public Starship Create()
        {
            var instance = _instantiator.InstantiatePrefabForComponent<Starship>(_starshipPrefab);

            return instance;
        }

        public void Dispose()
        {
            _assetService.Release(AssetId.STARSHIP);
        }
    }
}

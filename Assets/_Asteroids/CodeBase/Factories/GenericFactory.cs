using System;
using _Asteroids.CodeBase.Services.Asset;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Factories
{
    public sealed class GenericFactory<T, TPayload> : IAsyncInitializableFactory, IDisposable
        where T : MonoBehaviour
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetService _assetService;

        private readonly string _assetId;
        private T _prefab;

        public GenericFactory(IInstantiator instantiator, IAssetService assetService, string assetId)
        {
            _instantiator = instantiator;
            _assetService = assetService;
            _assetId = assetId;
        }

        public async UniTask InitializeAsync()
        {
            var prefab = await _assetService.LoadAsync<GameObject>(_assetId);
            _prefab = prefab.GetComponent<T>();
        }

        public T Create(TPayload payload)
        {
            var instance = _instantiator.InstantiatePrefabForComponent<T>(
                _prefab,
                extraArgs: new object[] { payload });

            return instance;
        }

        public void Dispose()
        {
            _assetService.Release(_assetId);
        }
    }
}

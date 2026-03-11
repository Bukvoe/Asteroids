using System;
using _Asteroids.CodeBase.Services.Asset;
using _Asteroids.CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Factories
{
    public class UiFactory : IAsyncInitializableFactory, IDisposable
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetService _assetService;

        private HudView _hudPrefab;
        private LoseView _loseScreenPrefab;

        public UiFactory(IInstantiator instantiator, IAssetService assetService)
        {
            _instantiator = instantiator;
            _assetService = assetService;
        }

        public async UniTask InitializeAsync()
        {
            var hudPrefab = await _assetService.LoadAsync<GameObject>(AssetId.HUD);
            _hudPrefab = hudPrefab.GetComponent<HudView>();

            var losePrefab = await _assetService.LoadAsync<GameObject>(AssetId.LOSE_SCREEN);
            _loseScreenPrefab = losePrefab.GetComponent<LoseView>();
        }

        public HudPresenter CreateHud()
        {
            var view = _instantiator.InstantiatePrefabForComponent<HudView>(_hudPrefab);
            var presenter = _instantiator.Instantiate<HudPresenter>(new object[] { view });

            return presenter;
        }

        public LosePresenter CreateLoseScreen()
        {
            var view = _instantiator.InstantiatePrefabForComponent<LoseView>(_loseScreenPrefab);
            var presenter = _instantiator.Instantiate<LosePresenter>(new object[] { view });

            return presenter;
        }

        public void Dispose()
        {
            _assetService.Release(AssetId.HUD);
            _assetService.Release(AssetId.LOSE_SCREEN);
        }
    }
}

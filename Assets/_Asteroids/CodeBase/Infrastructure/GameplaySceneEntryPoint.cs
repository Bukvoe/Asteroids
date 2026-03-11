using System;
using System.Collections.Generic;
using _Asteroids.CodeBase.Factories;
using _Asteroids.CodeBase.Services;
using _Asteroids.CodeBase.UI;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class GameplaySceneEntryPoint : IInitializable, IDisposable
    {
        private readonly List<IAsyncInitializableFactory> _factories;
        private readonly StarshipService _starshipService;
        private readonly UiFactory _uiFactory;
        private readonly InputService _inputService;
        private readonly CurrentRunService _currentRunService;

        private HudPresenter _hudPresenter;

        private LosePresenter _losePresenter;

        public GameplaySceneEntryPoint(
            List<IAsyncInitializableFactory> factories,
            StarshipService starshipService,
            UiFactory uiFactory,
            InputService inputService,
            CurrentRunService currentRunService)
        {
            _factories = factories;
            _starshipService = starshipService;
            _uiFactory = uiFactory;
            _inputService = inputService;
            _currentRunService = currentRunService;
        }

        public void Initialize()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            await UniTask.WhenAll(_factories.Select(x => x.InitializeAsync()));

            _inputService.DeactivateStarshipInput();
            _starshipService.CreateStarship();

            _hudPresenter = _uiFactory.CreateHud();
            _hudPresenter.Initialize();

            _losePresenter = _uiFactory.CreateLoseScreen();
            _losePresenter.Initialize();

            _inputService.ActivateStarshipInput();
            _currentRunService.StartRun();
        }

        public void Dispose()
        {
            _hudPresenter?.Dispose();
            _losePresenter?.Dispose();
        }
    }
}

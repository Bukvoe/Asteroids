using System;
using _Asteroids.CodeBase.Data;
using _Asteroids.CodeBase.Services;
using _Asteroids.CodeBase.Services.SceneLoad;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Asteroids.CodeBase.UI
{
    public class LosePresenter : IInitializable, IDisposable
    {
        private readonly LoseView _view;
        private readonly CurrentRunService _currentRunService;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly PlayerProgress _playerProgress;

        public LosePresenter(
            LoseView view,
            CurrentRunService currentRunService,
            ISceneLoadService sceneLoadService,
            PlayerProgress playerProgress)
        {
            _view = view;
            _currentRunService = currentRunService;
            _sceneLoadService = sceneLoadService;
            _playerProgress = playerProgress;
        }

        public void Initialize()
        {
            _currentRunService.RunEnded += OnRunEnded;
            _view.MainMenuRequested += OnMainMenuRequested;
            _view.RestartRequested += OnRestartRequested;
            _view.ReviveRequested += OnReviveRequested;

            _view.Hide();
        }

        public void Dispose()
        {
            _currentRunService.RunEnded -= OnRunEnded;
            _view.MainMenuRequested -= OnMainMenuRequested;
            _view.RestartRequested -= OnRestartRequested;
            _view.ReviveRequested -= OnReviveRequested;
        }

        private void OnRunEnded()
        {
            _view.UpdateScore(_currentRunService.Score);

            _view.UpdateBestScore(_playerProgress.BestScore);
            _view.UpdateRuns(_playerProgress.Runs);
            _view.UpdateUfoDestroyed(_playerProgress.UfoDestroyed);

            _view.Show();
        }

        private void OnMainMenuRequested()
        {
            _sceneLoadService.LoadSceneAsync(GameScene.MainMenu).Forget();
        }

        private void OnRestartRequested()
        {
            _sceneLoadService.ReloadCurrentSceneAsync().Forget();
        }

        private void OnReviveRequested()
        {
        }
    }
}

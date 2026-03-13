using System;
using _Asteroids.CodeBase.Data;
using _Asteroids.CodeBase.Services;
using _Asteroids.CodeBase.Services.Ad;
using _Asteroids.CodeBase.Services.SceneLoad;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Asteroids.CodeBase.UI
{
    public class LosePresenter : IInitializable, IDisposable
    {
        private const string REWARDED_REVIVE_PLACEMENT = "RewardedRevive";
        private const string INTERSTITIAL_GAME_OVER_PLACEMENT = "InterstitialGameOver";

        private readonly LoseView _view;
        private readonly CurrentRunService _currentRunService;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly PlayerProgress _playerProgress;
        private readonly IAdService _adService;

        private bool _adRewardedShown;
        private bool _rewardReceived;

        public LosePresenter(
            LoseView view,
            CurrentRunService currentRunService,
            ISceneLoadService sceneLoadService,
            PlayerProgress playerProgress,
            IAdService adService)
        {
            _view = view;
            _currentRunService = currentRunService;
            _sceneLoadService = sceneLoadService;
            _playerProgress = playerProgress;
            _adService = adService;
        }

        public void Initialize()
        {
            _currentRunService.RunEnded += OnRunEnded;
            _view.MainMenuRequested += OnMainMenuRequested;
            _view.RestartRequested += OnRestartRequested;
            _view.ReviveRequested += OnReviveRequested;
            _adService.Rewarded += OnRewarded;

            _view.Hide();
        }

        public void Dispose()
        {
            _currentRunService.RunEnded -= OnRunEnded;
            _view.MainMenuRequested -= OnMainMenuRequested;
            _view.RestartRequested -= OnRestartRequested;
            _view.ReviveRequested -= OnReviveRequested;
            _adService.Rewarded -= OnRewarded;
        }

        private void OnRunEnded()
        {
            _view.UpdateScore(_currentRunService.Score);

            _view.UpdateBestScore(_playerProgress.BestScore);
            _view.UpdateRuns(_playerProgress.Runs);
            _view.UpdateUfoDestroyed(_playerProgress.UfoDestroyed);
            _view.UpdateReviveButton(IsReviveButtonActive());

            _view.Show();
        }

        private void OnMainMenuRequested()
        {
            OnMainMenuRequestedAsync().Forget();
        }

        private async UniTask OnMainMenuRequestedAsync()
        {
            await ShowShortAd();

            _sceneLoadService.LoadSceneAsync(GameScene.MainMenu).Forget();
        }

        private void OnRestartRequested()
        {
            OnRestartRequestedAsync().Forget();
        }

        private async UniTask OnRestartRequestedAsync()
        {
            await ShowShortAd();

            _sceneLoadService.ReloadCurrentSceneAsync().Forget();
        }

        private void OnReviveRequested()
        {
            ShowRewardedAsync().Forget();
        }

        private bool IsReviveButtonActive()
        {
            return !_adRewardedShown && _currentRunService.CanRevive();
        }

        private async UniTask ShowShortAd()
        {
            if (_rewardReceived)
            {
                return;
            }

            var isLoaded = await _adService.LoadInterstitialAsync();

            if (!isLoaded)
            {
                return;
            }

            await _adService.ShowInterstitialAsync(INTERSTITIAL_GAME_OVER_PLACEMENT);
        }

        private async UniTask ShowRewardedAsync()
        {
            if (_adRewardedShown)
            {
                return;
            }

            _adRewardedShown = true;

            _view.UpdateReviveButton(IsReviveButtonActive());

            var isAdLoaded = await _adService.LoadRewardedAsync();

            if (!isAdLoaded)
            {
                return;
            }

            await _adService.ShowRewardedAsync(REWARDED_REVIVE_PLACEMENT);
        }

        private void OnRewarded(string placementName)
        {
            if (!placementName.Equals(REWARDED_REVIVE_PLACEMENT))
            {
                return;
            }

            _rewardReceived = true;
            _view.Hide();
            _currentRunService.ReviveStarship();
        }
    }
}

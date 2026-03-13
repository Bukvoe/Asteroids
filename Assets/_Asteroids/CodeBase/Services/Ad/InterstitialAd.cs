using System;
using Cysharp.Threading.Tasks;
using Unity.Services.LevelPlay;

namespace _Asteroids.CodeBase.Services.Ad
{
    public class InterstitialAd : IDisposable
    {
        private readonly LevelPlayInterstitialAd _interstitialAd;

        private UniTaskCompletionSource<bool> _loadCompletionSource;
        private UniTaskCompletionSource _showCompletionSource;

        public InterstitialAd(string unitId)
        {
            _interstitialAd = new LevelPlayInterstitialAd(unitId);

            _interstitialAd.OnAdLoaded += OnLoaded;
            _interstitialAd.OnAdLoadFailed += OnLoadFailed;
            _interstitialAd.OnAdDisplayFailed += OnDisplayFailed;
            _interstitialAd.OnAdClosed += OnClosed;
        }

        public async UniTask<bool> LoadAsync()
        {
            if (_loadCompletionSource != null)
            {
                return await _loadCompletionSource.Task;
            }

            _loadCompletionSource = new UniTaskCompletionSource<bool>();

            _interstitialAd.LoadAd();

            var result = await _loadCompletionSource.Task;
            _loadCompletionSource = null;

            return result;
        }

        public async UniTask ShowAsync(string placementName)
        {
            if (_showCompletionSource != null)
            {
                await _showCompletionSource.Task;
            }

            _showCompletionSource = new UniTaskCompletionSource();

            if (_interstitialAd.IsAdReady() && !LevelPlayInterstitialAd.IsPlacementCapped(placementName))
            {
                _interstitialAd.ShowAd(placementName);
            }
            else
            {
                _showCompletionSource.TrySetResult();
            }

            await _showCompletionSource.Task;
            _showCompletionSource = null;
        }

        private void OnLoaded(LevelPlayAdInfo adInfo)
        {
            _loadCompletionSource?.TrySetResult(true);
        }

        private void OnLoadFailed(LevelPlayAdError error)
        {
            _loadCompletionSource?.TrySetResult(false);
        }

        private void OnClosed(LevelPlayAdInfo adInfo)
        {
            _showCompletionSource.TrySetResult();
        }

        private void OnDisplayFailed(LevelPlayAdInfo adInfo, LevelPlayAdError error)
        {
            _showCompletionSource.TrySetResult();
        }

        public void Dispose()
        {
            _interstitialAd.OnAdLoaded -= OnLoaded;
            _interstitialAd.OnAdLoadFailed -= OnLoadFailed;
            _interstitialAd.OnAdDisplayFailed -= OnDisplayFailed;
            _interstitialAd.OnAdClosed -= OnClosed;
        }
    }
}

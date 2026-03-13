using System;
using Cysharp.Threading.Tasks;
using Unity.Services.LevelPlay;

namespace _Asteroids.CodeBase.Services.Ad
{
    public class RewardedAd : IDisposable
    {
        public event Action<string> Rewarded;

        private readonly LevelPlayRewardedAd _rewardedAd;

        private UniTaskCompletionSource<bool> _loadCompletionSource;
        private UniTaskCompletionSource _showCompletionSource;

        public RewardedAd(string unitId)
        {
            _rewardedAd = new LevelPlayRewardedAd(unitId);

            _rewardedAd.OnAdLoaded += OnLoaded;
            _rewardedAd.OnAdLoadFailed += OnLoadFailed;
            _rewardedAd.OnAdDisplayFailed += OnDisplayFailed;
            _rewardedAd.OnAdRewarded += OnRewarded;
            _rewardedAd.OnAdClosed += OnClosed;
        }

        public async UniTask<bool> LoadAsync()
        {
            if (_loadCompletionSource != null)
            {
                return await _loadCompletionSource.Task;
            }

            _loadCompletionSource = new UniTaskCompletionSource<bool>();

            _rewardedAd.LoadAd();

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

            if (_rewardedAd.IsAdReady() && !LevelPlayRewardedAd.IsPlacementCapped(placementName))
            {
                _rewardedAd.ShowAd(placementName);
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

        private void OnDisplayFailed(LevelPlayAdInfo adInfo, LevelPlayAdError error)
        {
            _showCompletionSource.TrySetResult();
        }

        private void OnRewarded(LevelPlayAdInfo adInfo, LevelPlayReward adReward)
        {
            Rewarded?.Invoke(adInfo.PlacementName);
        }

        private void OnClosed(LevelPlayAdInfo adInfo)
        {
            _showCompletionSource.TrySetResult();
        }

        public void Dispose()
        {
            _rewardedAd.OnAdLoaded -= OnLoaded;
            _rewardedAd.OnAdLoadFailed -= OnLoadFailed;
            _rewardedAd.OnAdDisplayFailed -= OnDisplayFailed;
            _rewardedAd.OnAdRewarded -= OnRewarded;
            _rewardedAd.OnAdClosed -= OnClosed;
        }
    }
}

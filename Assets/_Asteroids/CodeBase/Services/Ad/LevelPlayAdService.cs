using System;
using _Asteroids.CodeBase.Configs;
using Cysharp.Threading.Tasks;
using Unity.Services.LevelPlay;
using UnityEngine;

namespace _Asteroids.CodeBase.Services.Ad
{
    public class LevelPlayAdService : IAdService, IDisposable
    {
        public event Action<string> Rewarded;

        private readonly string _interstitialAdUnitId;
        private readonly string _rewardedAdUnitId;

        private RewardedAd _rewardedAd;
        private InterstitialAd _interstitialAd;

        public LevelPlayAdService(AdConfig adConfig)
        {
            _interstitialAdUnitId = adConfig.InterstitialAdUnitId;
            _rewardedAdUnitId = adConfig.RewardedAdUnitId;

            LevelPlay.OnInitSuccess += OnInitCompleted;
            LevelPlay.OnInitFailed += OnInitFailed;

            LevelPlay.Init(adConfig.AppKey);
        }

        public async UniTask<bool> LoadRewardedAsync()
        {
            if (_rewardedAd == null)
            {
                return false;
            }

            return await _rewardedAd.LoadAsync();
        }

        public async UniTask ShowRewardedAsync(string placementName)
        {
            if (_rewardedAd == null)
            {
                return;
            }

            await _rewardedAd.ShowAsync(placementName);
        }

        public async UniTask<bool> LoadInterstitialAsync()
        {
            if (_interstitialAd == null)
            {
                return false;
            }

            return await _interstitialAd.LoadAsync();
        }

        public async UniTask ShowInterstitialAsync(string placementName)
        {
            if (_interstitialAd == null)
            {
                return;
            }

            await _interstitialAd.ShowAsync(placementName);
        }

        private void OnInitCompleted(LevelPlayConfiguration config)
        {
            _rewardedAd = new RewardedAd(_rewardedAdUnitId);
            _rewardedAd.Rewarded += OnRewarded;

            _interstitialAd = new InterstitialAd(_interstitialAdUnitId);
        }

        private void OnInitFailed(LevelPlayInitError error)
        {
            Debug.LogError($"LevelPlay Init Failed: {error}");
        }

        private void OnRewarded(string placementName)
        {
            Rewarded?.Invoke(placementName);
        }

        public void Dispose()
        {
            LevelPlay.OnInitSuccess -= OnInitCompleted;
            LevelPlay.OnInitFailed -= OnInitFailed;

            if (_rewardedAd != null)
            {
                _rewardedAd.Dispose();
                _rewardedAd.Rewarded -= OnRewarded;
            }

            _interstitialAd?.Dispose();
        }
    }
}

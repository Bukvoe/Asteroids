using System;
using Cysharp.Threading.Tasks;

namespace _Asteroids.CodeBase.Services.Ad
{
    public interface IAdService
    {
        public event Action<string> Rewarded;

        public UniTask<bool> LoadRewardedAsync();
        public UniTask ShowRewardedAsync(string placementName);

        public UniTask<bool> LoadInterstitialAsync();
        public UniTask ShowInterstitialAsync(string placementName);
    }
}

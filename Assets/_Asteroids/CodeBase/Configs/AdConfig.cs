using UnityEngine;

namespace _Asteroids.CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/AdConfig")]
    public class AdConfig : ScriptableObject
    {
        public string AppKey;

        public string InterstitialAdUnitId;
        public string RewardedAdUnitId;
    }
}

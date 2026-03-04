using _Asteroids.CodeBase.Data;
using UnityEngine;

namespace _Asteroids.CodeBase.Services.Save
{
    public class PlayerPrefsSaveService : ISaveService
    {
        private const string PlayerProgressKey = "PlayerProgress";

        public void Save(PlayerProgress playerProgress)
        {
            var json = JsonUtility.ToJson(playerProgress);
            PlayerPrefs.SetString(PlayerProgressKey, json);
            PlayerPrefs.Save();
        }

        public PlayerProgress Load()
        {
            if (!PlayerPrefs.HasKey(PlayerProgressKey))
            {
                return new PlayerProgress(bestScore: 0, runs: 0, ufoDestroyed: 0);
            }

            var json = PlayerPrefs.GetString(PlayerProgressKey);
            return JsonUtility.FromJson<PlayerProgress>(json);
        }
    }
}

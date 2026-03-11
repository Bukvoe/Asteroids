using _Asteroids.CodeBase.Data;
using UnityEngine;

namespace _Asteroids.CodeBase.Services.Save
{
    public class PlayerPrefsSaveService : ISaveService
    {
        private const string PLAYER_PROGRESS_KEY = "PlayerProgress";

        public void Save(PlayerProgress playerProgress)
        {
            var json = JsonUtility.ToJson(playerProgress);
            PlayerPrefs.SetString(PLAYER_PROGRESS_KEY, json);
            PlayerPrefs.Save();
        }

        public PlayerProgress Load()
        {
            if (!PlayerPrefs.HasKey(PLAYER_PROGRESS_KEY))
            {
                return new PlayerProgress(bestScore: 0, runs: 0, ufoDestroyed: 0);
            }

            var json = PlayerPrefs.GetString(PLAYER_PROGRESS_KEY);
            return JsonUtility.FromJson<PlayerProgress>(json);
        }
    }
}

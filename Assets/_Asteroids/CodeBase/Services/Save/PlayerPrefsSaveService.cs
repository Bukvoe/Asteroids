using _Asteroids.CodeBase.Data;
using UnityEngine;

namespace _Asteroids.CodeBase.Services.Save
{
    public class PlayerPrefsSaveService : ISaveService
    {
        public void Save(PlayerProgress playerProgress)
        {
            var json = JsonUtility.ToJson(playerProgress);
            PlayerPrefs.SetString(SaveKeys.PlayerProgress, json);
            PlayerPrefs.Save();
        }

        public PlayerProgress Load()
        {
            if (!PlayerPrefs.HasKey(SaveKeys.PlayerProgress))
            {
                return new PlayerProgress(bestScore: 0, runs: 0, ufoDestroyed: 0);
            }

            var json = PlayerPrefs.GetString(SaveKeys.PlayerProgress);
            return JsonUtility.FromJson<PlayerProgress>(json);
        }
    }
}

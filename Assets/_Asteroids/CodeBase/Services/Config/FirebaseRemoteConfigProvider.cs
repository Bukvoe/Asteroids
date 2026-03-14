using System;
using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Services.Analytics;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using UnityEngine;

namespace _Asteroids.CodeBase.Services.Config
{
    public class FirebaseRemoteConfigProvider : IConfigProvider
    {
        private readonly FirebaseInitializer _firebaseInitializer;

        public FirebaseRemoteConfigProvider(FirebaseInitializer firebaseInitializer)
        {
            _firebaseInitializer = firebaseInitializer;
        }

        public async UniTask InitializeAsync()
        {
            await _firebaseInitializer.InitializeAsync();

            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;

            await remoteConfig.FetchAsync(TimeSpan.Zero);
            await remoteConfig.ActivateAsync();
        }

        public GameConfig GetConfig()
        {
            var json = FirebaseRemoteConfig.DefaultInstance.GetValue("game_config").StringValue;
            var config = JsonUtility.FromJson<GameConfig>(json);

            return config;
        }
    }
}

using _Asteroids.CodeBase.Common;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace _Asteroids.CodeBase.Services.Analytics
{
    public class FirebaseInitializer : IAsyncInitializable
    {
        private UniTaskCompletionSource _initializationTask;
        private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;

        public async UniTask InitializeAsync()
        {
            if (_initializationTask == null)
            {
                _initializationTask = new UniTaskCompletionSource();
                await InitializeInternalAsync();
            }

            await _initializationTask.Task;
        }

        private async UniTask InitializeInternalAsync()
        {
            try
            {
                var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

                if (dependencyStatus != DependencyStatus.Available)
                {
                    Debug.LogError($"Could not resolve Firebase dependencies: {dependencyStatus}");
                    return;
                }

                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                Debug.Log("FirebaseCoreService initialized");
            }
            finally
            {
                _initializationTask.TrySetResult();
            }
        }
    }
}

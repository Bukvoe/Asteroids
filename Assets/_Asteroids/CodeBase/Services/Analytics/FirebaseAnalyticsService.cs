using System.Threading.Tasks;
using _Asteroids.CodeBase.Data;
using Firebase;
using Firebase.Analytics;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Services.Analytics
{
    public class FirebaseAnalyticsService : IAnalyticsService, IInitializable
    {
        private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;

        public void Initialize()
        {
            _ = InitializeFirebaseAsync();
        }

        private async Task InitializeFirebaseAsync()
        {
            _dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (_dependencyStatus != DependencyStatus.Available)
            {
                Debug.LogError($"Could not resolve Firebase dependencies: {_dependencyStatus}");
                return;
            }

            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            Debug.Log("Firebase initialized");
        }

        public void TrackRunStarted()
        {
            FirebaseAnalytics.LogEvent("run_started");
        }

        public void TrackRunEnded(RunStats runStats)
        {
            FirebaseAnalytics.LogEvent(
                "run_ended",
                new Parameter("bullets_fired", runStats.BulletsFired),
                new Parameter("lasers_fired", runStats.LasersFired),
                new Parameter("asteroids_destroyed", runStats.AsteroidsDestroyed),
                new Parameter("ufo_destroyed", runStats.UfosDestroyed)
            );
        }

        public void TrackLaserFired()
        {
            FirebaseAnalytics.LogEvent("laser_fired");
        }
    }
}

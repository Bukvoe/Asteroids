using _Asteroids.CodeBase.Data;
using Cysharp.Threading.Tasks;
using Firebase.Analytics;

namespace _Asteroids.CodeBase.Services.Analytics
{
    public class FirebaseAnalyticsService : IAnalyticsService
    {
        private readonly FirebaseInitializer _firebaseInitializer;

        public FirebaseAnalyticsService(FirebaseInitializer firebaseInitializer)
        {
            _firebaseInitializer = firebaseInitializer;
        }

        public async UniTask InitializeAsync()
        {
            await _firebaseInitializer.InitializeAsync();
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

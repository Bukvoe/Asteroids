using _Asteroids.CodeBase.Data;

namespace _Asteroids.CodeBase.Services.Analytics
{
    public interface IAnalyticsService
    {
        public void TrackRunStarted();
        public void TrackRunEnded(RunStats runStats);
        public void TrackLaserFired();
    }
}

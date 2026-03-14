using _Asteroids.CodeBase.Common;
using _Asteroids.CodeBase.Data;

namespace _Asteroids.CodeBase.Services.Analytics
{
    public interface IAnalyticsService : IAsyncInitializable
    {
        public void TrackRunStarted();
        public void TrackRunEnded(RunStats runStats);
        public void TrackLaserFired();
    }
}

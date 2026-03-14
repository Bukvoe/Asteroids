using _Asteroids.CodeBase.Common;
using _Asteroids.CodeBase.Configs;

namespace _Asteroids.CodeBase.Services.Config
{
    public interface IConfigProvider : IAsyncInitializable
    {
        public GameConfig GetConfig();
    }
}

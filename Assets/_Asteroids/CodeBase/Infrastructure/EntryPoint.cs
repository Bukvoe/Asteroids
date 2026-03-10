using _Asteroids.CodeBase.Services.SceneLoad;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class EntryPoint : IInitializable
    {
        private readonly ISceneLoadService _sceneLoadService;

        public EntryPoint(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            _sceneLoadService.LoadScene(GameScene.MainMenu);
        }
    }
}

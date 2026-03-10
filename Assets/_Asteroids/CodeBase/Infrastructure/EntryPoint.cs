using _Asteroids.CodeBase.Services.Asset;
using _Asteroids.CodeBase.Services.SceneLoad;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class EntryPoint : IInitializable
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IAssetService _assetService;

        public EntryPoint(ISceneLoadService sceneLoadService, IAssetService assetService)
        {
            _sceneLoadService = sceneLoadService;
            _assetService = assetService;
        }

        public void Initialize()
        {
            InitializeAsync().Forget();
        }

        private async UniTask InitializeAsync()
        {
            await _assetService.InitializeAsync();

            _sceneLoadService.LoadScene(GameScene.MainMenu);
        }
    }
}

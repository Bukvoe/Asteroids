using _Asteroids.CodeBase.Services.Ad;
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
        private readonly IAdService _adService;

        public EntryPoint(ISceneLoadService sceneLoadService, IAssetService assetService, IAdService adService)
        {
            _sceneLoadService = sceneLoadService;
            _assetService = assetService;
            _adService = adService;
        }

        public void Initialize()
        {
            InitializeAsync().Forget();
        }

        private async UniTask InitializeAsync()
        {
            await _assetService.InitializeAsync();

            await _sceneLoadService.LoadSceneAsync(GameScene.MainMenu);
        }
    }
}

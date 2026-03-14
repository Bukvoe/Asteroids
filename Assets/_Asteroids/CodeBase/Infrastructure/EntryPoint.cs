using _Asteroids.CodeBase.Services.Ad;
using _Asteroids.CodeBase.Services.Analytics;
using _Asteroids.CodeBase.Services.Asset;
using _Asteroids.CodeBase.Services.Config;
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
        private readonly IAnalyticsService _analyticsService;
        private readonly IConfigProvider _configProvider;

        public EntryPoint(
            ISceneLoadService sceneLoadService,
            IAssetService assetService,
            IAdService adService,
            IAnalyticsService analyticsService,
            IConfigProvider configProvider)
        {
            _sceneLoadService = sceneLoadService;
            _assetService = assetService;
            _adService = adService;
            _analyticsService = analyticsService;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            InitializeAsync().Forget();
        }

        private async UniTask InitializeAsync()
        {
            await UniTask.WhenAll(
                _assetService.InitializeAsync(),
                _analyticsService.InitializeAsync(),
                _configProvider.InitializeAsync());

            await _sceneLoadService.LoadSceneAsync(GameScene.MainMenu);
        }
    }
}

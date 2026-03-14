using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Data;
using _Asteroids.CodeBase.Services.Ad;
using _Asteroids.CodeBase.Services.Analytics;
using _Asteroids.CodeBase.Services.Asset;
using _Asteroids.CodeBase.Services.Config;
using _Asteroids.CodeBase.Services.Save;
using _Asteroids.CodeBase.Services.SceneLoad;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField, Required] private AdConfig _adConfig;

        public override void InstallBindings()
        {
            Container.Bind<IAssetService>().To<AddressablesAssetService>().AsSingle();
            Container.Bind<ISaveService>().To<PlayerPrefsSaveService>().AsSingle();
            Container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelPlayAdService>().AsSingle().WithArguments(_adConfig);
            Container.Bind<FirebaseInitializer>().AsSingle();
            Container.Bind<IConfigProvider>().To<FirebaseRemoteConfigProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();

            Container.Bind<PlayerProgress>().FromMethod(ctx =>
            {
                var saveService = ctx.Container.Resolve<ISaveService>();
                return saveService.Load();
            }).AsSingle();

            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }
    }
}

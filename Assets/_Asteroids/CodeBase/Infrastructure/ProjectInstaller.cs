using _Asteroids.CodeBase.Data;
using _Asteroids.CodeBase.Services.Save;
using _Asteroids.CodeBase.Services.SceneLoad;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveService>().To<PlayerPrefsSaveService>().AsSingle();
            Container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();

            Container.Bind<PlayerProgress>().FromMethod(ctx =>
            {
                var saveService = ctx.Container.Resolve<ISaveService>();
                return saveService.Load();
            }).AsSingle();

            Container.BindInterfacesTo<EntryPoint>().AsSingle().NonLazy();
        }
    }
}

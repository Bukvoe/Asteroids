using _Asteroids.CodeBase.Services;
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
            Container.BindInterfacesAndSelfTo<PlayerProgressService>().AsSingle();
            Container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();

            Container.BindInterfacesTo<EntryPoint>().AsSingle().NonLazy();
        }
    }
}

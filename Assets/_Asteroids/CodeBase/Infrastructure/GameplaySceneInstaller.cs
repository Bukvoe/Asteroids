using _Asteroids.CodeBase.Entities.Starship;
using _Asteroids.CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField, Required] private Starship _starship;

        public override void InstallBindings()
        {
            Container.Bind<Starship>().FromInstance(_starship).AsSingle();

            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();
        }
    }
}

using _Asteroids.CodeBase.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField, Required] private MainMenuView _mainMenuViewPrefab;

        public override void InstallBindings()
        {
            Container.Bind<MainMenuView>().FromComponentInNewPrefab(_mainMenuViewPrefab).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle().NonLazy();
        }
    }
}

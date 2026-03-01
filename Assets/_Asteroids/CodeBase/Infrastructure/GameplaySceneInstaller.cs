using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Gameplay.Weapons;
using _Asteroids.CodeBase.Services;
using _Asteroids.CodeBase.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField, Required] private Camera _camera;

        [SerializeField, Required] private Starship _starship;

        [SerializeField, Required] private BulletWeapon _bulletWeaponPrefab;
        [SerializeField, Required] private Bullet _bulletPrefab;

        [SerializeField, Required] private Asteroid _asteroidPrefab;

        [SerializeField, Required] private HudView _hudView;
        [SerializeField, Required] private LoseView _loseView;

        public override void InstallBindings()
        {
            Container.Bind<Starship>().FromInstance(_starship).AsSingle();

            Container.BindFactory<BulletWeaponSpawnPayload, BulletWeapon, BulletWeapon.Factory>().FromComponentInNewPrefab(_bulletWeaponPrefab);
            Container.BindFactory<BulletSpawnPayload, Bullet, Bullet.Factory>().FromComponentInNewPrefab(_bulletPrefab);
            Container.BindFactory<AsteroidSpawnPayload, Asteroid, Asteroid.Factory>().FromComponentInNewPrefab(_asteroidPrefab);

            Container.Bind<SceneObjectService>().AsSingle().WithArguments(_camera);
            Container.Bind<RandomService>().AsSingle();
            Container.Bind<GameMapService>().AsSingle();
            Container.BindInterfacesTo<AsteroidService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();

            Container.Bind<HudView>().FromInstance(_hudView).AsSingle();
            Container.BindInterfacesAndSelfTo<HudPresenter>().AsSingle();
            Container.Bind<LoseView>().FromInstance(_loseView).AsSingle();
            Container.BindInterfacesAndSelfTo<LosePresenter>().AsSingle();
        }
    }
}

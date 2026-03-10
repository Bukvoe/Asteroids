using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Factories;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Gameplay.Ufo;
using _Asteroids.CodeBase.Gameplay.Weapons;
using _Asteroids.CodeBase.Services;
using _Asteroids.CodeBase.Services.Asset;
using _Asteroids.CodeBase.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField, Required] private Camera _camera;
        [SerializeField, Required] private GameConfig _gameConfig;

        [SerializeField, Required] private Starship _starshipPrefab;

        [SerializeField, Required] private BulletWeapon _bulletWeaponPrefab;
        [SerializeField, Required] private Bullet _bulletPrefab;

        [SerializeField, Required] private LaserWeapon _laserWeaponPrefab;
        [SerializeField, Required] private Laser _laserPrefab;

        [SerializeField, Required] private Asteroid _asteroidPrefab;
        [SerializeField, Required] private Ufo _ufoPrefab;

        [SerializeField, Required] private HudView _hudViewPrefab;
        [SerializeField, Required] private LoseView _loseViewPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GenericFactory<BulletWeapon, BulletWeaponSpawnPayload>>().AsSingle().WithArguments(AssetId.BULLET_WEAPON);
            Container.BindInterfacesAndSelfTo<GenericFactory<Bullet, BulletSpawnPayload>>().AsSingle().WithArguments(AssetId.BULLET);
            Container.BindInterfacesAndSelfTo<GenericFactory<LaserWeapon, LaserWeaponSpawnPayload>>().AsSingle().WithArguments(AssetId.LASER_WEAPON);
            Container.BindInterfacesAndSelfTo<GenericFactory<Laser, LaserSpawnPayload>>().AsSingle().WithArguments(AssetId.LASER);
            Container.BindInterfacesAndSelfTo<GenericFactory<Asteroid, AsteroidSpawnPayload>>().AsSingle().WithArguments(AssetId.ASTEROID);
            Container.BindInterfacesAndSelfTo<GenericFactory<Ufo, UfoSpawnPayload>>().AsSingle().WithArguments(AssetId.UFO);

            Container.Bind<GameConfigService>().AsSingle().WithArguments(_gameConfig);
            Container.Bind<SceneObjectService>().AsSingle().WithArguments(_camera);
            Container.Bind<RandomService>().AsTransient();
            Container.Bind<GameMapService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CurrentRunService>().AsSingle().NonLazy();

            Container.Bind<Starship>().FromComponentInNewPrefab(_starshipPrefab).AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();

            Container.Bind<HudView>().FromComponentInNewPrefab(_hudViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<HudPresenter>().AsSingle();
            Container.Bind<LoseView>().FromComponentInNewPrefab(_loseViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<LosePresenter>().AsSingle();
        }
    }
}

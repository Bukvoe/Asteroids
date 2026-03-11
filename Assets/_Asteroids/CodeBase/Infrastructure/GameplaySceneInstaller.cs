using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Factories;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using _Asteroids.CodeBase.Gameplay.Ufo;
using _Asteroids.CodeBase.Gameplay.Weapons;
using _Asteroids.CodeBase.Services;
using _Asteroids.CodeBase.Services.Asset;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.Infrastructure
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField, Required] private Camera _camera;
        [SerializeField, Required] private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GenericFactory<BulletWeapon, BulletWeaponSpawnPayload>>().AsSingle().WithArguments(AssetId.BULLET_WEAPON);
            Container.BindInterfacesAndSelfTo<GenericFactory<Bullet, BulletSpawnPayload>>().AsSingle().WithArguments(AssetId.BULLET);
            Container.BindInterfacesAndSelfTo<GenericFactory<LaserWeapon, LaserWeaponSpawnPayload>>().AsSingle().WithArguments(AssetId.LASER_WEAPON);
            Container.BindInterfacesAndSelfTo<GenericFactory<Laser, LaserSpawnPayload>>().AsSingle().WithArguments(AssetId.LASER);
            Container.BindInterfacesAndSelfTo<GenericFactory<Asteroid, AsteroidSpawnPayload>>().AsSingle().WithArguments(AssetId.ASTEROID);
            Container.BindInterfacesAndSelfTo<GenericFactory<Ufo, UfoSpawnPayload>>().AsSingle().WithArguments(AssetId.UFO);
            Container.BindInterfacesAndSelfTo<StarshipFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UiFactory>().AsSingle();

            Container.Bind<GameConfigService>().AsSingle().WithArguments(_gameConfig);
            Container.Bind<SceneObjectService>().AsSingle().WithArguments(_camera);
            Container.Bind<RandomService>().AsTransient();
            Container.Bind<GameMapService>().AsSingle();
            Container.Bind<StarshipService>().AsSingle();

            Container.BindInterfacesAndSelfTo<AsteroidService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CurrentRunService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();

            Container.BindInterfacesTo<GameplaySceneEntryPoint>().AsSingle().NonLazy();
        }
    }
}

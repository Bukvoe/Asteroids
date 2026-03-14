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

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MonoFactory<BulletWeapon, BulletWeaponSpawnPayload>>().AsSingle().WithArguments(AssetId.BULLET_WEAPON);
            Container.BindInterfacesAndSelfTo<MonoFactory<Bullet, BulletSpawnPayload>>().AsSingle().WithArguments(AssetId.BULLET);
            Container.BindInterfacesAndSelfTo<MonoFactory<LaserWeapon, LaserWeaponSpawnPayload>>().AsSingle().WithArguments(AssetId.LASER_WEAPON);
            Container.BindInterfacesAndSelfTo<MonoFactory<Laser, LaserSpawnPayload>>().AsSingle().WithArguments(AssetId.LASER);
            Container.BindInterfacesAndSelfTo<MonoFactory<Asteroid, AsteroidSpawnPayload>>().AsSingle().WithArguments(AssetId.ASTEROID);
            Container.BindInterfacesAndSelfTo<MonoFactory<Ufo, UfoSpawnPayload>>().AsSingle().WithArguments(AssetId.UFO);
            Container.BindInterfacesAndSelfTo<StarshipFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UiFactory>().AsSingle();

            Container.Bind<GameConfigService>().AsSingle();
            Container.Bind<SceneObjectService>().AsSingle().WithArguments(_camera);
            Container.Bind<RandomService>().AsTransient();
            Container.Bind<GameMapService>().AsSingle();
            Container.Bind<StarshipService>().AsSingle();

            Container.BindInterfacesAndSelfTo<AsteroidService>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrentRunService>().AsSingle();

            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();

            Container.BindInterfacesTo<GameplaySceneEntryPoint>().AsSingle();
        }
    }
}

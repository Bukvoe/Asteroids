using _Asteroids.CodeBase.Configs;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Gameplay.Asteroid;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Gameplay.Ufo;
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
        [SerializeField, Required] private GameConfig _gameConfig;

        [SerializeField, Required] private Starship _starshipPrefab;

        [SerializeField, Required] private BulletWeapon _bulletWeaponPrefab;
        [SerializeField, Required] private Bullet _bulletPrefab;

        [SerializeField, Required] private LaserWeapon _laserWeaponPrefab;
        [SerializeField, Required] private Laser _laserPrefab;

        [SerializeField, Required] private Asteroid _asteroidPrefab;
        [SerializeField, Required] private Ufo _ufoPrefab;

        [SerializeField, Required] private Transform _uiRoot;
        [SerializeField, Required] private HudView _hudViewPrefab;
        [SerializeField, Required] private LoseView _loseViewPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<BulletWeaponSpawnPayload, BulletWeapon, BulletWeapon.Factory>().FromComponentInNewPrefab(_bulletWeaponPrefab);
            Container.BindFactory<BulletSpawnPayload, Bullet, Bullet.Factory>().FromComponentInNewPrefab(_bulletPrefab);
            Container.BindFactory<LaserWeaponSpawnPayload, LaserWeapon, LaserWeapon.Factory>().FromComponentInNewPrefab(_laserWeaponPrefab);
            Container.BindFactory<LaserSpawnPayload, Laser, Laser.Factory>().FromComponentInNewPrefab(_laserPrefab);
            Container.BindFactory<AsteroidSpawnPayload, Asteroid, Asteroid.Factory>().FromComponentInNewPrefab(_asteroidPrefab);
            Container.BindFactory<UfoSpawnPayload, Ufo, Ufo.Factory>().FromComponentInNewPrefab(_ufoPrefab);

            Container.Bind<GameConfigService>().AsSingle().WithArguments(_gameConfig);
            Container.Bind<SceneObjectService>().AsSingle().WithArguments(_camera);
            Container.Bind<RandomService>().AsTransient();
            Container.Bind<GameMapService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameSessionService>().AsSingle().NonLazy();

            Container.Bind<Starship>().FromComponentInNewPrefab(_starshipPrefab).AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();

            Container.Bind<HudView>().FromComponentInNewPrefab(_hudViewPrefab).UnderTransform(_uiRoot).AsSingle();
            Container.BindInterfacesAndSelfTo<HudPresenter>().AsSingle();
            Container.Bind<LoseView>().FromComponentInNewPrefab(_loseViewPrefab).UnderTransform(_uiRoot).AsSingle();
            Container.BindInterfacesAndSelfTo<LosePresenter>().AsSingle();
        }
    }
}

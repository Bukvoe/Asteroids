using _Asteroids.CodeBase.Components;
using _Asteroids.CodeBase.Entities.Starship;
using _Asteroids.CodeBase.Factories.Payloads;
using _Asteroids.CodeBase.Services;
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

        public override void InstallBindings()
        {
            Container.Bind<Starship>().FromInstance(_starship).AsSingle();

            Container.BindFactory<BulletWeaponSpawnPayload, BulletWeapon, BulletWeapon.Factory>().FromComponentInNewPrefab(_bulletWeaponPrefab);
            Container.BindFactory<BulletSpawnPayload, Bullet, Bullet.Factory>().FromComponentInNewPrefab(_bulletPrefab);

            Container.Bind<SceneObjectService>().AsSingle().WithArguments(_camera);
            Container.Bind<RandomService>().AsSingle();
            Container.Bind<GameMapService>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();
        }
    }
}

using System;
using _Asteroids.CodeBase.Factories;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Gameplay.Weapons;

namespace _Asteroids.CodeBase.Services
{
    public class StarshipService : IDisposable
    {
        public event Action StarshipSpawned;
        public event Action StarshipDestroyed;
        public event Action<IWeapon> WeaponFired;

        private readonly StarshipFactory _starshipFactory;

        public Starship Starship { get; private set; }

        public StarshipService(StarshipFactory starshipFactory)
        {
            _starshipFactory = starshipFactory;
        }

        public void CreateStarship()
        {
            Starship = _starshipFactory.Create();

            Starship.OnDestroyed += OnStarshipDestroyed;
            Starship.Weapon.WeaponFired += OnStarshipWeaponFired;

            StarshipSpawned?.Invoke();
        }

        private void OnStarshipDestroyed()
        {
            Starship.OnDestroyed -= OnStarshipDestroyed;
            Starship.Weapon.WeaponFired -= OnStarshipWeaponFired;

            StarshipDestroyed?.Invoke();
        }

        private void OnStarshipWeaponFired(IWeapon weapon)
        {
            WeaponFired?.Invoke(weapon);
        }

        public void Dispose()
        {
            if (Starship == null)
            {
                return;
            }

            Starship.OnDestroyed -= OnStarshipDestroyed;
            Starship.Weapon.WeaponFired -= OnStarshipWeaponFired;
        }
    }
}

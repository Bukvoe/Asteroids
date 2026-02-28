using Sirenix.OdinInspector;
using UnityEngine;

namespace _Asteroids.CodeBase.Entities.Starship
{
    public class Starship : MonoBehaviour
    {
        [field: SerializeField, Required] public StarshipMovement Movement { get; private set; }
        [field: SerializeField, Required] public StarshipWeapon Weapon { get; private set; }
    }
}

// Dependancies :
using UnityEngine;
using ModularArchitecture;

namespace AbilitySystem
{
    public enum DamageTypes
    {
        None, Direct, Magical, Area    
    }
    
    [CreateAssetMenu(menuName = "Modular/Enums/New DamageTypes")]
    public class ExtendableDamageTypes : DataContainer<DamageTypes>, EnumBase {}
}
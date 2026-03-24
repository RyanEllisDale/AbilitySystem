// Dependancies :
using UnityEngine;
using ModularArchitecture;

namespace AbilitySystem
{
    public enum AbilityCategory
    {
        None, Offense,Defense,Support    
    }
    
    [CreateAssetMenu(menuName = "Modular/Enums/New AbilityCategory")]
    public class ExtendableAbilityCategory : DataContainer<AbilityCategory>, EnumBase {}
}
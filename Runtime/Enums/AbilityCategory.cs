// Dependancies :
using UnityEngine;
using ModularArchitecture;

namespace AbilitySystem
{
    public enum AbilityCategory
    {
        None, Offense,Defense,Support    
    }
    
    [CreateAssetMenu(fileName = "AbilityCategory", menuName = "Modular/Enums/Enum : AbilityCategory", order = 1)]
    public class ExtendableAbilityCategory : ExtendableEnum<AbilityCategory> 
    {

    }
}
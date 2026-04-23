// Dependancies :
using UnityEngine;
using ModularArchitecture;
using System;

namespace AbilitySystem
{
    public enum DamageType
    {
        None, Direct, Magical, Area    
    }

    [CreateAssetMenu(fileName = "DamageTypes", menuName = "Modular/Enums/Enum : DamageTypes", order = 1)]
    public class ExtendableDamageType : ExtendableEnum<DamageType> 
    {
    }

}
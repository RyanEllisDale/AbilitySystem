// Dependancies :
using ModularArchitecture.Enums;

namespace AbilitySystem.Ability
{
    /// <summary>
    /// Types of damage used by abilities and status effects. <br/><br/>
    /// Contains : <br/>
    /// <b>- None :</b> No damage type assigned, <br/>
    /// <b>- Direct :</b> Physical or immediate damage applied directly to a target, <br/>
    /// <b>- Magical :</b> Damage derived from magical or supernatural sources, <br/>
    /// <b>- Area :</b> Damage applied to multiple targets within an area. <br/>
    /// </summary>
    public enum DamageType
    {
        None,
        Direct,
        Magical,
        Area
    }


    /// <summary>
    /// Extendable enum asset for <b>DamageType</b>. Allows designers to define modular,
    /// data‑driven damage types that can be referenced throughout the Ability System. <br/><br/>
    /// Contains : <br/>
    /// <b>- value :</b> The selected DamageType value stored in this asset. <br/>
    /// </summary>
    /// [CreateAssetMenu(fileName = "DamageTypes", menuName = "Modular/Enums/Enum : DamageTypes", order = 1)]
    public class ExtendableDamageType : ExtendableEnum<DamageType> { }
}
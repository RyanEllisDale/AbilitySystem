// Dependancies :
using ModularArchitecture;

namespace AbilitySystem
{
    /// <summary>
    /// Categories used to classify abilities within the Ability System. <br/><br/>
    /// Contains : <br/>
    /// <b>- None :</b> No assigned category, <br/>
    /// <b>- Offense :</b> Abilities focused on dealing damage or applying harmful effects, <br/>
    /// <b>- Defense :</b> Abilities focused on protection, mitigation, or resistance, <br/>
    /// <b>- Support :</b> Abilities that assist allies through healing, buffs, or utility. <br/>
    /// </summary>
    public enum AbilityCategory
    {
        None,
        Offense,
        Defense,
        Support
    }


    /// <summary>
    /// Extendable enum asset for <b>AbilityCategory</b>. Allows designers to create modular,
    /// data‑driven enum entries that can be referenced throughout the Ability System. <br/><br/>
    /// Contains : <br/>
    /// <b>- value :</b> The selected AbilityCategory value stored in this asset. <br/>
    /// </summary>
    /// [CreateAssetMenu(fileName = "AbilityCategory", menuName = "Modular/Enums/Enum : AbilityCategory", order = 1)]
    public class ExtendableAbilityCategory : ExtendableEnum<AbilityCategory> { }
}
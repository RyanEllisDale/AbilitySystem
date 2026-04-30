// Dependancies :
using UnityEngine;
using AbilitySystem.Status;
using AbilitySystem;

namespace AbilitySystem.Ability
{
    /// <summary>
    /// Effect that applies a <b>StatusData</b> to the target unit. <br/><br/>
    /// Contains : <br/>
    /// <b>- _statusData :</b> The status effect to apply when activated. <br/>
    /// </summary>
    [CreateAssetMenu(fileName="ApplyStatus", menuName="Abilities/Effects/Effect : ApplyStatus", order = 1)]
    public class ApplyStatusEffect : Effect
    {
        // Data Members:
        [SerializeField] private StatusData _statusData;

        /// <summary>
        /// Applies the assigned status effect to the target unit.
        /// </summary>
        public override void Activate(IUnit parent, IUnit target)
        {
            AbilitySystemAPI.ApplyStatus(target, _statusData);
        }
    }

}
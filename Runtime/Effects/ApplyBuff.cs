// Dependancies :
using UnityEngine;
using AbilitySystem.Buff;

namespace AbilitySystem.Ability
{
    /// <summary>
    /// Effect that applies a <b>BuffData</b> to the target unit. <br/><br/>
    /// Contains : <br/>
    /// <b>- _buffData :</b> The buff to apply when this effect activates. <br/>
    /// </summary>
    [CreateAssetMenu(fileName = "ApplyBuff", menuName = "Abilities/Effects/Effect : ApplyBuff", order = 1)]
    public class ApplyBuff : Effect
    {
        // Data Methods : 
        [SerializeField] private BuffData _buffData;

        /// <summary>
        /// Applies the assigned buff to the target unit.
        /// </summary>
        public override void Activate(IUnit parent, IUnit target)
        {
            AbilitySystemAPI.ApplyBuff(target, _buffData);
        }
    }
}
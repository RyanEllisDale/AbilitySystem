using UnityEngine;
using ModularArchitecture;

namespace AbilitySystem
{
    /// <summary>
    /// Effect that restores health to the target unit. <br/><br/>
    /// Contains : <br/>
    /// <b>- _healAmount :</b> The amount of health restored when activated. <br/>
    /// </summary>
    [CreateAssetMenu(fileName="Heal", menuName="Abilities/Effects/Effect : Heal", order = 1)]
    public class HealEffect : Effect
    {
        // Data Members:
        [SerializeField] private DataReference<float> _healAmount;

        /// <summary>
        /// Restores health to the target unit.
        /// </summary>
        public override void Activate(IUnit parent, IUnit target)
        {
            target.UpdateHealth(_healAmount);
        }
    }
}
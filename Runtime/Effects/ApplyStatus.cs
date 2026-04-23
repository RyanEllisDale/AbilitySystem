// Dependancies :
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="ApplyStatus", menuName="Abilities/Effects/Effect : ApplyStatus", order = 1)]
    public class ApplyStatusEffect : Effect
    {
        // Data Variables:
        [SerializeField] private StatusData statusData;

        // Activation :
        public override void Activate(IUnit parent, IUnit target)
        {
            AbilitySystemAPI.ApplyStatus(target, statusData);
        }
    }

}
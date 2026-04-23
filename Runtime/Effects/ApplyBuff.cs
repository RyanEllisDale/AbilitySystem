// Dependancies :
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="ApplyBuff", menuName="Abilities/Effects/Effect : ApplyBuff", order = 1)]
    public class ApplyBuff : Effect
    {
        // Data Variables:
        [SerializeField] private BuffData buffData;
        
        // Activation :
        public override void Activate(IUnit parent, IUnit target)
        {
            AbilitySystemAPI.ApplyBuff(target, buffData);
        }
    }
}
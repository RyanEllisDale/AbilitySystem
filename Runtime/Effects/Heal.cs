using UnityEngine;
using ModularArchitecture;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="Heal", menuName="Abilities/Effects/Effect : Heal", order = 1)]
    public class HealEffect : Effect
    {
        // Data Variables:
        [SerializeField] private DataReference<float> healAmount; 
        
        // Activation :
        public override void Activate(IUnit parent, IUnit target)
        {
            target.UpdateHealth(healAmount);
        }
    }
}
using UnityEngine;
using ModularArchitecture;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="Heal", menuName="Abilities/Effects/Heal")]
    public class Heal : Effect
    {
        // Data Variables:
        [SerializeField] private DataReference<float> healAmount; 
        
        // Activation :
        public override void Activate(GameObject parent, IUnit target)
        {
            target.UpdateHealth(healAmount);
        }
    }
}
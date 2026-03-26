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
        public override void Activate(GameObject parent, GameObject target)
        {
            target.GetComponent<HealthInterface>().UpdateHealth(healAmount);
        }
    }
}
// Dependancies :
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="Print", menuName="Abilities/Effects/Effect : Print", order = 1)]
    public class PrintEffect : Effect
    {
        // Data Variables:
        [SerializeField] private string message;
        
        // Activation :
        public override void Activate(IUnit parent, IUnit target)
        {
            Debug.Log(message);
        }
    }
}
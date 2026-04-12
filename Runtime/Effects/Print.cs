// Dependancies :
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="Print", menuName="Abilities/Effects/Print")]
    public class Print : Effect
    {
        // Data Variables:
        [SerializeField] private string message;
        
        // Activation :
        public override void Activate(GameObject parent, IUnit target)
        {
            Debug.Log(message);
        }
    }
}
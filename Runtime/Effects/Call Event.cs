// Dependancies :
using ModularArchitecture;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="CallEvent", menuName="Abilities/Effects/Effect : CallEvent", order = 1)]
    public class CallEvent : Effect
    {
        // Data Variables:
        [SerializeField] private GameEvent gameEvent;
        
        // Activation :
        public override void Activate(IUnit parent, IUnit target)
        {
            gameEvent?.Raise();
        }
    }
}
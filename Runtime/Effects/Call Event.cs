// Dependancies :
using ModularArchitecture;
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// Effect that raises a <b>GameEvent</b> when activated. <br/><br/>
    /// Contains : <br/>
    /// <b>- _gameEvent :</b> The event to raise on activation. <br/>
    /// </summary>
    [CreateAssetMenu(fileName="CallEvent", menuName="Abilities/Effects/Effect : CallEvent", order = 1)]
    public class CallEvent : Effect
    {
        // Data Members:
        [SerializeField] private GameEvent _gameEvent;

        /// <summary>
        /// Raises the assigned GameEvent.
        /// </summary>
        public override void Activate(IUnit parent, IUnit target)
        {
            _gameEvent?.Raise();
        }
    }
}
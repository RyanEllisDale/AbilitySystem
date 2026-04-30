// Dependancies :
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// Effect that prints a message to the console when activated. <br/><br/>
    /// Contains : <br/>
    /// <b>- _message :</b> The message printed on activation. <br/>
    /// </summary>
    [CreateAssetMenu(fileName="Print", menuName="Abilities/Effects/Effect : Print", order = 1)]
    public class PrintEffect : Effect
    {
        // Data Members:
        [SerializeField] private string _message;

        /// <summary>
        /// Logs the assigned message to the console.
        /// </summary>
        public override void Activate(IUnit parent, IUnit target)
        {
            Debug.Log(_message);
        }
    }
}
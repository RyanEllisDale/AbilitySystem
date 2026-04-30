// Dependancies : 
using UnityEngine;

namespace AbilitySystem.Ability
{
    /// <summary>
    /// Effect that pushes the target unit by a specified grid offset. <br/><br/>
    /// Contains : <br/>
    /// <b>- _pushForce :</b> The grid direction and distance to push the target. <br/>
    /// </summary>
    [CreateAssetMenu(fileName="Push", menuName="Abilities/Effects/Effect : Push", order = 1)]
    public class PushEffect : Effect
    {
        // Data Members:
        [SerializeField] private Vector2Int _pushForce;

        /// <summary>
        /// Attempts to move the target unit by the configured push force.
        /// </summary>
        public override void Activate(IUnit parent, IUnit target)
        {
            target.MoveUnit(_pushForce);
        }
    }
}

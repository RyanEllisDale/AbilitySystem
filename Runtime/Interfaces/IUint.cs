using System.Collections.Generic;
using UnityEngine;
using AbilitySystem.Buff;
using AbilitySystem.Status;
using AbilitySystem.Ability;

namespace AbilitySystem
{
    /// <summary>
    /// Composite interface representing a fully functional gameplay unit. <br/>
    /// Combines grid movement, health management, status effects, and buff functionality. <br/><br/>
    ///
    /// Implementers must provide : <br/>
    /// <b>- abilityInstances :</b> A list of runtime ability instances available to this unit. <br/><br/>
    ///
    /// <b>Note for Developers :</b>  
    /// This is the central interface to update when expanding unit‑level functionality.  
    /// Any new system that all units must support should be added here. <br/><br/>
    /// </summary>
    public interface IUnit : IStatusContainer, IBuffContainer
    {
        /// <summary>
        /// The list of runtime ability instances available to this unit.
        /// </summary>
        public List<AbilityInstance> abilityInstances { get; }

        /// <summary>
        /// Updates the object's health by the given amount. <br/>
        /// Positive values heal, negative values deal damage. <br/><br/>
        /// </summary>
        /// <param name="update">The amount of health to add or subtract.</param>
        /// <returns>The new health value after the update.</returns>
        public float UpdateHealth(float update);

        /// <summary>
        /// Attempts to move the unit by the given grid offset. <br/><br/>
        /// </summary>
        /// <param name="moveSpaces">The number of grid spaces to move in X and Y directions.</param>
        /// <returns>True if the movement was successful, otherwise false.</returns>
        public bool MoveUnit(Vector2Int moveSpaces);
    }
}

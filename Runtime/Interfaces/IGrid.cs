// Dependancies : 
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// Interface for any object capable of moving across a grid‑based environment. <br/><br/>
    /// Implementers must provide : <br/>
    /// <b>- MoveUnit :</b> Attempts to move the unit by a given number of grid spaces. <br/><br/>
    /// </summary>
    public interface IGrid
    {
        /// <summary>
        /// Attempts to move the unit by the given grid offset. <br/><br/>
        /// </summary>
        /// <param name="moveSpaces">The number of grid spaces to move in X and Y directions.</param>
        /// <returns>True if the movement was successful, otherwise false.</returns>
        public bool MoveUnit(Vector2Int moveSpaces);
    }
}
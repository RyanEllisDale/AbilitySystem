// Dependancies
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// Base class for all ability effects. Effects define a single action performed when an ability activates. <br/><br/>
    /// Contains : <br/>
    /// <b>- Activate :</b> Executes the effect using the parent and target units. <br/>
    /// </summary>
    public abstract class Effect : ScriptableObject
    {
        /// <summary>
        /// Executes the effect using the parent (caster) and target units.
        /// </summary>
        public abstract void Activate(IUnit parent, IUnit target);
    }
}
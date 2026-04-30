using System.Collections.Generic;

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
    public interface IUnit : IGrid, IHealth, IStatusContainer, IBuffContainer
    {
        /// <summary>
        /// The list of runtime ability instances available to this unit.
        /// </summary>
        public List<AbilityInstance> abilityInstances { get; }
    }
}

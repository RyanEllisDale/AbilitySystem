// Dependancies : 
using UnityEngine;

namespace AbilitySystem.Ability
{
    /// <summary>
    /// Runtime instance of an <b>AbilityData</b> asset. Stores temporary state such as cooldown
    /// and provides a reference to the permanent ability configuration. <br/><br/>
    ///
    /// Contains : <br/>
    /// <b>- ability :</b> The permanent AbilityData asset this instance is based on, <br/>
    /// <b>- currentCooldown :</b> The number of turns remaining before the ability can be used again. <br/>
    /// </summary>
    [System.Serializable]
    public class AbilityInstance
    {
        // Member Data :
        public AbilityData ability;
        public int currentCooldown = 0;
    }
}

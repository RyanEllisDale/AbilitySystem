// Dependancies : 
using UnityEngine;
using ModularArchitecture.Data;

namespace AbilitySystem.Supplies
{
    /// <summary>
    /// Represents a resource requirement for activating an ability. <br/>
    /// Used by abilities to check whether the user has enough resources and to deduct them on activation. <br/><br/>
    /// Contains : <br/>
    /// <b>- id :</b> A unique identifier for this supply requirement, <br/>
    /// <b>- description :</b> A multiline description used for UI or debugging, <br/>
    /// <b>- _resource :</b> A reference to the current amount of the required resource, <br/>
    /// <b>- _cost :</b> A reference to the amount of resource consumed when used. <br/><br/>
    /// </summary>
    [System.Serializable]
    public class Supply
    {
        // Data Member : 
        public string id;
        [Multiline] public string description;
        [SerializeField] private DataReference<float> _resource;
        [SerializeField] private DataReference<float> _cost;

        #region Data Methods
        /// <summary>
        /// Evaluates whether the required resource amount is available. <br/><br/>
        /// </summary>
        /// <returns>True if the resource value is greater than or equal to the cost.</returns>
        public bool Evaluate() { return _resource.value >= _cost.value; }

        /// <summary>
        /// Deducts the cost from the resource value. <br/>
        /// Should only be called after <b>Evaluate</b> returns true. <br/><br/>
        /// </summary>
        public void Use() { _resource.value = _resource.value - _cost.value; }

        #endregion  
    }
}

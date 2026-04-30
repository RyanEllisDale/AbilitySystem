// Dependancies : 
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.Status
{
    /// <summary>
    /// Base data asset for all status effects. Stores permanent configuration for a status,
    /// including its identifier, description, and duration.<br/><br/>
    /// Contains: <br/>
    /// <b>- id :</b> A unique string identifier for referencing this status, <br/>
    /// <b>- description :</b> A multiline description used for UI, debugging, or documentation, <br/>
    /// <b>- duration :</b> How many turns the status remains active once applied, <br/>
    /// <b>- components :</b> A list of StatusData objects that represent sub‑effects used in mixing logic. <br/><br/>
    /// </summary>
    public abstract class StatusData : ScriptableObject
    {
        // Data Members :
        public string id;
        [Multiline] public string description;
        public int duration = 1;

        /// <summary>
        /// Component effects used for mixing and combination logic.
        /// Flatened list of StatusData that is used to check if this status is the restulf of a combination.
        /// </summary>
        [SerializeField] public List<StatusData> components = new List<StatusData>();

        /// <summary>
        /// Creates a runtime instance of this status effect.
        /// </summary>
        /// <returns>A new StatusInstance based on this data.</returns>
        public abstract StatusInstance CreateInstance();
    }
}
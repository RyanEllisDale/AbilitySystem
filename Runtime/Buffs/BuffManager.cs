// Dependancies : 
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// The Buff Manager is a composite runtime container that manages all active <b>BuffInstance</b> objects
    /// applied to a target implementing <b>IBuffContainer</b>. <br/> 
    /// This class is a the Buff Functionality mirror to <b>Status Manager.</b> <br/><br/>
    /// Responsible for: <br/>
    /// <b>- Adding Buffs :</b> Creates new runtime instances and applies their stat modifiers, <br/>
    /// <b>- Removing Buffs :</b> Expires instances and reverses their stat modifiers, <br/>
    /// <b>- Tracking Active Buffs :</b> Maintains a list of all currently applied BuffInstances. <br/>
    /// </summary>
    [System.Serializable]
    public class BuffManager
    {
        // Data Members : 
        private IBuffContainer _buffContainer; // The owner implementing IBuff; receives stat modifications.
        [SerializeField] private List<BuffInstance> _buffInstances = new List<BuffInstance>(); 
        public List<BuffInstance> buffInstances => _buffInstances; // public Read-Only for Referencing.

        // Construction : 
        /// <summary>
        /// Constructor for BuffManager. <br/>
        /// Requires a reference to the <b>IBuffContainer</b> container that owns this manager.
        /// </summary>
        /// <param name="buffContainer">The stored target that will receive buff stat modifications.</param>
        public BuffManager(IBuffContainer buffContainer)
        {
            this._buffContainer = buffContainer; 
        }

        #region Data Methods

        /// <summary>
        /// Creates a new <b>BuffInstance</b> from the given <b>BuffData</b>, applies its modifiers to the owner,
        /// and stores the instance in the active list. <br/>
        /// </summary>
        /// <param name="buffData">The <b>BuffData</b> asset used to construct the runtime instance.</param>
        /// <returns> The newly created BuffInstance. </returns>
        public BuffInstance AddBuff(BuffData buffData)
        {
            BuffInstance buffInstance = new BuffInstance(buffData);

            _buffInstances.Add(buffInstance);
            buffInstance.OnApply(_buffContainer);
            
            return buffInstance;
        }

        /// <summary>
        /// Removes a specific <b>BuffInstance</b> from the active list and expires it, reversing its stat effects. <br/>
        /// </summary>
        /// <param name="buffInstance">The runtime instance to remove.</param>
        /// <returns> The removed Instance, If the instance is not active, returns null. </returns>
        public BuffInstance RemoveBuff(BuffInstance buffInstance)
        {
            if (_buffInstances.Contains(buffInstance) == false)
            {
                return null;
            }

            _buffInstances.Remove(buffInstance);
            buffInstance.OnExpire(_buffContainer);

            return buffInstance;
        }

        /// <summary>
        /// Removes the first active <b>BuffInstance</b> that matches the given <b>BuffData</b>. <br/>
        /// </summary>
        /// <param name="buffData">The permanent data asset used to identify the instance.</param>
        /// <returns> The removed Instance, If the instance is not active, returns null. </returns>
        public BuffInstance RemoveBuff(BuffData buffData)
        {
            // Find the active instance that matches this BuffData
            BuffInstance buffInstance = _buffInstances.Find(b => b.data == buffData);

            if (buffInstances == null)
            {
                return null;
            }
            
            _buffInstances.Remove(buffInstance);
            buffInstance.OnExpire(_buffContainer);

            return buffInstance;
        }

        #endregion Data Methods
    }
}

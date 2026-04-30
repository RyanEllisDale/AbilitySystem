// Dependancies : 
using UnityEngine;
using System;

namespace AbilitySystem.Buff
{
    /// <summary>
    /// A runtime instance for buff data to be applied to buff managers, modifies the stats of <b>IBuffContainer</b> containers based on the data's modifiers. <br/>
    /// Contains the inpermenant runtime data for active buffs / de-buffs that are applied to a target / buff-manager. <br/> <br/>
    /// Contains : <br/>
    /// <b>- Data :</b> The permenant data asset file for the instance to read from, <br/>
    /// <b>- CurrentDuration :</b> How many turns the Instance has left before being removed. <br/> <br/>
    /// Responsible for: <br/>
    /// <b>- Triggering Modification :</b> When a BuffInstance is applied it will trigger all the stat modifications in it's data, <br/>
    /// <b>- Expiring Modification :</b> When a BuffInstance's duration ends, it will trigger the inverse of stat modifications in it's data, <br/>
    /// <b>- Storing Current Duration :</b> As each turn passes the API will reduce it's duration and remove the Instance when it hits 0. <br/>
    /// </summary>
    [System.Serializable]   
    public class BuffInstance : IEquatable<BuffInstance> // IEquatable for list enumeration ( Ignores currentDuration ) 
    {
        // Data Members : 
        [SerializeField] private BuffData _data; // Permenant data for the instance.
        public BuffData data => _data; // Readonly data for referencing.
        public int currentDuration = 1;

        // Construction : 
        /// <summary>
        /// Stand-alone constructor for BuffInstance, should only ever be created based off given data. 
        /// </summary>
        /// <param name="aData">The required data for the instance to be based off.</param>
        public BuffInstance(BuffData data)
        {
            this._data = data;
            this.currentDuration = _data.duration;
        }

        #region Data Methods

        /// <summary>
        /// Calls the <b>ModifyStat</b> function for each <b>modifier</b> in <u>this</u> instances' <b>buffdata</b>. <br/>
        /// Should be called by a <b>Buff Manager</b> whenever an Instance is applied to a <b>IBuffContainer</b> reference.
        /// </summary>
        /// <param name="target">Modifiers will be applied to target. <br/>Target should be the owner of the Buff Container creating the Instance.</param>
        public void OnApply(IBuffContainer target)
        {
            foreach (StatModifier currentModifier in _data.modifiers)
            { 
                target.ModifyStat(currentModifier.targetStatID, currentModifier.modifierValue, currentModifier.modifierType);
            }
        }

        /// <summary>
        /// Determines the opposite of the application of data modifiers and modifies the target with those values; <br/>
        /// Should be called by a <b>Buff Manager</b> whenever an Instance is removed from a <b>IBuffContainer</b> reference.
        /// </summary>
        /// <param name="target">Reverse of modifiers will be applied to target. <br/>Target should be the owner of the Buff Container creating the Instance.</param>
        public void OnExpire(IBuffContainer target)
        {
            // Determines a removal value and passes it on for every modifier : 

            foreach(StatModifier currentModifier in _data.modifiers)
            {
                float removalValue = 1.0f;
                switch (currentModifier.modifierType)
                {
                    case StatModifierType.Add:
                        removalValue = -currentModifier.modifierValue;
                        break;

                    case StatModifierType.Multiply:
                        if (currentModifier.modifierValue != 0)
                        {
                            removalValue = 1.0f / currentModifier.modifierValue;
                        }
                        else
                        {
                            #if UNITY_EDITOR
                            Debug.LogError("Current Operation: BuffInstance : OnExpire : Removing Multiplying Buff Modifier in IBuffContainer Target,\nModifier Value for modifier: " + currentModifier.targetStatID + " in :" + _data.id + " is 0\nYou cannot have a value of 0 for this operation.");
                            #endif
                        }

                        break;
                }

                target.ModifyStat(currentModifier.targetStatID, removalValue, currentModifier.modifierType);
            }
        }

        #endregion Data Methods

        // Ignores Duration : 
        #region IEquatable<>

        public bool Equals(BuffInstance other)
        {
            return data == other.data;
        }

        public override bool Equals(object obj)
        {
            if (obj is BuffInstance other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return HashCode.Combine(data, currentDuration);
            }
        }

        #endregion IEquatable<>
    }
}
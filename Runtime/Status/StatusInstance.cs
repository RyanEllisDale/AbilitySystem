// Status Instance : 
using UnityEngine;
using System;

namespace AbilitySystem.Status
{
    /// <summary>
    /// Runtime instance of a StatusData asset. Stores temporary state such as remaining duration
    /// and provides activation hooks for turn‑based status behavior. <br/><br/>
    /// Contains: <br/>
    /// <b>- data :</b> The permanent StatusData asset this instance is based on, <br/>
    /// <b>- currentDuration :</b> How many turns remain before the status expires. <br/><br/>
    /// Responsible for: <br/>
    /// <b>- Turn Events :</b> Provides virtual callbacks for turn start and end, <br/>
    /// <b>- Application :</b> Executes logic when applied to an IStatus target, <br/>
    /// <b>- Removal :</b> Executes cleanup logic when removed from an IStatus target. <br/>
    /// </summary>
    [System.Serializable]
    public class StatusInstance : IEquatable<StatusInstance>
    {
        // Data Members :
        [SerializeField] public StatusData data;
        [SerializeField] public int currentDuration = 0;

        // Construction :
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public StatusInstance() { }

        /// <summary>
        /// Creates a new StatusInstance based on the given StatusData.
        /// </summary>
        /// <param name="aData">The permanent data asset this instance is based on.</param>
        public StatusInstance(StatusData aData)
        {
            data = aData;
        }

        #region Data Methods

        // Activation Hooks :
        /// <summary>
        /// Called at the start of a turn. Override to implement custom behavior.
        /// </summary>
        /// <param name="target">The IStatus target affected by this status.</param>
        /// <returns>True if the status performed an action, otherwise false.</returns>
        public virtual bool OnTurnStart(IStatusContainer target) { return false; }

        /// <summary>
        /// Called at the end of a turn. Override to implement custom behavior.
        /// </summary>
        /// <param name="target">The IStatus target affected by this status.</param>
        /// <returns>True if the status performed an action, otherwise false.</returns>
        public virtual bool OnTurnEnd(IStatusContainer target) { return false; }

        /// <summary>
        /// Called when the status is first applied to a target.
        /// </summary>
        /// <param name="target">The IStatus target receiving the status.</param>
        public virtual void OnApply(IStatusContainer target) { }

        /// <summary>
        /// Called when the status is removed from a target.
        /// </summary>
        /// <param name="target">The IStatus target losing the status.</param>
        public virtual void OnRemove(IStatusContainer target) { }

        #endregion

        #region Interface

        // IEquatable :
        /// <summary>
        /// Determines equality based on underlying StatusData (ignores duration).
        /// </summary>
        public bool Equals(StatusInstance other)
        {
            return data == other.data;
        }

        public override bool Equals(object obj)
        {
            if (obj is StatusInstance other)
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

        #endregion
    }
}

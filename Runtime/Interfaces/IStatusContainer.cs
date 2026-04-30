namespace AbilitySystem.Status
{
    /// <summary>
    /// Primary interface for any object capable of receiving and interacting with status effects. <br/>
    /// This is the core contract used by the Status System. <br/><br/>
    ///
    /// <b>Note for Developers :</b>  
    /// This is the interface you should update when adding new status‑related functionality. <br/>
    /// Any new behavior that all status‑receiving objects must support should be added here. <br/><br/>
    /// </summary>
    public interface IStatusContainer
    {
        /// <summary>
        /// The StatusManager responsible for handling all active StatusInstances applied to this object.
        /// </summary>
        public StatusManager StatusManager { get; }

        /// <summary>
        /// Applies status‑based damage or healing to the object. <br/><br/>
        /// </summary>
        /// <param name="damage">The amount of damage to apply. Negative values represent healing.</param>
        public void ApplyStatusDamage(int damage);

        // New Functionality : 
    }
}

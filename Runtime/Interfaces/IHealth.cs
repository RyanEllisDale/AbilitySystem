namespace AbilitySystem
{
    /// <summary>
    /// Interface for any object capable of receiving health updates such as damage or healing. <br/><br/>
    /// Implementers must provide : <br/>
    /// <b>- UpdateHealth :</b> Applies a positive or negative change to the object's health. <br/><br/>
    /// </summary>
    public interface IHealth
    {
        /// <summary>
        /// Updates the object's health by the given amount. <br/>
        /// Positive values heal, negative values deal damage. <br/><br/>
        /// </summary>
        /// <param name="update">The amount of health to add or subtract.</param>
        /// <returns>The new health value after the update.</returns>
        public float UpdateHealth(float update);
    }
}

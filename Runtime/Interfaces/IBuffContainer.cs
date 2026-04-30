namespace AbilitySystem.Buff
{
    /// <summary>
    /// Interface for any object capable of receiving and managing buffs. <br/><br/>
    /// Implementers must provide: <br/>
    /// <b>- buffManager :</b> A BuffManager instance responsible for handling BuffInstances, <br/>
    /// <b>- GetStat :</b> Retrieves a stat value (float) by its string identifier, <br/>
    /// <b>- ModifyStat :</b> Applies additive or multiplicative changes to a stat. <br/><br/>
    /// </summary>
    public interface IBuffContainer
    {
        /// <summary>
        /// The BuffManager responsible for handling all BuffInstances applied to this object.
        /// </summary>
        public BuffManager buffManager { get; }

        /// <summary>
        /// Retrieves the value of a stat by its string identifier. <br/><br/>
        /// </summary>
        /// <param name="statID">The string identifier used to locate the stat.</param>
        /// <returns>The stat value, or null if the stat does not exist.</returns>
        float? GetStat(string statID);

        /// <summary>
        /// Modifies a stat using the given amount and modifier type. <br/><br/>
        /// </summary>
        /// <param name="statID">The string identifier used to locate the stat to be modified.</param>
        /// <param name="modifyAmount">The value applied to the stat based on the modifier type.</param>
        /// <param name="modifyType">Determines whether the modification is additive or multiplicative.</param>
        /// <returns>The new stat value, or null if the stat does not exist.</returns>
        float? ModifyStat(string statID, float modifyAmount, StatModifierType modifyType);
    }
}

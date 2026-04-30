// Dependancies :
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// StatusData representing a regeneration effect. <br/><br/>
    /// Contains : <br/>
    /// <b>- healthPerTurn :</b> The amount of health restored at the start of each turn. <br/>
    /// </summary>
    [CreateAssetMenu(fileName="Regeneration", menuName="Abilities/Status/Status : Regeneration", order = 2)]
    public class RegenerationStatusData : StatusData
    {
        // Data Members :
        public int healthPerTurn = 1;

        /// <summary>
        /// Creates a runtime instance for this regeneration effect.
        /// </summary>
        public override StatusInstance CreateInstance()
        {
            return new RegenerationStatusInstance(this);
        }
    }

    /// <summary>
    /// Runtime instance for <b>RegenerationStatusData</b>. Restores health to the target
    /// at the start of each turn. <br/><br/>
    /// Contains : <br/>
    /// <b>- _regenData :</b> Cached reference to the regeneration data asset. <br/>
    /// </summary>
    public class RegenerationStatusInstance : StatusInstance
    {
        // Data Members :
        [SerializeField] private RegenerationStatusData _regenData;

        // Construction :
        /// <summary>
        /// Constructs a regeneration instance based on the given data.
        /// </summary>
        public RegenerationStatusInstance(RegenerationStatusData data) : base(data)
        {
            _regenData = data;
        }

        #region Data Methods

        /// <summary>
        /// Restores health to the target at the start of each turn.
        /// </summary>
        public override bool OnTurnStart(IStatusContainer target)
        {
            target.ApplyStatusDamage(-_regenData.healthPerTurn);
            return true;
        }

        #endregion Data Methods
    }
}
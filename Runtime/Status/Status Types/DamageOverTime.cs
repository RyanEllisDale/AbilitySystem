// Dependancies : 
using UnityEngine;

namespace AbilitySystem.Status
{
    /// <summary>
    /// StatusData representing a damage‑over‑time effect. <br/><br/>
    /// Contains : <br/>
    /// <b>- damagePerTurn :</b> The amount of damage applied at the end of each turn. <br/>
    [CreateAssetMenu(fileName = "DamageOverTime", menuName = "Abilities/Status/Status : DamageOverTime", order = 2)]
    public class DamageOverTimeStatusData : StatusData
    {
        // Data Members :
        public int damagePerTurn = 5;

        /// <summary>
        /// Creates a runtime instance for this damage‑over‑time effect.
        /// </summary>
        public override StatusInstance CreateInstance()
        {
            return new DamageOverTimeStatusInstance(this);
        }
    }

    /// <summary>
    /// Runtime instance for <b>DamageOverTimeStatusData</b>. Applies damage to the target
    /// at the end of each turn. <br/><br/>
    /// Contains : <br/>
    /// <b>- _dotData :</b> Cached reference to the DOT data asset. <br/>
    /// </summary>
    public class DamageOverTimeStatusInstance : StatusInstance
    {
        // Data Members :
        [SerializeField] private DamageOverTimeStatusData _dotData;

        // Construction : 
        /// <summary>
        /// Constructs a DOT instance based on the given data.
        /// </summary>
        public DamageOverTimeStatusInstance(DamageOverTimeStatusData data) : base(data)
        {
            _dotData = data;
        }

        #region Data Methods

        /// <summary>
        /// Applies damage to the target at the end of each turn.
        /// </summary>
        public override bool OnTurnEnd(IStatusContainer target)
        {
            target.ApplyStatusDamage(_dotData.damagePerTurn);
            return true;
        }

        #endregion Data Methods
    }
}

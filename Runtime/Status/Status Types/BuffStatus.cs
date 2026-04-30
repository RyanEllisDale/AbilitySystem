// Dependancies :
using UnityEngine;
using AbilitySystem.Buff;

namespace AbilitySystem.Status
{
    /// <summary>
    /// StatusData wrapper for applying a <b>BuffData</b> asset as a status effect. <br/><br/>
    /// Contains : <br/>
    /// <b>- data :</b> The BuffData asset to apply when this status activates. <br/>
    /// </summary>
    [CreateAssetMenu(fileName = "Buff", menuName = "Abilities/Status/Status : Buff", order = 2)]
    public class BuffStatusData : StatusData
    {
        // Data Members :
        public BuffData data;

        /// <summary>
        /// Creates a runtime instance of this buff status.
        /// </summary>
        /// <returns>A new BuffStatusInstance.</returns>
        public override StatusInstance CreateInstance()
        {
            return new BuffStatusInstance(this);
        }
    }

    /// <summary>
    /// Runtime instance for <b>BuffStatusData</b>. Applies and removes a BuffData
    /// from any target implementing <b>IBuffContainer</b>. <br/><br/>
    /// Contains : <br/>
    /// <b>- _buffStatusData :</b> Cached reference to the BuffStatusData asset. <br/>
    /// </summary>
    public class BuffStatusInstance : StatusInstance
    {
        // Data Members :
        [SerializeField] private BuffStatusData _buffStatusData;

        // Construction :
        /// <summary>
        /// Constructs a BuffStatusInstance based on the given BuffStatusData.
        /// </summary>
        public BuffStatusInstance(BuffStatusData data) : base(data)
        {
            _buffStatusData = data;
        }

        #region Data Methods

        /// <summary>
        /// Applies the underlying BuffData to the target if it implements IBuffContainer.
        /// </summary>
        /// <param name="target">The IStatus target receiving the buff.</param>
        public override void OnApply(IStatusContainer target)
        {
            base.OnApply(target);

            if (target is IBuffContainer statsContainer)
            {
                AbilitySystemAPI.ApplyBuff(statsContainer, _buffStatusData.data);
            }
        }

        /// <summary>
        /// Removes the underlying BuffData from the target if it implements IBuffContainer.
        /// </summary>
        /// <param name="target">The IStatus target losing the buff.</param>
        public override void OnRemove(IStatusContainer target)
        {
            base.OnRemove(target);

            if (target is IBuffContainer statsContainer)
            {
                AbilitySystemAPI.RemoveBuff(statsContainer, _buffStatusData.data);
            }
        }

        #endregion Data Methods
    }
}
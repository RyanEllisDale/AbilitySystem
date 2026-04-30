#if UNITY_EDITOR
using System;
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// A simple in-editor testing component for validating the Buff System at runtime. <br/>
    /// Allows designers and programmers to manually apply, remove, and inspect buffs without needing a full gameplay loop. <br/><br/>
    /// 
    /// Contains : <br/>
    /// <b>- target :</b> The GameObject containing an <b>IBuffContainer</b> to receive buff effects, <br/>
    /// <b>_buffData :</b> The BuffData asset used for testing, <br/>
    /// <b>_buffManager :</b> A runtime BuffManager instance for this tester, <br/>
    /// <b>_statName / _statValue :</b> A simple stat pair used to simulate stat modification. <br/><br/>
    ///
    /// Responsible for : <br/>
    /// <b>- Creating a BuffManager :</b> Initializes a manager for applying and removing buffs, <br/>
    /// <b>- Relaying Buff Actions :</b> Calls AbilitySystemAPI methods for applying, removing, and ticking buffs, <br/>
    /// <b>- Simulating Stats :</b> Provides a minimal stat system for testing modifier behavior, <br/>
    /// <b>- Editor Testing :</b> Exposes context menu actions for quick editor validation. <br/>
    /// </summary>
    public class BuffTester : MonoBehaviour, IBuffContainer
    {
        // Data Members : 
        [Header("Buff Details :")]
        public GameObject target;
        private IBuffContainer _buffContainer;

        [SerializeField] private BuffData _buffData;
        [SerializeField] private BuffManager _buffManager;
        public BuffManager buffManager => _buffManager; // public Read-Only for Referencing.

        [Header("Stat Details :")]
        [SerializeField] private string _statName = "attack";
        [SerializeField] private float _statValue = 10.0f;

        #region Unity Methods : 

        // Initializes the Buff Container for IBuffContainer Implementation 
        // upon game start for runtime use : 
        private void Start()
        {   
            _buffContainer = target.GetComponent<IBuffContainer>();
            _buffManager = new BuffManager(this);
        }

        // Initializes the Buff Container for IBuffContainer Implementation 
        // upon validation for editor use : 
        private void OnValidate()
        {
            _buffContainer = target.GetComponent<IBuffContainer>();
            _buffManager = new BuffManager(this);
        }

        #endregion Unity Methods : 

        #region Data Methods 

        /// <summary>
        /// Retrieves the value of a stat by its string identifier. <br/>
        /// Only supports a single test stat defined by <b>_statName</b>. <br/><br/>
        /// <returns>
        /// The stat value if the ID matches, otherwise <b>null</b>.
        /// </returns>
        /// </summary>
        public float? GetStat(string statID)
        {
            if (statID.Equals(_statName, StringComparison.OrdinalIgnoreCase) == true)
            {
                return _statValue;
            }

            return null;
        }

        /// <summary>
        /// Modifies the test stat using the provided amount and modifier type. <br/>
        /// Supports <b>Add</b> and <b>Multiply</b> operations. <br/><br/>
        /// <returns>
        /// The updated stat value if the ID matches, otherwise <b>null</b>. <br/>
        /// Returns <b>null</b> and logs an error if the modifier type is unknown.
        /// </returns>
        /// </summary>
        public float? ModifyStat(string statID, float modifyAmount, StatModifierType modifyType)
        {
            if (statID.Equals(_statName, StringComparison.OrdinalIgnoreCase) == true)
            {
                switch (modifyType)
                {
                    case StatModifierType.Add: _statValue = _statValue + modifyAmount; return _statValue;
                    case StatModifierType.Multiply: _statValue = _statValue * modifyAmount; return _statValue;
                    default: Debug.LogError("Buff Tester(IBuff) : ModifyStat : Unknown Stat Modifier Type for " + statID + " of modifier type : " + modifyType); return null;
                }
            }

            Debug.Log("Buff Tester does not have a stat matching : " + statID);
            return null;
        }

        #endregion Data Methods

        #region Context Menu Testing Methods : 

        /// <summary>
        /// Applies the assigned <b>BuffData</b> to the target's buff container. <br/>
        /// Useful for quick editor testing. 
        /// </summary>
        [ContextMenu("Apply Buff")]
        public void ApplyBuff()
        {
            AbilitySystemAPI.ApplyBuff(_buffContainer, _buffData);
        }

        /// <summary>
        /// Removes a buff instance created from the assigned <b>BuffData</b>. <br/>
        /// Useful for simulating removing an active buff from the target. 
        /// </summary>
        [ContextMenu("Remove Buff")]
        public void RemoveBuff()
        {
            AbilitySystemAPI.RemoveBuff(_buffContainer, new BuffInstance(_buffData));
        }

        /// <summary>
        /// Prints the current test stat values to the console. <br/>
        /// Useful for verifying stat changes after applying or removing buffs.
        /// </summary>
        [ContextMenu("Print Buff")]
        public void PrintBuffDetails()
        {
            Debug.Log("Buff Tester : Print Buff :\nBuff Name: " + _statName + "\nBuff Value: " + _statValue + "\n");
        }

        /// <summary>
        /// Simulates a turn start event for the buff system. <br/>
        /// Calls <b>AbilitySystemAPI.OnTurnStart</b> on the target container. <br/>
        /// Useful for progressing counters with debugs enabled.
        /// </summary>
        [ContextMenu("TurnStart")]
        public void TurnStartTest()
        {
            AbilitySystemAPI.OnTurnStart(_buffContainer);
        }

        #endregion Context Menu Testing Methods

    }
}
#endif
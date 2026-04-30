// Dependancies : 
using System.Collections.Generic;
using UnityEngine;
using System;
using ModularArchitecture.Data;
using ModularArchitecture.Conditions;
using AbilitySystem.Status;
using AbilitySystem.Buff;
using AbilitySystem.Ability;
using AbilitySystem.Supplies;

namespace AbilitySystem
{
    /// <summary>
    /// Central API for executing abilities, applying statuses, applying buffs, and managing turn‑based
    /// progression within the Ability System. Provides a unified interface for gameplay code to interact
    /// with abilities, buffs, and statuses. <br/>
    /// This class acts as the main integration layer between units, abilities, buffs, and statuses. <br/><br/>
    ///
    /// Contains : <br/>
    /// <b>- AbilityExecuted :</b> Event invoked when an ability successfully activates, <br/>
    /// <b>- AbilityCooldownReduced :</b> Event invoked when an ability's cooldown decreases, <br/>
    /// <b>- StatusApplied / StatusActivated / StatusRemoved :</b> Events for status lifecycle, <br/>
    /// <b>- BuffApplied / BuffRemoved :</b> Events for buff lifecycle, <br/>
    /// <b>- OnTurnStart / OnTurnEnd :</b> Turn‑based progression handlers, <br/>
    /// <b>- ExecuteAbility :</b> Executes an ability and applies all effects, <br/>
    /// <b>- ApplyStatus / RemoveStatus :</b> Status management helpers, <br/>
    /// <b>- ApplyBuff / RemoveBuff :</b> Buff management helpers. <br/><br/>
    ///
    /// Responsible for : <br/>
    /// <b>- Turn Processing :</b> Reducing cooldowns, ticking statuses, and removing expired effects, <br/>
    /// <b>- Ability Execution :</b> Checking conditions, consuming supplies, and activating effects, <br/>
    /// <b>- Event Dispatch :</b> Broadcasting ability, status, and buff events to listeners, <br/>
    /// <b>- Centralized Access :</b> Providing a single entry point for all ability‑related operations. <br/><br/>
    /// </summary>
    public sealed class AbilitySystemAPI
    {
        // Data Members : 

        private static readonly Lazy<AbilitySystemAPI> _underlyingInstance = new Lazy<AbilitySystemAPI>(() => new AbilitySystemAPI());

        /// <summary>
        /// Singleton instance of the AbilitySystemAPI.
        /// </summary>
        public static AbilitySystemAPI Instance => _underlyingInstance.Value;

        #region Data Members - Events

        public static event Action<IUnit, AbilityInstance> AbilityExecuted;
        public static event Action<IUnit, AbilityInstance> AbilityCooldownReduced;

        public static event Action<IStatusContainer, StatusInstance> StatusApplied;
        public static event Action<IStatusContainer, StatusInstance> StatusActivated;
        public static event Action<IStatusContainer, StatusInstance> StatusRemoved;

        public static event Action<IBuffContainer, BuffInstance> BuffApplied;
        public static event Action<IBuffContainer, BuffInstance> BuffRemoved;

        #endregion

        // Turn Control : 
        #region Data Methods - Turn Control

        /// <summary>
        /// Processes turn‑start logic for all active statuses on the given status container. <br/>
        /// Calls <b>OnTurnStart</b> on each StatusInstance and dispatches <b>StatusActivated</b> events
        /// for any statuses that report activation. <br/>
        /// Used to trigger effects such as regeneration, cleansing, or other start‑of‑turn behaviors.
        /// </summary>
        /// <param name="aCurrentStatusContainer">The status container whose statuses should be processed.</param>
        public static void OnTurnStart(IStatusContainer aCurrentStatusContainer)
        {
            StatusManager StatusManager = aCurrentStatusContainer.StatusManager;
            if (StatusManager == null) return;

            foreach (StatusInstance currentInstance in StatusManager.activeEffects)
            {
                bool activated = currentInstance.OnTurnStart(aCurrentStatusContainer);
                if (activated == true)
                {
                    StatusActivated?.Invoke(aCurrentStatusContainer, currentInstance);
                }
            }
        }

        /// <summary>
        /// Processes turn‑start logic for all active buffs on the given buff container. <br/>
        /// Reduces buff durations, removes expired buffs, and dispatches <b>BuffRemoved</b> events through the buff manager. <br/>
        /// Used to handle timed buffs such as temporary stat boosts or short‑duration effects.
        /// </summary>
        /// <param name="aCurrentStatsContainer">The buff container whose buffs should be processed.</param>
        public static void OnTurnStart(IBuffContainer aCurrentStatsContainer)
        {
             Debug.Log("On Turn Start - Stats");

            List<BuffInstance> Instances = new List<BuffInstance>(aCurrentStatsContainer.buffManager.buffInstances);

            Debug.Log("Pre Loop");
            Debug.Log(aCurrentStatsContainer.buffManager.buffInstances.Count);

            foreach (BuffInstance CurrentInstance in Instances)
            {
                Debug.Log("INstance cooldown Reduced");
               
                CurrentInstance.currentDuration = CurrentInstance.currentDuration - 1;
                Debug.Log(CurrentInstance.currentDuration);

                if (CurrentInstance.currentDuration <= 0)
                {
                    Debug.Log("Removing Buff");
                    RemoveBuff(aCurrentStatsContainer, CurrentInstance);
                }
            }
        }


        /// <summary>
        /// Processes turn‑start logic for a full unit. <br/>
        /// Executes both status turn‑start logic and buff turn‑start logic in sequence. <br/>
        /// Equivalent to calling <b>OnTurnStart(IStatusContainer)</b> and <b>OnTurnStart(IBuffContainer)</b> on the same unit.
        /// </summary>
        /// <param name="aCurrentUnit">The unit whose turn‑start logic should be processed.</param>
        public static void OnTurnStart(IUnit aCurrentUnit)
        {
            OnTurnStart((IStatusContainer)aCurrentUnit);
            OnTurnStart((IBuffContainer)aCurrentUnit);
        }


        /// <summary>
        /// Processes turn‑end logic for a full unit. <br/>
        /// Handles status ticking, status expiration, ability cooldown reduction, and dispatches <b>StatusActivated</b> and <b>AbilityCooldownReduced</b> events as needed. <br/>
        /// Used to finalize all end‑of‑turn effects such as damage‑over‑time, healing‑over‑time, and ability cooldown progression.
        /// </summary>
        /// <param name="aCurrentUnit">The unit whose turn‑end logic should be processed.</param>
        public static void OnTurnEnd(IUnit aCurrentUnit)
        {
            StatusManager UnitStatusManager = aCurrentUnit.StatusManager;
            if (UnitStatusManager == null) return;

            List<StatusInstance> Instances = new List<StatusInstance>(UnitStatusManager.activeEffects);

            foreach (StatusInstance CurrentInstance in Instances)
            {
                bool activated = CurrentInstance.OnTurnEnd(aCurrentUnit);
                if (activated == true)
                {
                    StatusActivated?.Invoke(aCurrentUnit, CurrentInstance);
                }

                CurrentInstance.currentDuration = CurrentInstance.currentDuration - 1;

                if (CurrentInstance.currentDuration <= 0)
                {
                    RemoveStatus(aCurrentUnit, CurrentInstance);
                }
            }

            // Cooldowns : 
            foreach (AbilityInstance CurrentInstance in aCurrentUnit.abilityInstances)
            {
                int currentCooldown = CurrentInstance.currentCooldown;
                CurrentInstance.currentCooldown = Math.Max(0, CurrentInstance.currentCooldown - 1);

                if (CurrentInstance.currentCooldown != currentCooldown)
                {
                    AbilityCooldownReduced?.Invoke(aCurrentUnit, CurrentInstance);
                }
            }

        }

        #endregion Data Methods - Turn Control
        #region Data Methods - Abilities

        /// <summary>
        /// Determines whether an ability instance can currently be activated. <br/>
        /// Checks cooldowns, conditions, and supply requirements. <br/><br/>
        /// Returns <b>false</b> if: <br/>
        /// - The ability is on cooldown, <br/>
        /// - Any condition evaluates to false, <br/>
        /// - Any supply requirement is not met. <br/><br/>
        /// </summary>
        /// <param name="Instance">The ability instance being evaluated for activation.</param>

        public static bool IsActivatable(AbilityInstance Instance)
        {
            if (Instance.currentCooldown > 0)
            {
                Debug.Log("Ability Instance Cooldown");
                return false;
            }

            foreach (ConditionReference currentConditionReference in Instance.ability.conditions)
            {
                Condition currentCondition = currentConditionReference.value;
                if (currentCondition.Evaluate() == false)
                {
                    Debug.Log("Conditions False");
                    return false;
                }
            }

            foreach (Supply currentSupplies in Instance.ability.supplies)
            {
                if (currentSupplies.Evaluate() == false)
                {
                    Debug.Log("Supplies False");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Executes an ability on a target unit. <br/>
        /// Consumes supplies, activates all effects, sets cooldowns, and dispatches <b>AbilityExecuted</b>. <br/>
        /// This method assumes the ability is valid for activation and will not execute
        /// if <b>IsActivatable</b> returns false.
        /// </summary>
        /// <param name="aUser">The unit activating the ability.</param>
        /// <param name="aTarget">The unit receiving the ability's effects.</param>
        /// <param name="aInstance">The runtime ability instance being executed.</param>
        public static void ExecuteAbility(IUnit aUser, IUnit aTarget, AbilityInstance aInstance)
        {
            Debug.Log("Instance Activation Called");

            if (IsActivatable(aInstance) == false)
            {
                Debug.Log("Ability Instance Not Executable");
                return;
            }

            foreach (Supply currentSupplies in aInstance.ability.supplies)
            {
                currentSupplies.Use();
            }

            foreach (Effect currentEffect in aInstance.ability.plugInEffects)
            {
                currentEffect.Activate(aUser, aTarget);
            }

            // Audio : 
            // ability.audioClips.ForEach(currentClip => source?.PlayOneShot(currentClip));

            // Cooldown : 
            aInstance.currentCooldown = aInstance.ability.turnCooldown;

            AbilityExecuted?.Invoke(aTarget, aInstance);
        }

        #endregion Data Methods - Abilities
        #region Data Methods - Stauts

        /// <summary>
        /// Applies a status effect to the target unit. <br/>
        /// Creates a new <b>StatusInstance</b> through the target's StatusManager and dispatches
        /// a <b>StatusApplied</b> event if the application succeeds. <br/>
        /// Used when abilities or effects need to apply a new status to a unit.
        /// </summary>
        /// <param name="aTargetUnit">The unit receiving the status effect.</param>
        /// <param name="data">The StatusData asset defining the effect to apply.</param>
        public static void ApplyStatus(IStatusContainer aTargetUnit, StatusData data)
        {
            if (aTargetUnit?.StatusManager == null) return;

            StatusInstance? AppliedInstance = aTargetUnit.StatusManager.ApplyEffect(data);
            if (AppliedInstance != null)
            {
                StatusApplied?.Invoke(aTargetUnit, AppliedInstance);
            }
        }

        /// <summary>
        /// Removes a status effect from the target unit using its <b>StatusData</b> reference. <br/>
        /// If the status is found and removed, a <b>StatusRemoved</b> event is dispatched. <br/>
        /// Useful when removing a status by type rather than by instance.
        /// </summary>
        /// <param name="aTargetUnit">The unit whose status should be removed.</param>
        /// <param name="data">The StatusData asset identifying which status to remove.</param>
        public static void RemoveStatus(IStatusContainer aTargetUnit, StatusData data)
        {
            if (aTargetUnit?.StatusManager == null) return;

            StatusInstance? targetInstance = aTargetUnit.StatusManager.RemoveEffect(data);

            if (targetInstance != null)
            {
                StatusRemoved?.Invoke(aTargetUnit, targetInstance);
            }
        }

        /// <summary>
        /// Removes a specific <b>StatusInstance</b> from the target unit. <br/>
        /// If the instance is successfully removed, a <b>StatusRemoved</b> event is dispatched. <br/>
        /// Useful when you already have a direct reference to the runtime status instance.
        /// </summary>
        /// <param name="aTargetUnit">The unit whose status instance should be removed.</param>
        /// <param name="aTargetInstance">The specific StatusInstance to remove.</param>
        public static void RemoveStatus(IStatusContainer aTargetUnit, StatusInstance aTargetInstance)
        {
            if (aTargetUnit?.StatusManager == null) return;

            StatusInstance? targetInstance = aTargetUnit.StatusManager.RemoveEffect(aTargetInstance);

            if (targetInstance != null)
            {
                StatusRemoved?.Invoke(aTargetUnit, targetInstance);
            }
        }

        #endregion Data Methods - Status

        #region Data Methods - Buffs

        /// <summary>
        /// Applies a buff to the target unit. <br/>
        /// Creates a new <b>BuffInstance</b> through the target's buff manager and dispatches
        /// a <b>BuffApplied</b> event when successful. <br/>
        /// Used when abilities or effects need to apply a new buff to a unit.
        /// </summary>
        /// <param name="aTargetUnit">The unit receiving the buff.</param>
        /// <param name="aBuffInstance">The BuffData asset defining the buff to apply.</param>
        public static void ApplyBuff(IBuffContainer aTargetUnit, BuffData aBuffInstance)
        {
            if (aTargetUnit?.buffManager == null) return;

            Debug.Log("Ability API Applying");
            BuffInstance AppliedInstance = aTargetUnit.buffManager.AddBuff(aBuffInstance);
            BuffApplied?.Invoke(aTargetUnit, AppliedInstance);
        }

        /// <summary>
        /// Removes a specific <b>BuffInstance</b> from the target unit. <br/>
        /// If the instance is successfully removed, a <b>BuffRemoved</b> event is dispatched. <br/>
        /// Useful when you already have a direct reference to the runtime buff instance.
        /// </summary>
        /// <param name="aTargetUnit">The unit whose buff instance should be removed.</param>
        /// <param name="buffInstance">The specific BuffInstance to remove.</param>
        public static void RemoveBuff(IBuffContainer aTargetUnit, BuffInstance buffInstance)
        {
            if (aTargetUnit?.buffManager == null) return;

            Debug.Log("Ability API Removing");

            BuffInstance? targetInstance = aTargetUnit.buffManager.RemoveBuff(buffInstance);

            if (targetInstance != null)
            {
                BuffRemoved?.Invoke(aTargetUnit, targetInstance);
            }
        }

        /// <summary>
        /// Removes a buff from the target unit using its <b>BuffData</b> reference. <br/>
        /// If the buff is found and removed, a <b>BuffRemoved</b> event is dispatched. <br/>
        /// Useful when removing a buff by type rather than by instance.
        /// </summary>
        /// <param name="aTargetUnit">The unit whose buff should be removed.</param>
        /// <param name="buffInstance">The BuffData asset identifying which buff to remove.</param>
        public static void RemoveBuff(IBuffContainer aTargetUnit, BuffData buffInstance)
        {
            if (aTargetUnit?.buffManager == null) return;

            Debug.Log("Ability API Removing");

            BuffInstance? targetInstance = aTargetUnit.buffManager.RemoveBuff(buffInstance);

            if (targetInstance != null)
            {
                BuffRemoved?.Invoke(aTargetUnit, targetInstance);
            }
        }

        #endregion Data Methods - Buffs



    }
}

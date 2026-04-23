using System.Collections.Generic;
using UnityEngine;
using System;
using ModularArchitecture;

namespace AbilitySystem
{
    public sealed class AbilitySystemAPI
    {
        // Events : 
        public static event Action<IUnit, AbilityInstance> AbilityExecuted;
        public static event Action<IUnit, AbilityInstance> AbilityCooldownReduced;

        public static event Action<IStatus, StatusInstance> StatusApplied;
        public static event Action<IStatus, StatusInstance> StatusActivated;
        public static event Action<IStatus, StatusInstance> StatusRemoved;

        public static event Action<IStats, BuffInstance> BuffApplied;
        public static event Action<IStats, BuffInstance> BuffRemoved;

        // Instance : 
        private static readonly Lazy<AbilitySystemAPI> UnderlyingInstance = new Lazy<AbilitySystemAPI>(() => new AbilitySystemAPI());
        public static AbilitySystemAPI Instance => UnderlyingInstance.Value;

        // Turn Control : 
        #region Turn Control

        public static void OnTurnStart(IStatus aCurrentStatusContainer)
        {
            Debug.Log("On Turn Start - Status");

            StatusEffectManager StatusManager = aCurrentStatusContainer.StatusManager;
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

        public static void OnTurnStart(IStats aCurrentStatsContainer)
        {
             Debug.Log("On Turn Start - Stats");

            List<BuffInstance> Instances = new List<BuffInstance>(aCurrentStatsContainer.buffManager.activeBuffs);

            Debug.Log("Pre Loop");
            Debug.Log(aCurrentStatsContainer.buffManager.activeBuffs.Count);

            foreach (BuffInstance CurrentInstance in Instances)
            {
                Debug.Log("INstance cooldown Reduced");
               
                CurrentInstance.Duration = CurrentInstance.Duration - 1;
                Debug.Log(CurrentInstance.Duration);

                if (CurrentInstance.Duration <= 0)
                {
                    Debug.Log("Removing Buff");
                    RemoveBuff(aCurrentStatsContainer, CurrentInstance);
                }
            }
        }

        public static void OnTurnStart(IUnit aCurrentUnit)
        {
            Debug.Log("On Turn Start - Unit");

            OnTurnStart((IStatus)aCurrentUnit);
            OnTurnStart((IStats)aCurrentUnit);
        } 
        

        public static void OnTurnEnd(IUnit aCurrentUnit)
        {
            StatusEffectManager UnitStatusManager = aCurrentUnit.StatusManager;
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

        #endregion Turn Control

        public static bool IsActivatable(AbilityInstance Instance)
        {
            if (Instance.currentCooldown > 0)
            {
                Debug.Log("Ability Instance Cooldown");
                return false;
            }

            foreach (Condition currentCondition in Instance.ability.conditions)
            {
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

        #region Stauts

        public static void ApplyStatus(IStatus aTargetUnit, StatusData data)
        {
            if (aTargetUnit?.StatusManager == null) return;

            StatusInstance? AppliedInstance = aTargetUnit.StatusManager.ApplyEffect(data);
            if (AppliedInstance != null)
            {
                StatusApplied?.Invoke(aTargetUnit, AppliedInstance);
            }
        }

        public static void RemoveStatus(IStatus aTargetUnit, StatusData data)
        {
            if (aTargetUnit?.StatusManager == null) return;

            StatusInstance? targetInstance = aTargetUnit.StatusManager.RemoveEffect(data);

            if (targetInstance != null)
            {
                StatusRemoved?.Invoke(aTargetUnit, targetInstance);
            }
        }

        public static void RemoveStatus(IStatus aTargetUnit, StatusInstance aTargetInstance)
        {
            if (aTargetUnit?.StatusManager == null) return;

            StatusInstance? targetInstance = aTargetUnit.StatusManager.RemoveEffect(aTargetInstance);

            if (targetInstance != null)
            {
                StatusRemoved?.Invoke(aTargetUnit, targetInstance);
            }
        }

        #endregion Status

        #region Buffs

        public static void ApplyBuff(IStats aTargetUnit, BuffData aBuffInstance)
        {
            if (aTargetUnit?.buffManager == null) return;

            Debug.Log("Ability API Applying");
            BuffInstance AppliedInstance = aTargetUnit.buffManager.AddBuff(aBuffInstance);
            BuffApplied?.Invoke(aTargetUnit, AppliedInstance);
        }

        public static void RemoveBuff(IStats aTargetUnit, BuffInstance buffInstance)
        {
            if (aTargetUnit?.buffManager == null) return;

            Debug.Log("Ability API Removing");

            BuffInstance? targetInstance = aTargetUnit.buffManager.RemoveBuff(buffInstance);

            if (targetInstance != null)
            {
                BuffRemoved?.Invoke(aTargetUnit, targetInstance);
            }
        }

        public static void RemoveBuff(IStats aTargetUnit, BuffData buffInstance)
        {
            if (aTargetUnit?.buffManager == null) return;

            Debug.Log("Ability API Removing");

            BuffInstance? targetInstance = aTargetUnit.buffManager.RemoveBuff(buffInstance);

            if (targetInstance != null)
            {
                BuffRemoved?.Invoke(aTargetUnit, targetInstance);
            }
        }

        #endregion Buffs



    }
}

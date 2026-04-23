using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class StatusTester : MonoBehaviour, IStatus
    {
        [SerializeField] private StatusData Status;

        [SerializeField] private StatusEffectManager statusEffectManager;
        public StatusEffectManager StatusManager => statusEffectManager;

        public GameObject Target;

        [ContextMenu("Apply Status")]
        public void ApplyStatus()
        {
            IStatus statusTarget = Target.GetComponent<IStatus>();
            if (statusTarget != null)
            {
                AbilitySystemAPI.ApplyStatus(statusTarget, Status);
            }
        }

        [ContextMenu("Remove Status")]
        public void RemoveStatus()
        {
            IStatus statusTarget = Target.GetComponent<IStatus>();
            if (statusTarget != null)
            {
                AbilitySystemAPI.RemoveStatus(statusTarget, Status);
            }
        }

        [ContextMenu("TurnStart")]
        public void TurnStartTest()
        {
            IStatus statusTarget = Target.GetComponent<IStatus>();
            if (statusTarget != null)
            {
                AbilitySystemAPI.OnTurnStart(statusTarget);
            }
        }

        public void ApplyStatusDamage(int damage)
        {

        }

                // DEBUG and Testing : 
    #if UNITY_EDITOR
        [Header("DEBUG: Testing Status'")]
            public StatusData statusData1;
            public StatusData statusData2;

            [ContextMenu("Add StatusEffect 1")]
            public void ApplyStatusEffect1()
            {
                IStatus statusTarget = Target.GetComponent<IStatus>();
                if (statusTarget != null)
                {
                    AbilitySystemAPI.ApplyStatus(statusTarget, statusData1);
                }
            }

            [ContextMenu("Add StatusEffect 2")]
            public void ApplyStatusEffect2()
            {
                IStatus statusTarget = Target.GetComponent<IStatus>();
                if (statusTarget != null)
                {
                    AbilitySystemAPI.ApplyStatus(statusTarget, statusData2);
                }
            }
        #endif

    }
}

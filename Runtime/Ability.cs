using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ModularArchitecture;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Create New Ability")]
    public class Ability : ScriptableObject
    {
        // Data : 
        [Header("Effects:")]
        [SerializeField] private string name;
        [SerializeField] private List<Effect> effects;
        
        [Header("Categorization:")]
        [SerializeField] private DataReference<DamageTypes> damageType;
        [SerializeField] private DataReference<AbilityCategory> category; 

        [Header("Requirements:")]
        [SerializeField] private List<Condition> conditions;
        [SerializeField] private List<Resource> resources;

        [Header("NDI:")]
        [SerializeField] private GameEvent NDIGameEvent;
        [SerializeField] private DataReference<int> NDITurnCooldown;
        [SerializeField] private DataReference<int> NDIRange;

        [SerializeField] private int currentCooldown = 0;

        [SerializeField] private GameEventListenerSerial listener;

        // Buffs
        // Status Effects
        // States

        public void TurnDown()
        {
            PrintDetails("Cooldown- Down ! :)");
            currentCooldown = currentCooldown - 1;
        }

        private void OnEnable()
        {
            Debug.Log("On Enable - Ability");
            listener.OnEnable();
        }

        private void PrintDetails()
        {
            Debug.Log("Ability Print Details:\nAbility Name: " + name);
        }

        private void PrintDetails(string message)
        {
            Debug.Log("Ability Print Details:\nAbility Name: " + name + "\n" + message);
        }


        public virtual void Activate(GameObject parent, GameObject target)
        {
            // Cooldown : 
            if (currentCooldown > 0)
            {
                PrintDetails("Cooldown still active for this ability.");
                return;
            }



            // check conditions
            foreach (Condition currentCondition in conditions)
            {
                if (currentCondition.Evaluate() == false)
                {
                    return;
                }
            }
        
            // Resources : 
            foreach (Resource currentResource in resources)
            {
                if (currentResource.Evaluate() == false)
                {
                    return;
                }
            }

            foreach (Resource currentResource in resources)
            {
                currentResource.Use();
            }


            // Conditions :
            foreach(Effect currentEffect in effects)
            {
                currentEffect.Activate(parent, target);
            }

            NDIGameEvent?.Raise();
            currentCooldown = NDITurnCooldown;
        }
    }
}
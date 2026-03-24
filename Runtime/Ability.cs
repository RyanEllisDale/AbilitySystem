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

        // Buffs
        // Status Effects
        // States




        private void Awake()
        {
            
        }

        public virtual void Activate(GameObject parent, GameObject target)
        {
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
        }
    }
}
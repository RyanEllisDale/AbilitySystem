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
        [SerializeField] private string name;
        [SerializeField] private List<Effect> effects;
        [SerializeField] private List<Condition> conditions;
        [SerializeField] private DataReference<AbilityCategory> category; 

        private void Awake()
        {
            
        }

        public virtual void Activate(GameObject parent, GameObject target)
        {
            // check conditions
            bool conditionsMet = true;
            foreach (Condition currentCondition in conditions)
            {
                if (currentCondition.Evaluate() == false)
                {
                    conditionsMet = false;
                }
            }
        
            if (conditionsMet == true)
            {
                foreach(Effect currentEffect in effects)
                {
                    currentEffect.Activate(parent, target);
                }
            }
            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ModularArchitecture;

namespace AbilitySystem
{
    // Action Type 
    public enum ActionTag
    {
        None, 
        Spell,
        Movement,
        Melee,
        Status
    }

    [CreateAssetMenu(menuName = "Abilities/Create New Ability")]
    public class Ability : ScriptableObject
    {
        // Data : 
        [Header("Effects:")]
        [SerializeField] private string name;
        [SerializeField] private List<Effect> effects;
        
        [Header("Categorization:")]
        [SerializeField] private DamageTypes damageType;
        [SerializeField] private AbilityCategory category; 
        [SerializeField] public List<ActionTag> actionTags;

        [Header("Requirements:")]
        [SerializeField] private List<Condition> conditions;
        [SerializeField] private List<Supply> Supplies;
        [SerializeField] public DataReference<int> turnCooldown;

        [Header("NDI:")]
        [SerializeField] private List<AudioClip> NDIaudioClips;
        [SerializeField] private GameEvent NDIGameEvent;
        [SerializeField] private DataReference<int> NDIRange;

        // Buffs
        // Status Effects
        // States

        // Disable

        private void PrintDetails()
        {
            Debug.Log("Ability Print Details:\nAbility Name: " + name);
        }

        private void PrintDetails(string message)
        {
            Debug.Log("Ability Print Details:\nAbility Name: " + name + "\n" + message);
        }

        // /// <summary>
        // /// Testing XML Parse
        // /// </summary>
        // /// <param name="a">First number</param>
        // /// <param name="b">Second number</param>
        // public void TestXML()
        // {
        //
        // }
        
        public virtual bool Activate(GameObject parent, IUnit target)
        {
            Debug.Log("Ability Activation Called");


            // check conditions
            foreach (Condition currentCondition in conditions)
            {
                if (currentCondition.Evaluate() == false)
                {
                    Debug.Log("Conditions False");
                    return false;
                }
            }
        
            // Supplies : 
            foreach (Supply currentSupplies in Supplies)
            {
                if (currentSupplies.Evaluate() == false)
                {
                    Debug.Log("Supplies False");
                    return false;
                }
            }

            foreach (Supply currentSupplies in Supplies)
            {
                currentSupplies.Use();
            }


            
            foreach(Effect currentEffect in effects)
            {
                currentEffect.Activate(parent, target);
            }

            // NDIGameEvent?.Raise();

            return true;
        }
    }
}
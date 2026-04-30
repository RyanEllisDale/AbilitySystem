// Dependancies : 
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// Small enumeration type for telling buffs ( positive and negative ) how their value responds to the respective target stat. <br/> 
    /// Stat values are modified upon application and removal of a buff. <br/> <br/>
    /// <b>Add :</b> Addiction / Subtract to target stat. <br/>
    /// <b>Multiply :</b> Multiplication / Divison to target stat. <br/>
    /// </summary>
    public enum StatModifierType : byte
    { 
        Add,
        Multiply
    }

    /// <summary>
    /// Stat Modifier is a small struct that contains the rules and values for changing and how to change a stat <br/> <br/>
    /// Contains : <br/>
    /// <b>- targetStatID :</b> The string identifier / name of that stat to be modified, <br/>
    /// <b>- modifierValue :</b> The float value that will be used to change the targetStat relative to the modifierType, <br/>
    /// <b>-cmodifierType :</b> Small enum which determines how the modifierValue will affect the targetStat. 
    /// </summary>
    [System.Serializable]
    public struct StatModifier
    {
        // Data Members : 
        public string targetStatID; // Also the name of the element in BuffData Inspector List View
        public float modifierValue;
        public StatModifierType modifierType;
    }

    /// <summary>
    /// Data asset for buffs, contains all the permenant data for buffs, and lives within the project's files. <br/>
    /// Contains the list of modifier rules which determine how buffs interact with the stats of <b>IBuff</b> references. <br/> <br/>
    /// Contains : <br/>
    /// <b>- id :</b> A unique string identifier for referencing this buff, <br/>
    /// <b>- description :</b> A multiline description used for UI, debugging, or doccumentation, <br/>
    /// <b>- duration :</b> How many turns the buff remains active once applied, <br/>
    /// <b>- modifiers :</b> The array of <b>StatModifier</b> rules that define how this buff affects stats. <br/>
    /// </summary>
    [CreateAssetMenu(fileName = "New Buff",  menuName ="Abilities/Buffs/Create New Buff", order = 0)]
    public class BuffData : ScriptableObject
    {
        // Data Members : 
        [Header("Details :")]
        public string id;
        [Multiline] public string description;

        [Header("Effects :")]
        public int duration = 1;
        [SerializeField] private StatModifier[] _modifiers; // permenant modifier data for the asset.
        public StatModifier[] modifiers => _modifiers; // Readonly modifier data for referencing.
    }
}

// Dependancies : 
using UnityEngine;

namespace AbilitySystem
{ 
    public enum BuffModifierType
    { 
        Add,
        Multiply
    }

    [System.Serializable]
    public struct Modifier
    {
        public string stat;
        public BuffModifierType type;
        public float value;
    }

    [CreateAssetMenu(fileName = "New Buff",  menuName ="Abilities/Buffs/Create New Buff", order = 0)]
    public class BuffData : ScriptableObject
    {
        public string id;
        public int duration;
        public Modifier[] modifiers;
    }
}

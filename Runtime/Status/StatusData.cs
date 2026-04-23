using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public struct placeholderModifier
    {
        public int value;
    }

    // Data Asset For Status Effects 
    // [CreateAssetMenu(menuName = "Abilities/Create New Status")]

    public abstract class StatusData : ScriptableObject
    {
        [SerializeField] public string name;
        [Multiline, SerializeField] public string description;
        [SerializeField] public int duration = 1; 

        // [SerializeField] public int valueOverTime;
        // [SerializeField] public List<placeholderModifier> modifiers; 
        // [SerializeField] public List<ActionTag> targetTags;

        public List<StatusData> components = new List<StatusData>();

        public abstract StatusInstance CreateInstance();
    }
}
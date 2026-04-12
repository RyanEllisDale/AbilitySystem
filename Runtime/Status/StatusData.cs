using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [System.Serializable]
    public struct Combinations
    {
        [SerializeField] private string name;
        [SerializeField] public List<StatusData> combinesWith;
        [SerializeField] public List<StatusData> resultsIn;
    }

    public struct placeholderModifier
    {
        public int value;
    }

    // Data Asset For Status Effects 
    [CreateAssetMenu(menuName = "Abilities/Create New Status")]
    public class StatusData : ScriptableObject
    {
        [SerializeField] public string name;
        [SerializeField] public string description;
        [SerializeField] public int duration; 
        [SerializeField] public int valueOverTime;
        [SerializeField] public List<placeholderModifier> modifiers; 
        [SerializeField] public List<ActionTag> targetTags;
        [SerializeField] public List<Combinations> combinations;
    }
}

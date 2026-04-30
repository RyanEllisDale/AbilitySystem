// Dependancies :
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.Buff
{
    [System.Serializable]
    public class Stat
    {
        public string id;
        public float baseValue;
        public float value;
    }

    [CreateAssetMenu(fileName = "New Buff Unit Data", menuName = "Abilities/Samples/Create New Buff Unit", order = 1)]
    public class BuffUnitData : ScriptableObject
    {
        // Stats : 
        public List<Stat> stats;

        [ContextMenu("Initialize)")]
        public void Init()
        {
            for (int i = 0; i < stats.Count; i++)
            {
                stats[i].value = stats[i].baseValue;
            }
        }
    }
}
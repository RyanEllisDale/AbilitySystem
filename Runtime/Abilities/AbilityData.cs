using System.Collections.Generic;
using UnityEngine;
using ModularArchitecture;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Create New Ability", order = -1)]
    public class AbilityData : ScriptableObject
    {
        // Data : 
        [Header("Effects:")]
        [SerializeField] public string ID;
        [SerializeField] public List<AudioClip> audioClips;
        [SerializeField] public List<Effect> plugInEffects;
        [SerializeField] public int range;

        [Header("Categorization:")]
        [SerializeField] public DamageType damageType;
        [SerializeField] public AbilityCategory category; 

        [Header("Requirements:")]
        [SerializeField] public List<DataReference<Condition>> conditions;
        [SerializeField] public List<Supply> supplies;
        [SerializeField] public DataReference<int> turnCooldown;
    }
}
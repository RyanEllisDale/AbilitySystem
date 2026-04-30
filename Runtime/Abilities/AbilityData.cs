// Dependancies : 
using System.Collections.Generic;
using UnityEngine;
using ModularArchitecture.Data;
using AbilitySystem.Supplies;

namespace AbilitySystem.Ability
{
    /// <summary>
    /// Data asset defining an ability within the Ability System. Stores all permanent configuration
    /// such as effects, audio, categorization, requirements, and cooldown settings. <br/><br/>
    ///
    /// Contains : <br/>
    /// <b>- id :</b> A unique string identifier for referencing this ability, <br/>
    /// <b>- audioClips :</b> A list of audio clips to be referenced by developers ( via event calls or reading data ), <br/>
    /// <b>- plugInEffects :</b> A list of Effect objects executed when the ability is used , <br/>
    /// <b>- range :</b> The maximum distance at which the ability can target ( must be implemented ny developers for their own range system ), <br/>
    /// <b>- damageType :</b> The type of damage this ability deals, <br/>
    /// <b>- abilityCategory :</b> The ability category this ability belongs to, <br/>
    /// <b>- conditions :</b> Requirements that must be met before the ability can be used, <br/>
    /// <b>- supplies :</b> Resource costs required to activate the ability, <br/>
    /// <b>- turnCooldown :</b> The number of turns required before the ability can be reused. <br/>
    /// </summary>
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Create New Ability", order = -1)]
    public class AbilityData : ScriptableObject
    {
        // Mwember Data : 
        [Header("Effects:")]
        [SerializeField] public string id;
        [SerializeField] public List<AudioClip> audioClips;
        [SerializeField] public List<GameObject> particlePrefabs;
        [SerializeField] public List<Effect> plugInEffects;
        [SerializeField] public int range;

        [Header("Categorization:")]
        [SerializeField] public DamageType damageType;
        [SerializeField] public AbilityCategory abilityCategory; 

        [Header("Requirements:")]
        [SerializeField] public List<ConditionReference> conditions;
        [SerializeField] public List<Supply> supplies;
        [SerializeField] public DataReference<int> turnCooldown;
    }
}
// Dependancies : 
using UnityEngine;

namespace AbilitySystem
{
    [System.Serializable]
    public class AbilityInstance
    {
        // Member Data : 
        [SerializeField] public AbilityData ability;
        [SerializeField] public int currentCooldown = 0;
    }
}

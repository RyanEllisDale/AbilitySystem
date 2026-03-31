using UnityEngine;
using ModularArchitecture;

namespace AbilitySystem
{   
    [System.Serializable]
    public class Supply
    {
        [SerializeField] private string name;
        [SerializeField] private DataReference<float> resource;
        [SerializeField] private DataReference<float> cost;

        public bool Evaluate() { return resource.value >= cost.value; }
        public void Use() { resource.value = resource.value - cost.value; }
    }
}

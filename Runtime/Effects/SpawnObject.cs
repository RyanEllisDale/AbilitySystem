using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="SpawnObject", menuName="Abilities/Effects/SpawnObject")]
    public class SpawnObject : Effect
    {
        // Data Variables:
        [SerializeField] private GameObject objectPrefab; 
        [SerializeField] private Vector3 offset;
        
        // Activation :
        public override void Activate(GameObject parent, IUnit target)
        {
            // GameObject newObject = Instantiate(objectPrefab, target.transform.position + offset, target.transform.rotation);
        }
    }
}
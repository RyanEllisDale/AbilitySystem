using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="SpawnObject", menuName="Abilities/Effects/Effect : SpawnObject", order = 1)]
    public class SpawnObjectEffect : Effect
    {
        // Data Variables:
        [SerializeField] private GameObject objectPrefab; 
        [SerializeField] private Vector3 offset;
        
        // Activation :
        public override void Activate(IUnit parent, IUnit target)
        {
            // GameObject newObject = Instantiate(objectPrefab, target.transform.position + offset, target.transform.rotation);
        }
    }
}
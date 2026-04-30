using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// Effect that spawns a prefab at the target's position with an optional offset. <br/><br/>
    /// Contains : <br/>
    /// <b>- _objectPrefab :</b> The object to spawn, <br/>
    /// <b>- _offset :</b> The positional offset from the target's location. <br/>
    /// </summary>
    [CreateAssetMenu(fileName="SpawnObject", menuName="Abilities/Effects/Effect : SpawnObject", order = 1)]
    public class SpawnObjectEffect : Effect
    {
        // Data Members:
        [SerializeField] private GameObject _objectPrefab; 
        [SerializeField] private Vector3 _offset;

        /// <summary>
        /// Spawns the configured prefab at the target's position plus offset.
        /// </summary>
        public override void Activate(IUnit parent, IUnit target)
        {
            // GameObject newObject = Instantiate(objectPrefab, target.transform.position + offset, target.transform.rotation);
        }
    }
}
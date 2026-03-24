using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="Push", menuName="Abilities/Effects/Push")]
    public class Push : Effect
    {
        // Data Variables:
        [SerializeField] private Vector2Int pushForce;
        
        // Activation :
        public override void Activate(GameObject parent, GameObject target)
        {
            target.GetComponent<GridInterface>().MoveUnit(pushForce);
        }
    }
}

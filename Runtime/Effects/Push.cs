using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="Push", menuName="Abilities/Effects/Effect : Push", order = 1)]
    public class PushEffect : Effect
    {
        // Data Variables:
        [SerializeField] private Vector2Int pushForce;
        
        // Activation :
        public override void Activate(IUnit parent, IUnit target)
        {
            target.MoveUnit(pushForce);
        }
    }
}

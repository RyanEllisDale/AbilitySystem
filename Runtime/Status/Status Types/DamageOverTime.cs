using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "DamageOverTime", menuName = "Abilities/Status/Status : DamageOverTime", order = 2)]
    public class DamageOverTimeStatusData : StatusData
    {
        public int damagePerTurn = 5;
        
        public override StatusInstance CreateInstance()
        {
            return new DamageOverTimeStatusInstance(this);
        }
    }

    public class DamageOverTimeStatusInstance : StatusInstance
    {
        [SerializeField] private DamageOverTimeStatusData dot;

        public DamageOverTimeStatusInstance(DamageOverTimeStatusData data) : base(data)
        {
            dot = data;
        }

        public override bool OnTurnEnd(IStatus target)
        {
            Debug.Log("Status Activation");
            target.ApplyStatusDamage(dot.damagePerTurn);
            return true;
        }
    }
}

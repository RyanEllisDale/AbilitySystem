using UnityEngine;

namespace AbilitySystem.Status
{
    public class StatusApplierSample : MonoBehaviour
    {
        public StatusUnitSample unit;
        public StatusData regenData;
        public StatusData damageData;

        [ContextMenu("Apply Damage Status")]
        public void ApplyDamage()
        {
            AbilitySystemAPI.ApplyStatus(unit, damageData);
        }

        [ContextMenu("Apply Regen Status")]
        public void ApplyRegen()
        {
            AbilitySystemAPI.ApplyStatus(unit, regenData);
        }

        [ContextMenu("Turn Start")]
        public void TurnStart()
        {
            AbilitySystemAPI.OnTurnStart(unit); 
        }

        [ContextMenu("Turn End")]
        public void TurnEnd()
        {
            AbilitySystemAPI.OnTurnEnd(unit);
        }
    }
}

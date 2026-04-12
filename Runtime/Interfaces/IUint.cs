using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public interface IUnit : IGrid, IHealth
    {
        public void ApplyStatusEffects(List<StatusInstance> aStatusInstanceList);
        public void ApplyStatus(StatusInstance aStatusInstance);
        public void RemoveStatus(StatusInstance aStatusInstance);
        public void ApplyDamage(int damage);
        public void TriggerStatus();
        public List<StatusInstance> GetStatus();
    }
}

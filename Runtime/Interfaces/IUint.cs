using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public interface IUnit : IGrid, IHealth, IStatus, IStats
    {
        public List<AbilityInstance> abilityInstances { get; }
    }
}

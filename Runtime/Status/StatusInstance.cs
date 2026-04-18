using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AbilitySystem
{
    // Serializable to see data in inspector 
    [System.Serializable]
    public class StatusInstance : IEquatable<StatusInstance>
    {
        [SerializeField] public StatusData data;
        [SerializeField] public int currentDuration = 0;

        public StatusInstance() {}
        public StatusInstance(StatusData aData)
        {
            data = aData;
        }

        public void Activation(IUnit target)
        {
            Debug.Log("Status Activation");
            target.ApplyDamage(data.valueOverTime);
        }

        public StatusData? FindStatusCombination(StatusData aStatusData)
        {
            if (data.combination.combinesWith == aStatusData)
                return data.combination.resultsIn;

            return null;
        }


        


        // Ignores Duration : 
        public bool Equals(StatusInstance other)
        {
            return data == other.data;
        }

        public override bool Equals(object obj)
        {
            if (obj is StatusInstance other) 
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return HashCode.Combine(data, currentDuration);
            }
        }

    }
}

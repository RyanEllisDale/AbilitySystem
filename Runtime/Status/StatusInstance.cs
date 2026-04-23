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

        public virtual bool OnTurnStart(IStatus target) { return false; }
        public virtual bool OnTurnEnd(IStatus target) { return false; }
        public virtual void OnApply(IStatus target) { }
        public virtual void OnRemove(IStatus target) { }

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class BuffInstance : IEquatable<BuffInstance>
    {
        public string Id => data.id;
        public int Duration { get; set; }

        public BuffData data;

        public BuffInstance(BuffData data)
        {
            this.data = data;
            Duration = data.duration;
        }

        public void OnApply(IStats target)
        {
            foreach (var mod in data.modifiers)
            {
                target.ModifyStat(mod.stat.ToLower(), mod.value, mod.type);
            }
        }

        public void OnExpire(IStats target)
        {
            foreach (var mod in data.modifiers)
            {
                float inverseValue = mod.value;

                switch (mod.type)
                {
                    case BuffModifierType.Add:
                        inverseValue = -mod.value;
                        break;

                    case BuffModifierType.Multiply:
                        if (mod.value != 0)
                            inverseValue = 1f / mod.value;
                        break;
                }

                target.ModifyStat(mod.stat.ToLower(), inverseValue, mod.type);
            }
        }

        // Ignores Duration : 
        public bool Equals(BuffInstance other)
        {
            return data == other.data;
        }

        public override bool Equals(object obj)
        {
            if (obj is BuffInstance other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return HashCode.Combine(data, Duration);
            }
        }
    }
}
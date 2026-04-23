using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class BuffManager
    {
        public List<BuffInstance> activeBuffs = new List<BuffInstance>();
        public IStats stats;

        public BuffManager(IStats stats)
        {
            this.stats = stats; 
        }

        public BuffInstance AddBuff(BuffData data)
        {
            BuffInstance buff = new BuffInstance(data);
            buff.OnApply(stats);
            activeBuffs.Add(buff);
            return buff;
        }

        public BuffInstance RemoveBuff(BuffInstance buff)
        {
            if (activeBuffs.Contains(buff) == true)
            {
                Debug.Log("Contains Buff");

                buff.OnExpire(stats);
                activeBuffs.Remove(buff);
                return buff;
            }

            return null;
        }

        public BuffInstance RemoveBuff(BuffData data)
        {
            // Find the active instance that matches this BuffData
            BuffInstance instance = activeBuffs.Find(b => b.data == data);

            if (instance != null)
            {
                instance.OnExpire(stats);
                activeBuffs.Remove(instance);
                return instance;
            }

            return null;
        }

    }
}

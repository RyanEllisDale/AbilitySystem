using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class BuffTester : MonoBehaviour, IStats
    {
        public BuffManager buffManager => new BuffManager(this);
        [SerializeField] private BuffData Buff;
        public GameObject gTarget;
        public IUnit target;

        private Dictionary<string, float> stats = new Dictionary<string, float>()
        {
            { "attack", 10 },
            { "speed", 5 },
            { "defense", 3 }
        };

        private void Start()
        {
            target = gTarget.GetComponent<IUnit>();
        }


        [ContextMenu("Apply Buff")]
        public void ApplyBuff()
        {
            AbilitySystemAPI.ApplyBuff(target, Buff);
        }

        [ContextMenu("Remove Buff")]
        public void RemoveBuff()
        {
            AbilitySystemAPI.RemoveBuff(target, new BuffInstance(Buff));
        }

        [ContextMenu("TurnStart")]
        public void TurnStartTest()
        {
            AbilitySystemAPI.OnTurnStart(target);
        }

        public float GetStat(string statId)
        {
            return stats.TryGetValue(statId, out float value) ? value : 0f;
        }

        public void ModifyStat(string statId, float amount, BuffModifierType type)
        {
            if (!stats.ContainsKey(statId))
                stats[statId] = 0;

            stats[statId] += amount;
        }
    }
}

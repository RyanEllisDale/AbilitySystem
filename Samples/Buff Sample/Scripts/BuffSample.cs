using System;
using UnityEngine;

namespace AbilitySystem.Buff
{
    public class BuffSample : MonoBehaviour, IBuffContainer
    {
        // Data Members :
        [SerializeField] private BuffUnitData _unitData;
        [SerializeField] private BuffManager _buffManager;
        public BuffManager buffManager => _buffManager;


        private void Awake()
        {
            _buffManager = new BuffManager(this);
        }

        public float? GetStat(string statID)
        {
            Stat foundStat = _unitData.stats.Find(stat => stat.id == statID);

            if (foundStat == null)
            {
                return null;
            }

            return foundStat.value;
        }

        public float? ModifyStat(string statID, float modifyAmount, StatModifierType modifyType)
        {
            Stat foundStat = _unitData.stats.Find(stat => stat.id == statID);

            if (foundStat == null)
            {
                return null;
            }

            switch (modifyType)
            {
                case StatModifierType.Add: foundStat.value = foundStat.value + modifyAmount; return foundStat.value;
                case StatModifierType.Multiply: foundStat.value = foundStat.value * modifyAmount; return foundStat.value;
                default: Debug.LogError("Buff Sample : ModifyStat : Unknown Stat Modifier Type for " + statID + " of modifier type : " + modifyType); return null;
            }
        }

    }
}
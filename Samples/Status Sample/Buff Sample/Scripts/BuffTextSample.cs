// Dependancies : 
using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem.Buff
{
    public class BuffTextSample : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private BuffUnitData _unitData;

        private void Awake()
        {
            _unitData.Init();
        }

        private void Update()
        {
            _text.text = "Stats:\n\n";
            foreach (Stat currentStat in _unitData.stats)
            {
                _text.text = _text.text + currentStat.id + ": Base Value: " + currentStat.baseValue + " Current Value: " + currentStat.value + "\n";
            }
        }
    }
}
// Dependancies :
using UnityEngine;

namespace AbilitySystem.Buff
{
    public class BuffApplyerSample : MonoBehaviour
    {
        [SerializeField] private BuffSample _sampleTarget;
        [SerializeField] private BuffData _buffData;

        [ContextMenu("Apply Buff")]
        public void ApplyBuff()
        {
            AbilitySystemAPI.ApplyBuff(_sampleTarget, _buffData);
        }


    }
}

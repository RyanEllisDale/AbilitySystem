// Dependancies :
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="Buff", menuName="Abilities/Status/Status : Buff", order = 2)]
    public class BuffStatusData : StatusData
    {
        // Data Members:
        public BuffData data;
        
        // Activation :
        public override StatusInstance CreateInstance()
	    {
    		return new BuffStatusInstance(this);
	    }
    }

    public class BuffStatusInstance : StatusInstance
    {
        // Data Members :
        [SerializeField] private BuffStatusData buffStatusData;

        // Construction : 
        public BuffStatusInstance(BuffStatusData data) : base(data)
        {
            buffStatusData = data;
        }
        
        // Data Methods : 
        public override void OnApply(IStatus target)
        {
            base.OnApply(target);

            if (target is IStats statsContainer)
            {
                AbilitySystemAPI.ApplyBuff(statsContainer, buffStatusData.data);
            }
        }
        
        public override void OnRemove(IStatus target)
        {
            base.OnRemove(target);

            if (target is IStats statsContainer)
            {
                AbilitySystemAPI.RemoveBuff(statsContainer, buffStatusData.data);
            }
        }   
    }

}
// Dependancies :
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="Regeneration", menuName="Abilities/Status/Status : Regeneration", order = 2)]
    public class RegenerationStatusData : StatusData
    {
        // Data Members:
        public int healthPerTurn = 1;
        
        // Activation :
        public override StatusInstance CreateInstance()
	    {
    		return new RegenerationStatusInstance(this);
	    }
    }

    public class RegenerationStatusInstance : StatusInstance
    {
        // Data Members :
        [SerializeField] private RegenerationStatusData regenData;
        
        // Construction : 
        public RegenerationStatusInstance(RegenerationStatusData data) : base(data)
        {
            regenData = data;
        }
        
        // Data Methods : 
        public override bool OnTurnStart(IStatus target)
        {
            target.ApplyStatusDamage(-regenData.healthPerTurn);
            return true;
        }
        
        
    }
}
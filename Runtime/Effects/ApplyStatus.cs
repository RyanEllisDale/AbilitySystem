// Dependancies :
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName="ApplyStatus", menuName="Abilities/Effects/ApplyStatus")]
    public class ApplyStatus : Effect
    {
        // Data Variables:
        [SerializeField] private StatusData status;

        
        // Activation :
        public override void Activate(GameObject parent, IUnit target)
        {
            StatusInstance statusInstance = new StatusInstance(status);


            //

            List<StatusInstance> targetStatus = target.GetStatus();

            // Redundancy :

            if (targetStatus.Contains(statusInstance) == true)
            {
                Debug.Log("DuplicateStatus");
                return;
            }

            // Combination Matrix : 
            
            foreach (StatusInstance currentInstance in targetStatus)
            {
                foreach (Combinations currentCombination in currentInstance.data.combinations)
                {
                    if (currentCombination.combinesWith.Contains(statusInstance.data))
                    {
                        Debug.Log("Combination Found");

                        List<StatusData> combinationResults = currentCombination.resultsIn;


                        Debug.Log(currentInstance.data.name + " has been removed");
                        target.RemoveStatus(currentInstance);

                        foreach(StatusData result in combinationResults)
                        {
                            Debug.Log(result.name + "Has been added");
                            target.ApplyStatus(new StatusInstance(result));
                        }


                        

                        return; 
                    }
                }
            }


            // status data combination 
            // target status 


            target.ApplyStatus(statusInstance);
        }


    }
}
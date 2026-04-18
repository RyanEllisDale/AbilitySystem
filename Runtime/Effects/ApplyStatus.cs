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
        [SerializeField] private StatusData statusData;

        
        // Recurse
        // public void ApplyStatusToTarget(GameObject parent, IUnit target, StatusData aStatusData)
        // {
        //     StatusInstance statusInstance = new StatusInstance(status);
        //     List<StatusInstance> targetStatus = target.GetStatus();

        //     // Redundancy :

        //     if (targetStatus.Contains(statusInstance) == true)
        //     {
        //         Debug.Log("DuplicateStatus");
        //         return;
        //     }

        //     // Combination Matrix : 
        //     bool hasCombination = false;
        //     for (int i = targetStatus.Count -1; i > -1; i = i - 1)
        //     {
        //         StatusInstance currentInstance = targetStatus[i];
        //         HashSet<StatusData> combinationResults = currentInstance.FindStatusCombination(statusInstance);

        //         if (combinationResults.Count > 0)
        //         {
        //             hasCombination = true;
        //             foreach (StatusData status in combinationResults)
        //             {
        //                 Debug.Log("Combination Found");                    
        //                 Debug.Log(status.name + "Has been added");

        //                 target.ApplyStatus(new StatusInstance(status));
        //             }

        //             Debug.Log(currentInstance.data.name + " has been removed");
        //             target.RemoveStatus(currentInstance);
        //         }
        //     }

        //     if (hasCombination == false)
        //     {
        //         target.ApplyStatus(statusInstance);
        //     } 


        // }

        // Activation :
        public override void Activate(GameObject parent, IUnit target)
        {
            // Make The Instance : 
            StatusInstance statusInstance = new StatusInstance(statusData);
            List<StatusInstance> targetStatusInstances = target.GetStatus();

            // Redundancy : 
            if (targetStatusInstances.Contains(statusInstance) == true)
            {
                Debug.Log("DuplicateStatus");
                return;
            }

            // Combination Matrix: 
            bool hasCombination = false;
            for (int i = targetStatusInstances.Count -1; i > -1; i = i - 1)
            {
                StatusInstance currentInstance = targetStatusInstances[i];
                StatusData? combinationResult = statusInstance.FindStatusCombination(currentInstance.data);
                
                if (combinationResult != null)
                {
                    hasCombination = true;
                    Debug.Log("Combination Found");                    
                    Debug.Log(combinationResult.name + "Has been added");
                    target.ApplyStatus(new StatusInstance(combinationResult));
                }
            }

            if (hasCombination == true)
            {
                Debug.Log(statusInstance.data.name + " has been removed");
                target.RemoveStatus(statusInstance);
                return;
            }

            Debug.Log(statusInstance.data.name + "Has been added");
            target.ApplyStatus(statusInstance);
        }
    }

}
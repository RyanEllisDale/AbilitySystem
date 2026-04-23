using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "Composite", menuName = "Abilities/Status/Status : Composite", order = 2)]
    public class CompositeStatusData : StatusData
    {
        public StatusData[] subEffects;
        public override StatusInstance CreateInstance()
        {
            return new CompositeStatusInstance(this);
        }
    }

    public class CompositeStatusInstance : StatusInstance
    {
        private CompositeStatusData composite;
        private List<StatusInstance> instances = new List<StatusInstance>();

        public CompositeStatusInstance(CompositeStatusData data) : base(data)
        {
            composite = data;
            foreach (StatusData currentData in composite.subEffects)
            {
                if (currentData == null)
                {
                    continue;
                }

                Debug.Log("Composite Status : " + currentData.name);


                instances.Add(currentData.CreateInstance());
            }    
        }

        public override bool OnTurnStart(IStatus target) 
        {
            bool activated = false;

            foreach (StatusInstance currentInstance in instances)
            {
                bool instanceActivated = currentInstance.OnTurnStart(target);
                if (instanceActivated == true) activated = true;
            }

            return activated;
        }

        public override bool OnTurnEnd(IStatus target) 
        {
            bool activated = false;

            foreach (StatusInstance currentInstance in instances)
            {
                bool instanceActivated = currentInstance.OnTurnEnd(target);
                if (instanceActivated == true) activated = true;
            }

            return activated;
        }

        public override void OnApply(IStatus target) 
        {
            foreach (StatusInstance currentInstance in instances)
            {
                currentInstance.OnApply(target);
            }
        }

        public override void OnRemove(IStatus target) 
        {
            foreach (StatusInstance currentInstance in instances)
            {
                currentInstance.OnRemove(target);
            }
        }
    }
}

// Dependancies :
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// StatusData representing a composite effect made up of multiple sub‑effects. <br/><br/>
    /// Contains : <br/>
    /// <b>- subEffects :</b> An array of StatusData assets that will activate together. <br/>
    /// </summary>
    [CreateAssetMenu(fileName = "Composite", menuName = "Abilities/Status/Status : Composite", order = 2)]
    public class CompositeStatusData : StatusData
    {
        // Data Members :
        public StatusData[] subEffects;

        /// <summary>
        /// Creates a runtime instance that manages all sub‑effect instances.
        /// </summary>
        public override StatusInstance CreateInstance()
        {
            return new CompositeStatusInstance(this);
        }
    }

    /// <summary>
    /// Runtime instance for <b>CompositeStatusData</b>. Manages and forwards activation
    /// events to all sub‑effect instances. <br/><br/>
    /// Contains : <br/>
    /// <b>- _compositeData :</b> Cached reference to the composite data asset, <br/>
    /// <b>- _instances :</b> Runtime instances created from each sub‑effect. <br/>
    /// </summary>
    public class CompositeStatusInstance : StatusInstance
    {
        // Data Members :
        private CompositeStatusData _compositeData;
        private List<StatusInstance> _instances = new List<StatusInstance>();

        // Construction :
        /// <summary>
        /// Constructs a CompositeStatusInstance and initializes all sub‑effect instances.
        /// </summary>
        public CompositeStatusInstance(CompositeStatusData data) : base(data)
        {
            _compositeData = data;

            foreach (StatusData currentData in _compositeData.subEffects)
            {
                if (currentData == null)
                    continue;

                _instances.Add(currentData.CreateInstance());
            }
        }

        #region Data Methods

        /// <summary>
        /// Forwards turn start events to all sub‑effect instances.
        /// </summary>
        public override bool OnTurnStart(IStatusContainer target)
        {
            bool activated = false;

            foreach (StatusInstance currentInstance in _instances)
            {
                if (currentInstance.OnTurnStart(target))
                    activated = true;
            }

            return activated;
        }

        /// <summary>
        /// Forwards turn end events to all sub‑effect instances.
        /// </summary>
        public override bool OnTurnEnd(IStatusContainer target)
        {
            bool activated = false;

            foreach (StatusInstance currentInstance in _instances)
            {
                if (currentInstance.OnTurnEnd(target))
                    activated = true;
            }

            return activated;
        }

        /// <summary>
        /// Applies all sub‑effect instances to the target.
        /// </summary>
        public override void OnApply(IStatusContainer target)
        {
            foreach (StatusInstance currentInstance in _instances)
            {
                currentInstance.OnApply(target);
            }
        }

        /// <summary>
        /// Removes all sub‑effect instances from the target.
        /// </summary>
        public override void OnRemove(IStatusContainer target)
        {
            foreach (StatusInstance currentInstance in _instances)
            {
                currentInstance.OnRemove(target);
            }
        }

        #endregion Data Methods
    }
}

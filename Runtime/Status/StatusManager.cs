// Dependancies : 
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace AbilitySystem
{
    /// <summary>
    /// Runtime manager responsible for handling all active <b>StatusInstance</b> effects applied to a unit. <br/>
    /// Supports applying, removing, mixing, and tracking status effects during gameplay. <br/><br/>
    /// Contains : <br/>
    /// <b>- activeEffects :</b> A list of all currently active StatusInstances, <br/>
    /// <b>- _owner :</b> The MonoBehaviour implementing <b>IStatus</b> that owns this manager, <br/>
    /// <b>- unitOwner :</b> A cached reference to the owning IStatus interface. <br/><br/>
    /// Responsible for : <br/>
    /// <b>- Applying Effects :</b> Creates instances, handles mixing logic, and triggers OnApply, <br/>
    /// <b>- Removing Effects :</b> Removes instances and triggers OnRemove, <br/>
    /// <b>- Mixing Logic :</b> Uses <b>StatusEffectMixer</b> to combine compatible effects, <br/>
    /// <b>- Component Validation :</b> Prevents adding effects that are components of active combined effects. <br/>
    /// </summary>
    [System.Serializable]
    public class StatusManager
    {
        // Data Members :
        public List<StatusInstance> activeEffects = new();

        [SerializeField] private MonoBehaviour _owner;

        /// <summary>
        /// The IStatus interface implemented by the owning MonoBehaviour.
        /// </summary>
        public IStatusContainer unitOwner => _owner as IStatusContainer;

        #region Unity Methods

        /// <summary>
        /// Constructs a new StatusManager for the given owner.
        /// </summary>
        /// <param name="aUitOwner">The MonoBehaviour implementing IStatus that owns this manager.</param>
        public StatusManager(MonoBehaviour aUitOwner)
        {
            _owner = aUitOwner;
        }

        #endregion Unity Methods

        #region Data Methods

        /// <summary>
        /// Applies a new status effect to the owner. Handles mixing logic to determine
        /// whether the effect should combine with existing effects before being applied. <br/><br/>
        /// </summary>
        /// <param name="newEffect">The StatusData asset representing the effect to apply.</param>
        /// <returns>
        /// The applied StatusInstance, or <b>null</b> if the effect was rejected or replaced by mixing.
        /// </returns>
        public StatusInstance ApplyEffect(StatusData newEffect)
        {
            // Prevent adding component effects if a combined effect is active
            if (IsComponentOfActive(newEffect))
                return null;

            // Start with the incoming effect as the only pending effect
            List<StatusData> pending = new List<StatusData> { newEffect };

            bool mixed;

            do
            {
                mixed = false;
                List<StatusData> next = new List<StatusData>();

                // Mix pending effects with active effects
                foreach (var p in pending)
                {
                    foreach (var active in new List<StatusInstance>(activeEffects))
                    {
                        var mix = StatusEffectMixer.instance.TryMix(active.data, p);

                        if (mix != null)
                        {
                            RemoveEffect(active.data);
                            next.Add(mix);
                            mixed = true;
                        }
                    }
                }

                // Mix pending effects with each other
                for (int i = 0; i < pending.Count; i++)
                {
                    for (int j = i + 1; j < pending.Count; j++)
                    {
                        var mix = StatusEffectMixer.instance.TryMix(pending[i], pending[j]);

                        if (mix != null)
                        {
                            next.Add(mix);
                            mixed = true;
                        }
                    }
                }

                // If we mixed, the next list becomes the new pending list
                if (mixed)
                    pending = next;

            } while (mixed);

            // Final combined effect
            StatusData finalEffect = pending[pending.Count - 1];

            return AddIfNotPresent(finalEffect);
        }

        /// <summary>
        /// Determines whether the given effect is a component of any currently active combined effect. <br/><br/>
        /// </summary>
        /// <param name="effect">The effect to check.</param>
        /// <returns>True if the effect is a component of an active effect, otherwise false.</returns>
        private bool IsComponentOfActive(StatusData effect)
        {
            foreach (var active in activeEffects)
            {
                if (active.data.components.Contains(effect))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the given effect if it is not already active. Creates a new instance,
        /// applies it to the owner, and stores it in the active list. <br/><br/>
        /// </summary>
        /// <param name="effect">The StatusData asset to apply.</param>
        /// <returns>The created StatusInstance, or null if the effect was already active.</returns>
        private StatusInstance AddIfNotPresent(StatusData effect)
        {
            if (!activeEffects.Exists(e => e.data == effect))
            {
                StatusInstance instance = effect.CreateInstance();
                activeEffects.Add(instance);
                instance.OnApply(unitOwner);
                return instance;
            }

            return null;
        }

        /// <summary>
        /// Removes a status effect based on its StatusData asset. Triggers OnRemove
        /// and removes the instance from the active list. <br/><br/>
        /// </summary>
        /// <param name="effect">The StatusData asset identifying the effect to remove.</param>
        /// <returns>The removed StatusInstance, or null if no matching effect was active.</returns>
        public StatusInstance? RemoveEffect(StatusData effect)
        {
            StatusInstance? instance = activeEffects.Find(e => e.data == effect);
            if (instance != null)
            {
                instance.OnRemove(unitOwner);
                activeEffects.Remove(instance);
            }

            return instance;
        }

        /// <summary>
        /// Removes a specific StatusInstance from the active list. Triggers OnRemove
        /// and returns the removed instance. <br/><br/>
        /// </summary>
        /// <param name="instance">The runtime instance to remove.</param>
        /// <returns>The removed instance, or null if it was not active.</returns>
        public StatusInstance? RemoveEffect(StatusInstance instance)
        {
            if (activeEffects.Remove(instance))
            {
                instance.OnRemove(unitOwner);
                return instance;
            }

            return null;
        }

        #endregion Data Methods
    }
}

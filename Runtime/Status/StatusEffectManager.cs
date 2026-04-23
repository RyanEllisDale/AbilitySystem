// Dependancies : 
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace AbilitySystem
{
    [System.Serializable]
    public class StatusEffectManager
    {
        // Data Members : 
        public List<StatusInstance> activeEffects = new();

        [SerializeField] private MonoBehaviour owner;

        public IStatus unitOwner => owner as IStatus;

        public StatusEffectManager(MonoBehaviour aUitOwner) 
        {
             owner = aUitOwner; 
        }

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

                Debug.Log("Hello Status");
                pending.ForEach(p => Debug.Log("Pending: " + p.name));
                next.ForEach(n => Debug.Log("Next: " + n.name));
                Debug.Log("Mixed: " + mixed);

                // If we mixed, the next list becomes the new pending list
                if (mixed)
                    pending = next;

            } while (mixed);

            // At this point, pending contains the final effect(s)
            // You want only the final combined effect
            StatusData finalEffect = pending[pending.Count - 1];

             return AddIfNotPresent(finalEffect);
        }

        private bool IsComponentOfActive(StatusData effect)
        {
            foreach (var active in activeEffects)
            {
                if (active.data.components.Contains(effect))
                    return true;
            }
            return false;
        }

        private StatusInstance AddIfNotPresent(StatusData effect)
        {
            if (!activeEffects.Exists(e => e.data == effect))
            {
                StatusInstance Instance = effect.CreateInstance();
                activeEffects.Add(Instance);
                Instance.OnApply(unitOwner);
                return Instance;
            }

            return null;
        }

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

        public StatusInstance? RemoveEffect(StatusInstance instance)
        {
            if (activeEffects.Remove(instance))
            {
                instance.OnRemove(unitOwner);
                return instance;
            }

            return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModularArchitecture;
using System;

namespace AbilitySystem
{
    [System.Serializable]
    public class AbilityInstance
    {
        // Member Data : 
        [SerializeField] private Ability ability;
        private int currentCooldown = 0;
        private GameEventListener listener;

        [ContextMenu("Turn Down")]
        public void TurnDown()
        {
            currentCooldown = Math.Max(0, currentCooldown - 1);
        }

        public void Initialize()
        {
            listener = new GameEventListener(Resources.Load<GameEvent>("TurnOver"), TurnDown );
            listener.SubscribeSelf();
        }

        public void Dispose()
        {
            listener.UnsubscribeSelf();
            listener.response.RemoveListener(TurnDown);
        }


        [ContextMenu("Activate")]
        public void Activate(GameObject parent, GameObject target)
        {
            // Cooldown : 
            if (currentCooldown > 0)
            {
                return;
            }

            ability.Activate(parent, target);
            currentCooldown = ability.turnCooldown;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityTester : MonoBehaviour
    {
        [SerializeField] private Ability ability;
        [SerializeField] private KeyCode key; 
        [SerializeField] private GameObject target;

        void Update()
        {
            if (Input.GetKeyDown(key) == true)
            {
                Activate();
            }
        }

        [ContextMenu("Activate")]
        private void Activate()
        {
            ability.Activate(gameObject, target);
        }
    }
}
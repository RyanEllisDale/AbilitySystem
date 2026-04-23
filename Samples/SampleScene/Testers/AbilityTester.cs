using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityTester : MonoBehaviour
    {
        [SerializeField] private AbilityInstance ability;
        [SerializeField] private KeyCode key; 
        [SerializeField] private GameObject target;
        [SerializeField] private bool onStart = false;

        public void Start()
        {
            if (onStart == true)
            {
                Activate();
            }
        }

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
            Debug.Log("Activated");
            IUnit unit = target.GetComponent<IUnit>();

            AbilitySystemAPI.ExecuteAbility(unit, unit, ability);
        }
    }
}
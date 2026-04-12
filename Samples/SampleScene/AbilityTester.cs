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

        private void OnEnable()
        {
            Initialize();
        }

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
            IUnit unit = target.GetComponent<IUnit>();
            ability.Activate(gameObject, unit);
        }

        [ContextMenu("Initalize")]
        public void Initialize()
        {
            ability.Initialize();
        }

        public void OnDisable()
        {
            ability.Dispose();
        }
    }
}
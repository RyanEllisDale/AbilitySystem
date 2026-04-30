using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem.Status
{
    public class StatusUnitSample : MonoBehaviour, IStatusContainer
    {
        [SerializeField] private StatusManager _statusManager;
        public StatusManager StatusManager => _statusManager;
        public float health = 100;
        public Text _healthText;
        public Text _listText;

        private void Awake()
        {
            _statusManager = new StatusManager(this);
        }

        public void ApplyStatusDamage(int damage)
        {
            Debug.Log($"[Unit] Took {damage} damage (negative = healing)");

            _healthText.text = "";

            if (damage > 0)
            {
                _healthText.text = "Unit took " + damage + " damage !\n";
            }
            else if (damage < 0)
            {
                _healthText.text = "Unit healed " + -damage + " hp !\n";
            }

            health = health - damage;
            _healthText.text = _healthText.text + "Health : " + health;
        }

        public void Update()
        {
            _listText.text = "Current Status Effects:\n";
            foreach (StatusInstance currentInstance in _statusManager.activeEffects)
            {
                _listText.text = _listText.text + currentInstance.data.id + " - Turns left: " + currentInstance.currentDuration + "\n";
            }


            
        }


    }
}
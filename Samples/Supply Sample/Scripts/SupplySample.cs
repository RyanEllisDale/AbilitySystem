using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem.Supplies
{
    public class SupplySample : MonoBehaviour
    {
        [SerializeField] private Supply _supply;
        [SerializeField] private Text _text;

        private void Start()
        {
            _text.text = _supply.id + ".\nResource: " + _supply.GetResource() + "\nCost: " + _supply.GetCost();
        }


        [ContextMenu("Use Resource")]
        public void Use()
        {
            if (_supply.Evaluate() == true)
            {
                _supply.Use();
                _text.text = _supply.id + " was used.\nResource: " + _supply.GetResource() + "\nCost: " + _supply.GetCost();
            }
            else
            {
                _text.text = _supply.id + " was not used because of insufficient resources.\nResource: " + _supply.GetResource() + "\nCost: " + _supply.GetCost();
            }
        }
    }

}
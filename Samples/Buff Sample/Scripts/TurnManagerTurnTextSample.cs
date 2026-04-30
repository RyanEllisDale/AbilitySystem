using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem.Buff
{
    public class TurnManagerTurnTextSample : MonoBehaviour
    {
        [SerializeField] private BuffTurnManager _manager;
        [SerializeField] private Text _text;

        void Update()
        {
            _text.text = "Turn: " + _manager.turnCount;
        }
    }
}
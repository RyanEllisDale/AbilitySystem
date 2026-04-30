// Dependancies :
using UnityEngine;

namespace AbilitySystem.Buff
{
    public class BuffTurnManager : MonoBehaviour
    {
        [SerializeField] private BuffSample _sampleTarget;
        public int turnCount = 0;

        [ContextMenu("Next Turn")]
        public void NextTurn()
        {
            AbilitySystemAPI.OnTurnStart(_sampleTarget);
            turnCount = turnCount + 1;
        }

    }
}

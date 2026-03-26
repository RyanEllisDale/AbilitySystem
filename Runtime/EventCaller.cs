using ModularArchitecture;
using UnityEngine;

namespace AbilitySystem
{
    public class EventCaller : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;

        public void Start()
        {
            gameEvent?.Raise();
        }


    }
}

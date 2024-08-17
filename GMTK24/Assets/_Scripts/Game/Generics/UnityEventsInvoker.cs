using UnityEngine;
using UnityEngine.Events;

namespace com.game.generics
{
    public class UnityEventsInvoker : MonoBehaviour
    {
        [SerializeField] private bool m_invokeOnStart = false;

        [Space(5)]

        [SerializeField] private UnityEvent m_event;

        private void Start()
        {
            if (m_invokeOnStart) Invoke();
        }

        public void Invoke()
        {
            m_event?.Invoke();
        }
    }
}

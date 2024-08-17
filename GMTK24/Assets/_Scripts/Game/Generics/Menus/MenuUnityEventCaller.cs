using com.absence.attributes;
using com.game.menus;
using UnityEngine;
using UnityEngine.Events;

namespace com.game.generics.menus
{
    [RequireComponent(typeof(Menu))]
    public class MenuUnityEventCaller : MonoBehaviour
    {
        [SerializeField, Readonly] private Menu m_menu;
        [SerializeField] private UnityEvent m_onOpen;
        [SerializeField] private UnityEvent m_onClose;

        private void Start()
        {
            m_menu.OnOpen += OnMenuOpens;
            m_menu.OnClose += OnMenuCloses;
        }

        private void OnMenuOpens()
        {
            m_onOpen?.Invoke();
        }

        private void OnMenuCloses()
        {
            m_onClose?.Invoke();
        }

        private void Reset()
        {
            m_menu = GetComponent<Menu>();
        }
    }
}

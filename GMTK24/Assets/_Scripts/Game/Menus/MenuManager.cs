using com.absence.attributes;
using com.absence.utilities;
using com.game.internals;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.menus
{
    [DefaultExecutionOrder(-100)]
    public class MenuManager : Singleton<MenuManager>
    {
        [SerializeField, Min(1)] private int m_maxMenuCount = 1;
        [SerializeField] private List<Menu> m_allMenus = new();

        [SerializeField, Readonly, Runtime] 
        private List<Menu> m_openMenus = new();

        public List<Menu> AllMenus => m_allMenus;

        public bool NoMenusOpen => m_openMenus.Count == 0;
        public bool AnyMenusOpen => m_openMenus.Count > 0;
        public Menu Last => m_openMenus.LastOrDefault();

        public event Action OnAnyMenuOpens = null;
        public event Action OnAllMenusClosed = null;

        protected override void Awake()
        {
            base.Awake();

            OnAnyMenuOpens = null;
            OnAllMenusClosed = null;

            m_allMenus.ForEach(m => Close(m));
        }

        public void Open(string menuName, bool forceOpen = false)
        {
            if(m_maxMenuCount == 0)
            {
                Debug.LogWarning("Max menu count of MenuManagers has set to '1'. You cannot open any menus.");
                return;
            }

            List<Menu> foundMenus = m_allMenus.Where(m => m.name.Equals(menuName)).ToList();

            if(foundMenus.Count == 0)
            {
                Debug.LogError($"There are no menus with the name: '{menuName}'.");
                return;
            }

            if(foundMenus.Count > 1)
            {
                Debug.LogError($"There are more than one menu with the name: '{menuName}'");
                return;
            }

            Menu menu = foundMenus[0];

            if (IsOpen(menu))
            {
                Debug.LogWarning($"The menu: '{menuName}' is already open.");
                if (!IsLast(menu)) BringFront(menu);
                return;
            }

            if (m_openMenus.Count >= m_maxMenuCount)
            {
                if(forceOpen)
                {
                    CloseFirst();

                    Open(menu);
                    return;
                }

                Debug.LogError($"Max menu count exceeded. Cannot open the menu: '{menuName}'.");
                return;
            }

            Open(menu);
            return;
        }
        public void Close(string menuName)
        {
            List<Menu> foundMenus = m_openMenus.Where(m => m.name.Equals(menuName)).ToList();

            if(foundMenus.Count == 0)
            {
                Debug.LogWarning($"The menu: '{menuName}' is not open. You cannot close it.");
                return;
            }

            Menu menu = foundMenus.FirstOrDefault();
            Close(menu);
        }
        public void CloseLast()
        {
            if(NoMenusOpen)
            {
                Debug.Log("There are no menus open right now. Cannot close last.");
                return;
            }

            Menu last = m_openMenus.LastOrDefault();
            Close(last);
        }
        public void CloseFirst()
        {
            if (NoMenusOpen)
            {
                Debug.Log("There are no menus open right now. Cannot close first.");
                return;
            }

            Menu first = m_openMenus.FirstOrDefault();
            Close(first);
        }
        public void Toggle(string menuName, bool forceOpen = false)
        {
            if (IsOpen(menuName)) Close(menuName);
            else Open(menuName, forceOpen);
        }

        public void ReselectFirstElement()
        {
            if (m_openMenus.Count == 0) return;

            Menu last = m_openMenus.LastOrDefault();
            last.SelectFirstElement();
        }

        public bool Exists(string menuName)
        {
            return m_allMenus.Any(m => m.name.Equals(menuName));
        }
        public bool IsOpen(string menuName)
        {
            return m_openMenus.Any(m => m.name.Equals(menuName));
        }
        public bool IsLast(string menuName)
        {
            if (m_openMenus.Count == 0) return false;
            return m_openMenus.LastOrDefault().name.Equals(menuName);
        }

        public bool IsInGenericMenu() => IsLast(Constants.GENERIC_MENU_NAME);

        bool IsOpen(Menu menu)
        {
            return m_openMenus.Contains(menu);
        }
        bool IsLast(Menu menu)
        {
            if (m_openMenus.Count == 0) return false;
            return m_openMenus.LastOrDefault().Equals(menu);
        }
        void Close(Menu menu)
        {
            menu.SetVisibility(false);
            if (m_openMenus.Contains(menu)) m_openMenus.Remove(menu);

            if (m_openMenus.Count == 0) OnAllMenusClosed?.Invoke();
            else BringFront(m_openMenus.LastOrDefault());
        }
        void Open(Menu menu)
        {
            menu.SetVisibility(true);
            m_openMenus.Remove(menu);
            m_openMenus.Add(menu);

            OnAnyMenuOpens?.Invoke();
        }
        void BringFront(Menu menu)
        {
            menu.BringFront();

            m_openMenus.Remove(menu);
            m_openMenus.Add(menu);
        }

        [Button("Find Menus In Scene")]
        void RefreshMenuList()
        {
            m_openMenus.Clear();
            m_allMenus = FindObjectsOfType<Menu>().ToList();
            //m_allMenus.ForEach(m => Close(m));
        }
    }
}

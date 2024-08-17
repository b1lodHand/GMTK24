using com.absence.attributes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.game.menus
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject m_firstSelectedElement;
        [SerializeField, Readonly] private bool m_isOpen = false;

        [Space(5)]

        [SerializeField, Readonly] GameObject m_lastSelectedElement;

        public event Action OnOpen = null;
        public event Action OnClose = null;

        public void ForceOpen() 
        {
            MenuManager.Instance.Open(name, true);
        }
        public void ForceClose()
        {
            MenuManager.Instance.Close(name);
        }
        public void ForceToggle()
        {
            if (m_isOpen) ForceClose();
            else ForceOpen();
        }
        public void ForceSetVisibility(bool visibility)
        {
            if (visibility) ForceOpen();
            else ForceClose();
        }

        public void Open()
        {
            m_isOpen = true;
            gameObject.SetActive(true);

            BringFront();

            OnOpen?.Invoke();
        }
        public void Close()
        {
            m_isOpen = false;
            gameObject.SetActive(false);

            m_lastSelectedElement = null;

            OnClose?.Invoke();
        }
        public void BringFront()
        {
            transform.SetAsLastSibling();
            SelectLastElement();
        }
        public void Toggle()
        {
            if (m_isOpen) Close();
            else Open();
        }
        public void SetVisibility(bool visibility)
        {
            if (visibility) Open();
            else Close();
        }

        public void SelectFirstElement()
        {
            if (m_firstSelectedElement != null) EventSystem.current.SetSelectedGameObject(m_firstSelectedElement);
            else EventSystem.current.SetSelectedGameObject(null);
        }

        public void SelectLastElement()
        {
            if (m_lastSelectedElement != null) EventSystem.current.SetSelectedGameObject(m_lastSelectedElement);
            else SelectFirstElement();
        }

        public void SetLastSelectedObject(GameObject currentSelectedGameObject)
        {
            m_lastSelectedElement = currentSelectedGameObject;
        }
    }
}

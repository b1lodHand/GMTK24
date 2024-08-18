using com.game.interactables;
using System;
using UnityEngine;

namespace com.game.elements
{
    public class Lever : Interactable
    {
        [Header("Initial Fields")]

        [SerializeField] private bool m_oneTimeUse = false;
        [SerializeField] private bool m_startActivated = false;

        [Header("Graphics")]
        [SerializeField] private SpriteRenderer m_renderer;
        [SerializeField] private Sprite m_activatedSprite;
        [SerializeField] private Sprite m_deactivatedSprite;

        public event Action<bool> OnStateChanged;
        public event Action OnActivation;
        public event Action OnDeactivation;

        bool m_used = false;
        bool m_active;

        private void Start()
        {
            if (m_startActivated) Activate();
            else Deactivate();
        }

        public void Toggle()
        {
            if (m_active) Deactivate();
            else Activate();
        }

        public void Activate()
        {
            SetState(true);
            OnActivation?.Invoke();
        }

        public void Deactivate()
        {
            SetState(false);
            OnDeactivation?.Invoke();
        }

        public void Set(bool state)
        {
            if (state != m_active) Toggle();
        }

        void SetState(bool newState)
        {
            if (m_used && m_oneTimeUse) return;
            if (!m_used) m_used = true;

            m_active = newState;

            if(m_renderer != null)
            {
                if (newState) m_renderer.sprite = m_activatedSprite;
                else m_renderer.sprite = m_deactivatedSprite;
            }

            OnStateChanged?.Invoke(newState);
        }

        protected override void Interact_Internal(InteractorData sender)
        {
            Toggle();
        }
    }
}

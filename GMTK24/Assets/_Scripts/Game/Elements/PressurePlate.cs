using com.absence.attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.elements
{
    public class PressurePlate : MonoBehaviour
    {
        public enum PressurePlateType
        {
            Hold = 0,
            Toggle = 1,
        }

        [Header("Initial Fields")]
        [SerializeField] private PressurePlateType m_type = PressurePlateType.Hold;
        [SerializeField] [Min(0.001f)] private float m_activationMass = 1f;
        [SerializeField, Readonly] bool m_isPressed = false;

        [Header("Graphics")]
        [SerializeField] private SpriteRenderer m_renderer;
        [SerializeField] private Sprite m_activatedSprite;
        [SerializeField] private Sprite m_deactivatedSprite;

        List<Rigidbody2D> m_pressers = new();

        public event Action<bool> OnStateChanged;
        public event Action OnPress;
        public event Action OnDeactivation;

        public bool IsPressed
        {
            get
            {
                return m_isPressed;
            }

            private set
            {
                if (value != m_isPressed)
                {
                    InvokeEvents(value);
                    RefreshGraphics(value);
                }

                m_isPressed = value;
            }
        }

        private void Start()
        {
            IsPressed = false;
        }

        void RefreshGraphics(bool newValue)
        {
            if (m_renderer == null) return;

            if (newValue) m_renderer.sprite = m_activatedSprite;
            else m_renderer.sprite = m_deactivatedSprite;
        }

        private void InvokeEvents(bool newValue)
        {
            OnStateChanged?.Invoke(newValue);
            if (newValue) OnPress?.Invoke();
            else OnDeactivation?.Invoke();
        }

        private void FixedUpdate()
        {
            if (m_type == PressurePlateType.Toggle) return;

            IsPressed = m_pressers.Sum(presser => presser.mass) >= m_activationMass;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_type == PressurePlateType.Toggle)
            {
                IsPressed = true;
                return;
            }

            Rigidbody2D rb = collision.attachedRigidbody;
            if (!m_pressers.Contains(rb)) m_pressers.Add(rb);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (m_type == PressurePlateType.Toggle) return;

            Rigidbody2D rb = collision.attachedRigidbody;
            if (m_pressers.Contains(rb)) m_pressers.Remove(rb);
        }

        public void DeactivateIfPossible()
        {
            if (m_type != PressurePlateType.Toggle) return;

            IsPressed = false;
        }
    }
}

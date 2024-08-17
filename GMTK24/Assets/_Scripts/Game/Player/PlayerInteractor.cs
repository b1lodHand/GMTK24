using com.absence.attributes;
using com.game.input;
using com.game.interactables;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.game.player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private int m_maxInteractables = 8;
        [SerializeField] private float m_radius;
        [SerializeField] private LayerMask m_layerMask;

        [SerializeField, Runtime, Readonly] private List<Interactable> m_foundInteractables = null;

        Collider2D[] m_foundColliders;
        int m_selectedInteractableIndex = 0;

        public event Action<Interactable> OnSelectedInteractableChanged;

        public bool CanInteract => (!Player.Instance.InDialogue) && (!InputManager.Instance.InUI) && (!InputManager.Instance.InRebindProcess);

        private void Awake()
        {
            m_foundColliders = new Collider2D[m_maxInteractables];
            m_foundInteractables = new();

            InputEventChannel.Player.OnInteractionInput += OnInteract;
            InputEventChannel.Player.OnSwitchInteractionInput += OnSwitchInteractable;
        }

        private void FixedUpdate()
        {
            ScanForInteractables();
        }

        private void OnInteract()
        {
            if (!CanInteract) return;

            Interactable interactable = GetTargetInteractable();
            if (interactable == null)
            {
                Debug.Log("there is nothing to interact.");
                return;
            }

            interactable.Interact(GeneratePlayerInteractorData());

            if (m_debugMode) Debug.Log($"interacted with '{interactable.gameObject.name}'.");
        }
        private void OnSwitchInteractable()
        {
            SetSelectedOfTarget(false);

            m_selectedInteractableIndex++;

            ClampTargetIndex();

            SetSelectedOfTarget(true);
            Interactable target = GetTargetInteractable();

            OnSelectedInteractableChanged?.Invoke(target);

            if (target == null) return;

            if (m_debugMode) Debug.Log($"switched interactable: '{target.gameObject.name}'.");
        }

        void ScanForInteractables()
        {
            Interactable previousTarget = GetTargetInteractable();
            if (previousTarget != null) previousTarget.SetSelectedByPlayer(false);

            m_foundInteractables.Clear();

            int colliderCount = Physics2D.OverlapCircleNonAlloc(transform.position, m_radius, m_foundColliders, m_layerMask);
            if (colliderCount == 0) return;

            for (int i = 0; i < colliderCount; i++)
            {
                if (!(m_foundColliders[i].attachedRigidbody.gameObject.TryGetComponent(out Interactable interactable))) continue;
                m_foundInteractables.Add(interactable);
            }

            ClampTargetIndex();

            if (m_foundInteractables.Contains(previousTarget))
            {
                m_selectedInteractableIndex = m_foundInteractables.IndexOf(previousTarget);
                SetSelectedOfTarget(true);
            }

            Interactable target = GetTargetInteractable();
            if (target != previousTarget) OnSelectedInteractableChanged?.Invoke(target);

            if (m_debugMode) Debug.Log($"found interactables: '{m_foundInteractables.Count}'");
        }

        void SetSelectedOfTarget(bool isSelected)
        {
            Interactable target = GetTargetInteractable();
            if(target == null) return;

            target.SetSelectedByPlayer(isSelected);
        }
        void ClampTargetIndex()
        {
            if (m_selectedInteractableIndex >= m_foundInteractables.Count) m_selectedInteractableIndex -= m_foundInteractables.Count;
        }

        public Interactable GetTargetInteractable()
        {
            if (m_foundInteractables.Count == 0) return null;
            if (m_selectedInteractableIndex >= m_foundInteractables.Count) return null;

            Interactable targetInteractable = m_foundInteractables[m_selectedInteractableIndex];
            if (targetInteractable == null) return null;

            return targetInteractable;
        }
        InteractorData GeneratePlayerInteractorData()
        {
            InteractorData data = new();
            data.Person = Player.Instance.Person;
            data.Scaler = Player.Instance.Hub.Scaler;

            return data;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_radius);
        }
    }
}

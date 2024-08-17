using com.absence.attributes;
using System;
using UnityEngine;

namespace com.game.interactables
{
    [DisallowMultipleComponent]
    public abstract class Interactable : MonoBehaviour
    {
        public event Action OnInteract = null;
        public event Action OnSelectedByPlayer = null;
        public event Action OnUnselectedByPlayer = null;

        [SerializeField] private bool m_useCustomMessageTextPosition = false;
        [SerializeField, ShowIf(nameof(m_useCustomMessageTextPosition))] private Transform m_customMessageTextTransform;

        public virtual string CustomInteractionMessageForPlayer { get; } = "Interact";
        public Vector2 GetCustomTextMessagePosition() => m_customMessageTextTransform.position;

        public bool UseCustomMessageTextPosition => m_useCustomMessageTextPosition;

        public void Interact(InteractorData interactor)
        {
            Interact_Internal(interactor);
            OnInteract?.Invoke();
        }
        public virtual void SetSelectedByPlayer(bool isSelected)
        {
            SetSelectedByPlayer_Internal(isSelected);

            if (isSelected) OnSelectedByPlayer?.Invoke();
            else OnUnselectedByPlayer?.Invoke();
        }

        protected abstract void Interact_Internal(InteractorData sender);
        protected virtual void SetSelectedByPlayer_Internal(bool isSelected)
        {

        }
    }
}

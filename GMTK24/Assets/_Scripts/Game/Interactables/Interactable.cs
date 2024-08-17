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

using UnityEngine;
using com.game.interactables;
using UnityEngine.Events;

namespace com.game.generics
{
    public class InteractableWithUnityEvents : Interactable
    {
        [SerializeField] private UnityEvent OnInteraction = null;

        protected override void Interact_Internal(InteractorData sender)
        {
            OnInteraction?.Invoke();
        }
    }

}
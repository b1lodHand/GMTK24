using com.game.interactables;
using UnityEngine;

namespace com.game.testing
{
    public class TestInteractable : Interactable
    {       
        protected override void Interact_Internal(InteractorData sender)
        {
            Debug.Log("interaction!");
            Destroy(gameObject);
        }
    }
}

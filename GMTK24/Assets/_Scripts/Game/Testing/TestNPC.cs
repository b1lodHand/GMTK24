using com.absence.dialoguesystem;
using com.game.interactables;
using UnityEngine;

namespace com.game.testing
{
    public class TestNPC : Interactable
    {
        [SerializeField] private DialogueInstance m_dialogueInstance;

        protected override void Interact_Internal(InteractorData sender)
        {
            if (m_dialogueInstance.InDialogue) return;

            m_dialogueInstance.EnterDialogue();
        }
    }
}

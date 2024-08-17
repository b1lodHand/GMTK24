using com.absence.attributes;
using com.absence.dialoguesystem;
using com.game.interactables;
using com.game.player;
using System;
using UnityEngine;

namespace com.game.generics
{
    public class InteractableDialogueStarter : Interactable
    {
        [SerializeField] private DialogueInstance m_dialogueInstance;
        [SerializeField, Readonly] bool m_occupiedByPlayer = false;

        public bool InDialogueWithPlayer => m_occupiedByPlayer;

        public event Action OnEnterDialogueWithPlayer = null;
        public event Action OnExitDialogueWithPlayer = null;

        private void Awake()
        {
            m_dialogueInstance.OnExitDialogue += OnExitDialogue;
        }

        protected override void Interact_Internal(InteractorData sender)
        {
            if (m_dialogueInstance.InDialogue) return;

            m_dialogueInstance.EnterDialogue();
            m_occupiedByPlayer = false;

            if (sender.IsPlayer())
            {
                Player.Instance.EnterDialogue(m_dialogueInstance);
                m_occupiedByPlayer = true;

                OnEnterDialogueWithPlayer?.Invoke();
            }
        }

        private void OnExitDialogue()
        {
            if (m_occupiedByPlayer)
            {
                Player.Instance.ExitDialogue();
                OnExitDialogueWithPlayer?.Invoke();
            }
        }
    }
}

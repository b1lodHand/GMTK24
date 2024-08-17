using com.absence.attributes;
using com.game.input;
using com.game.internals;
using com.game.player;
using UnityEngine;

namespace com.game.generics
{
    [RequireComponent(typeof(InteractableDialogueStarter))]
    public class AutoExitDialogueOnMove : MonoBehaviour
    {
        [SerializeField, Readonly] private InteractableDialogueStarter m_dialogueStarter;

        bool m_enabled = false;

        private void Start()
        {
            m_dialogueStarter.OnEnterDialogueWithPlayer += OnEnterDialogue;
            m_dialogueStarter.OnExitDialogueWithPlayer += OnExitDialogue;
            InputEventChannel.Player.OnMovementInput += OnMove;
        }

        private void OnMove(Vector2 vector)
        {
            if (!m_enabled) return;
            if (!InternalSettings.EXIT_DIALOGUE_ON_MOVE) return;
            if (!m_dialogueStarter.InDialogueWithPlayer) return;

            Player.Instance.ForceExitDialogue();
        }

        private void OnEnterDialogue()
        {
            m_enabled = true;
        }

        private void OnExitDialogue()
        {
            m_enabled = false;
        }

        private void Reset()
        {
            m_dialogueStarter = GetComponent<InteractableDialogueStarter>();
        }
    }
}

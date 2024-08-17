using com.absence.attributes;
using com.absence.dialoguesystem;
using com.absence.personsystem;
using com.absence.utilities;
using com.game.input;
using UnityEngine;

namespace com.game.player
{
    public class Player : Singleton<Player>
    {
        [SerializeField] private PlayerComponentHub m_componentHub;
        public PlayerComponentHub Hub => m_componentHub;

        [SerializeField] private Person m_person;   
        public Person Person => m_person;

        [SerializeField, Readonly] bool m_inDialogue = false;
        public bool InDialogue => m_inDialogue;

        DialogueInstance m_occupier = null;

        public bool EnterDialogue(DialogueInstance instance)
        {
            if (m_inDialogue) return false;

            InputManager.Instance.SwitchToDialogueMap();
            m_inDialogue = true;

            m_occupier = instance;
            return true;
        }

        public void ExitDialogue()
        {
            InputManager.Instance.SwitchToPlayerMap();
            m_inDialogue = false;
            m_occupier = null;
        }

        public void ForceExitDialogue()
        {
            m_occupier.ExitDialogue();
        }
    }
}

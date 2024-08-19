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

        [SerializeField] private PlayerCamera m_camera;
        public PlayerCamera Camera => m_camera;

        public Person Person => Hub.Entity.Person;
        public Transform Body => Hub.Entity.Body;
        public PlayerStats Stats => Hub.Entity.Hub.Stats as PlayerStats;


        [SerializeField, Readonly] bool m_inDialogue = false;
        public bool InDialogue => m_inDialogue;

        [SerializeField, Readonly] bool m_inCutscene = false;
        public bool InCutscene => m_inCutscene;

        [SerializeField, Readonly] bool m_isChewing = false;
        public bool IsChewing => m_isChewing;

        [SerializeField, Readonly] bool m_isEating = false;
        public bool IsEating => m_isChewing;

        DialogueInstance m_occupier = null;

        public bool IsAttacking => Hub.Combat.IsAttacking;

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

        public void StartEating()
        {
            if (m_isChewing) return;

            m_isChewing = true;
            m_isEating = true;
            PlayerEventChannel.Eating.CommitEatStart();
        }

        public void StopEating()
        {
            m_isEating = false;
        }

        public void StopChewing()
        {
            m_isChewing = false;
            PlayerEventChannel.Eating.CommitChewStop();
        }

        public void EnterCutscene()
        {
            m_inCutscene = true;
        }

        public void EndCutscene()
        {
            m_inCutscene = false;
        }

        void NotifyEatAnimationEnd()
        {
            StopEating();
            Hub.Animator.NotifyEatAnimationEnded();
        }
        void NotifyCombatAnimationEnd()
        {
            Hub.Animator.NotifyCombatAnimationEnded();
        }

        public void ForceExitDialogue()
        {
            m_occupier.ExitDialogue();
        }
    }
}

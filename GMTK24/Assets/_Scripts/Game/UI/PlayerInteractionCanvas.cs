using com.game.interactables;
using UnityEngine;

namespace com.game.player.ui
{
    public class PlayerInteractionCanvas : MonoBehaviour
    {
        [SerializeField] private PlayerInteractor m_interactor;
        [SerializeField] private PlayerInteractionMessage m_message;
        [SerializeField] private Vector2 m_offset = Vector2.zero;

        Interactable m_lastInteractable;

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            Interactable interactable = m_interactor.GetTargetInteractable();
            if (interactable == null || !m_interactor.CanInteract)
            {
                m_message.gameObject.SetActive(false);

                m_lastInteractable = interactable;
                return;
            }

            m_message.gameObject.SetActive(true);
            m_message.SetMessage(interactable.CustomInteractionMessageForPlayer);

            Vector2 targetPosition;

            if (interactable.UseCustomMessageTextPosition) targetPosition = m_message.transform.position = interactable.GetCustomTextMessagePosition() + m_offset;
            else targetPosition = m_message.transform.position = (Vector2)interactable.transform.position + m_offset;

            bool interactableChanged = false;
            if (m_lastInteractable == null) interactableChanged = true;
            else if (m_lastInteractable != interactable) interactableChanged = true;

            if (interactableChanged) m_message.FadeTo(targetPosition);
            else m_message.SnapTo(targetPosition);

            m_lastInteractable = interactable;
            return;
        }
    }
}

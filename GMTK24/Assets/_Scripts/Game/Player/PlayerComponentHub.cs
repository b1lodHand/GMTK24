using com.game.abilities;
using com.game.player;
using com.game.scaling.generics;
using UnityEngine;

namespace com.game
{
    public class PlayerComponentHub : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler m_inputHandler;
        [SerializeField] private PlayerInteractor m_interactor;
        [SerializeField] private PlayerMovement m_movement;
        [SerializeField] private PlayerAnimator m_animator;
        [SerializeField] private PlayerCombat m_combat;
        [SerializeField] private PlayerUIEventsHandler m_uiEventsHandler;
        [SerializeField] private EntityScaler m_scaler;
        [SerializeField] private PlayerAbilities m_abilities;

        public PlayerInputHandler InputHandler => m_inputHandler;
        public PlayerInteractor Interactor => m_interactor;
        public PlayerMovement Movement => m_movement;
        public PlayerAnimator Animator => m_animator;
        public PlayerCombat Combat => m_combat;
        public PlayerUIEventsHandler UIEventsHandler => m_uiEventsHandler;
        public EntityScaler Scaler => m_scaler;
        public PlayerAbilities Abilities => m_abilities;
    }
}

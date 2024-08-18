using com.absence.attributes;
using com.game.input;
//using DG.Tweening;
using UnityEngine;

namespace com.game.player
{
    public class PlayerAnimator : MonoBehaviour
    {
        public enum MoveDirection
        {
            Up = 0,
            Side = 1,
        }

        static readonly int s_idle_side_hash = Animator.StringToHash("player_idle_side");
        static readonly int s_idle_up_hash = Animator.StringToHash("player_idle_up");
        static readonly int s_walk_side_hash = Animator.StringToHash("player_walk_side");
        static readonly int s_walk_up_hash = Animator.StringToHash("player_walk_up");
        static readonly int s_eat_hash = Animator.StringToHash("player_eat");
        static readonly int s_chew_hash = Animator.StringToHash("player_chew");
        static readonly int s_chew_walk_hash = Animator.StringToHash("player_chew_while_walk");

        [Header("Initial Fields")]

        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private Animator m_animator;
        [SerializeField] private SpriteRenderer m_renderer;

        //[Header("Squeesh Effect")]

        //[SerializeField] private bool m_squeesh = true;
        //[SerializeField] [Min(0.01f)] private float m_squeeshSpeed;
        //[SerializeField] private float m_squeeshAmplitude;
        //[SerializeField] private Ease m_squeeshEase = Ease.InOutSine;

        [Header("Runtime")]
        [SerializeField, Readonly] bool m_isFacingRight = true;

        MoveDirection m_lastMoveDirection;
        Vector2 m_lastMovementInput;
        bool m_handledByCombatSystem = false;
        bool m_isChewing = false;
        bool m_inEatAnimation = false;

        bool p_isMoving => m_lastMovementInput != Vector2.zero;
        bool p_isAttacking => Player.Instance.IsAttacking;
        bool p_lastMovedUp => m_lastMoveDirection == MoveDirection.Up;

        private void Start()
        {
            InputEventChannel.Player.OnMovementInput += OnMove;
            PlayerEventChannel.OnStartChewing += OnStartChewing;
            PlayerEventChannel.OnStopChewing += OnStopChewing;

            m_isFacingRight = true;
            CrossfadeSide();
        }

        void CrossfadeSide()
        {
            if (p_isMoving) m_animator.CrossFade(s_walk_side_hash, 0f, 0);
            else m_animator.CrossFade(s_idle_side_hash, 0f, 0);
        }

        void CrossfadeUp()
        {
            if (p_isMoving) m_animator.CrossFade(s_walk_up_hash, 0f, 0);
            else m_animator.CrossFade(s_idle_up_hash, 0f, 0);
        }

        private void OnStartChewing()
        {
            if (p_isAttacking) return;

            m_isChewing = true;
            m_inEatAnimation = true;

            m_animator.CrossFade(s_eat_hash, 0f, 0);
        }

        private void OnStopChewing()
        {
            if (p_isAttacking) return;

            m_isChewing = false;

            if (p_lastMovedUp) CrossfadeUp();
            else CrossfadeSide();
        }

        private void OnMove(Vector2 vector)
        {
            if (m_debugMode) Debug.Log($"move: ({vector.x}, {vector.y})");

            m_lastMovementInput = vector;

            if (p_isMoving)
            {
                if (vector.y > 0f) m_lastMoveDirection = MoveDirection.Up;
                else m_lastMoveDirection = MoveDirection.Side;
            }

            if (m_handledByCombatSystem) return;
            if (m_inEatAnimation) return;

            if (vector == Vector2.zero) HandleStop();
            else HandleMovement();
        }

        void HandleMovement()
        {
            HandleFlip();

            if (m_isChewing && !p_lastMovedUp)
            {
                m_animator.CrossFade(s_chew_walk_hash, 0f, 0);
                return;
            }

            if (p_lastMovedUp) m_animator.CrossFade(s_walk_up_hash, 0f, 0);
            else m_animator.CrossFade(s_walk_side_hash, 0f, 0);
        }

        void HandleStop()
        {
            if (m_isChewing && !p_lastMovedUp)
            {
                m_animator.CrossFade(s_chew_hash, 0f, 0);
                return;
            }

            if (p_lastMovedUp) m_animator.CrossFade(s_idle_up_hash, 0f, 0);
            else m_animator.CrossFade(s_idle_side_hash, 0f, 0);
        }

        void HandleFlip()
        {
            if (m_lastMovementInput.x > 0f && !m_isFacingRight) Flip();
            else if (m_lastMovementInput.x < 0f && m_isFacingRight) Flip();
        }

        void Flip()
        {
            m_isFacingRight = !m_isFacingRight;
            m_renderer.flipX = !m_renderer.flipX;
        }

        public void CommitCombatAnimation(int hash)
        {
            m_handledByCombatSystem = true;
            m_animator.CrossFade(hash, 0f, 0);
        }

        public void NotifyEatAnimationEnded()
        {
            m_inEatAnimation = false;
            PlayerEventChannel.CommitEatAnimationEnd();

            if (m_handledByCombatSystem) return;

            if (m_isChewing && !p_lastMovedUp)
            {
                if (p_isMoving) m_animator.CrossFade(s_chew_walk_hash, 0f, 0);
                else m_animator.CrossFade(s_chew_hash, 0f, 0);

                return;
            }

            if (p_lastMovedUp) CrossfadeUp();
            else CrossfadeSide();
        }

        public void NotifyCombatAnimationEnded()
        {
            m_handledByCombatSystem = false;
            if(!m_isChewing)
            {
                if (p_lastMovedUp) CrossfadeUp();
                else CrossfadeSide();

                return;
            }

            if (p_lastMovedUp) return;
            m_animator.CrossFade(s_chew_hash, 0f, 0);
        }
    }
}

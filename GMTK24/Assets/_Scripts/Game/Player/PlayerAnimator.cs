using com.absence.attributes;
using com.game.entities;
using com.game.input;
using System.Collections.Generic;
using System.Text;

//using DG.Tweening;
using UnityEngine;

namespace com.game.player
{
    public class PlayerAnimator : MonoBehaviour
    {
        static readonly int s_hash_eat = Animator.StringToHash("player_eat");

        static readonly string s_player = "player";

        static readonly string s_idle = "_idle";
        static readonly string s_walk = "_walk";

        static readonly string s_chew = "_chew";

        static readonly string s_up = "_up";
        static readonly string s_down = "_down";
        static readonly string s_side = "_side";

        Dictionary<string, int> m_hashes;

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

        Vector2 m_lastMovementInput;
        Vector2 m_currentMovementInput;
        bool m_handledByCombatSystem = false;
        bool m_isChewing = false;
        bool m_inEatAnimation = false;

        int m_lastHash;

        string m_state;
        string m_direction;
        string m_suffix;

        private void Start()
        {
            InputEventChannel.Player.OnMovementInput += OnMove;
            PlayerEventChannel.Eating.OnStartEating += OnStartEating;
            PlayerEventChannel.Eating.OnStopChewing += OnStopChewing;
            PlayerEventChannel.Eating.OnFormChange += OnFormChange;

            m_isFacingRight = true;
            m_hashes = new();
        }

        private void LateUpdate()
        {
            Cleanup();
        }

        void Cleanup()
        {
            int hash = GenerateHash();
            if (hash != m_lastHash && (!m_handledByCombatSystem) && (!m_inEatAnimation)) RefreshAnimations(true);
        }

        private void OnFormChange(EntityForm form1, EntityForm form2)
        {
            ChangeController(form2.OverrideController);
            RefreshAnimations();
        }

        private void OnStartEating()
        {
            m_isChewing = true;
            m_inEatAnimation = true;

            m_animator.CrossFade(s_hash_eat, 0f, 0);
        }

        private void OnStopChewing()
        {
            m_isChewing = false;
        }

        private void OnMove(Vector2 vector)
        {
            if (m_debugMode) Debug.Log($"move: ({vector.x}, {vector.y})");

            m_currentMovementInput = vector;

            RefreshAnimations(!m_handledByCombatSystem);
            HandleFlip();

            m_lastMovementInput = m_currentMovementInput;
        }

        void HandleMovement()
        {
            m_state = s_walk;

            float currentY = m_currentMovementInput.y;
            float lastY = m_lastMovementInput.y;

            float currentX = m_currentMovementInput.x;
            float lastX = m_lastMovementInput.x;

            if (currentY == 0f && lastY != currentY) m_direction = s_side;
            else if (currentY > lastY && lastY == 0f) m_direction = s_up;
            else if (currentY < lastY && lastY == 0f) m_direction = s_down;

            if (currentX != lastX && lastX == 0f) m_direction = s_side;
            else if (currentX == 0f && lastX != currentX)
            {
                if (currentY > lastY) m_direction = s_up;
                else if (currentY < lastY) m_direction = s_down;
            }
        }

        void HandleStop()
        {
            m_state = s_idle;
        }

        void CheckChewing()
        {
            if (m_isChewing) m_suffix = s_chew;
            else m_suffix = string.Empty;
        }

        int GenerateHash()
        {
            StringBuilder sb = new(s_player);
            sb.Append(m_state);
            sb.Append(m_direction);
            sb.Append(m_suffix);

            string result = sb.ToString();

            if (!m_hashes.TryGetValue(result, out int hash))
            {
                hash = Animator.StringToHash(result);
                m_hashes.Add(result, hash);
            }

            return hash;
        }

        void RefreshAnimations(bool fade = true)
        {
            if (m_currentMovementInput != Vector2.zero) HandleMovement();
            else HandleStop();

            CheckChewing();

            m_lastHash = GenerateHash();
            if (fade) m_animator.CrossFade(m_lastHash, 0f, 0);
        }

        void HandleFlip()
        {
            if (m_currentMovementInput.x > 0f && !m_isFacingRight) Flip();
            else if (m_currentMovementInput.x < 0f && m_isFacingRight) Flip();
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
            PlayerEventChannel.Eating.CommitEatStop();
        }

        public void NotifyCombatAnimationEnded()
        {
            m_handledByCombatSystem = false;
        }

        public void ChangeController(AnimatorOverrideController controller)
        {
            m_animator.runtimeAnimatorController = controller;
        }
    }
}

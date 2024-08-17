using com.game.input;
using com.game.misc;
using System.Collections.Generic;
using UnityEngine;

namespace com.game.player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private enum InputLogic
        {
            KeyboardAndMouse = 0,
            Gamepad = 1,
        }

        const string k_up_suffix = "_up";
        const string k_down_suffix = "_down";
        const string k_side_suffix = "_side";

        const string k_idle = "idle";
        const string k_walk = "walk";

        static readonly string s_idle_up = k_idle + k_up_suffix;
        static readonly string s_idle_down = k_idle + k_down_suffix;
        static readonly string s_idle_side = k_idle + k_side_suffix;

        static readonly string s_walk_up = k_walk + k_up_suffix;
        static readonly string s_walk_down = k_walk + k_down_suffix;
        static readonly string s_walk_side = k_walk + k_side_suffix;

        static readonly Dictionary<string, int> s_hashes = new Dictionary<string, int>()
        {
            { s_idle_up, Animator.StringToHash(s_idle_up) },
            { s_idle_down, Animator.StringToHash(s_idle_down) },
            { s_idle_side, Animator.StringToHash(s_idle_side) },
            { s_walk_up, Animator.StringToHash(s_walk_up) },
            { s_walk_down, Animator.StringToHash(s_walk_down) },
            { s_walk_side, Animator.StringToHash(s_walk_side) },
        };

        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private Animator m_animator;
        [SerializeField] private SpriteRenderer m_renderer;

        Vector2 m_previousMovement = Vector2.zero;
        Vector2 m_currentMovement;
        Vector2 m_rasterizedGamepadDirection = Vector2.zero;
        string m_moveState = k_idle;
        string m_directionState = k_side_suffix;

        bool m_facingRight = true;
        InputLogic m_inputLogic = InputLogic.KeyboardAndMouse;

        private void Awake()
        {
            InputEventChannel.Player.OnMovementInput += OnMove;
        }

        private void Update()
        {
            Cleanup();
        }

        private void OnMove(Vector2 vector)
        {
            if (m_debugMode) Debug.Log($"move: ({vector.x}, {vector.y})");

            m_currentMovement = vector;

            CacheVariables();
            SetMoveState();
            SetDirectionState();
            HandleFlip();
            Crossfade();

            m_previousMovement = m_currentMovement;
        }

        void Cleanup()
        {
            if (InputManager.Instance == null) return;

            PlayerInputActions inputActions = InputManager.Instance.InputActions;
            Vector2 currentMovementVector = inputActions.Player.Move.ReadValue<Vector2>();

            if(currentMovementVector.Equals(Vector2.zero))
            {
                HandleStop();
            }
        }

        void CacheVariables()
        {
            if (InputManager.Instance.IsKeyboardAndMouse) m_inputLogic = InputLogic.KeyboardAndMouse;
            else if (InputManager.Instance.IsGamepad) m_inputLogic = InputLogic.Gamepad;
        }
        void Crossfade()
        {
            int targetHash = s_hashes[m_moveState + m_directionState];
            m_animator.CrossFade(targetHash, 0f, 0);
        }
        void SetMoveState()
        {
            if (m_currentMovement == Vector2.zero) m_moveState = k_idle;
            else m_moveState = k_walk;
        }
        void SetDirectionState()
        {
            if (m_inputLogic == InputLogic.KeyboardAndMouse) SetDirectionState_Keyboard();
            else if (m_inputLogic == InputLogic.Gamepad) SetDirectionState_Gamepad();

            return;

            void SetDirectionState_Keyboard()
            {
                if (m_currentMovement.y > 0f && m_previousMovement.y == 0f) m_directionState = k_up_suffix;
                else if (m_currentMovement.y < 0f && m_previousMovement.y == 0f) m_directionState = k_down_suffix;
                else if (m_currentMovement.y == 0f && m_currentMovement.x != 0f) m_directionState = k_side_suffix;

                if (m_currentMovement.x != 0f && m_previousMovement.x == 0f) m_directionState = k_side_suffix;
                else if (m_currentMovement.x == 0f && m_currentMovement.y > 0f) m_directionState = k_up_suffix;
                else if (m_currentMovement.x == 0f && m_currentMovement.y < 0f) m_directionState = k_down_suffix;
            }

            void SetDirectionState_Gamepad()
            {
                if (m_currentMovement.Equals(Vector2.zero)) return;

                float angle = Vector2.SignedAngle(Vector2.right, m_currentMovement);

                // right
                if (angle > -45f && angle < 45f)
                {
                    m_directionState = k_side_suffix;
                    m_rasterizedGamepadDirection = Vector2.right;
                }
                // up
                else if (angle >= 45f && angle <= 135f)
                {
                    m_directionState = k_up_suffix;
                    m_rasterizedGamepadDirection = Vector2.up;
                }
                // down
                else if (angle <= -45f && angle >= -135f)
                {
                    m_directionState = k_down_suffix;
                    m_rasterizedGamepadDirection = Vector2.down;
                }
                // left
                else
                {
                    m_directionState = k_side_suffix;
                    m_rasterizedGamepadDirection = Vector2.left;
                }
            }
        }

        void HandleFlip()
        {
            if (m_inputLogic == InputLogic.KeyboardAndMouse) HandleFlip_Keyboard();
            else if (m_inputLogic == InputLogic.Gamepad) HandleFlip_Gamepad();

            return;

            void HandleFlip_Keyboard()
            {
                if (m_currentMovement.x > 0f && !m_facingRight) Flip();
                else if (m_currentMovement.x < 0f && m_facingRight) Flip();
            }

            void HandleFlip_Gamepad()
            {
                if (m_rasterizedGamepadDirection.Equals(Vector2.right) && !m_facingRight) Flip();
                else if (m_rasterizedGamepadDirection.Equals(Vector2.left) && m_facingRight) Flip();
            }
        }
        void HandleStop()
        {
            m_moveState = k_idle;
            Crossfade();
        }

        void Flip()
        {
            m_renderer.flipX = !m_renderer.flipX;
            m_facingRight = !m_facingRight;
        }
    }
}

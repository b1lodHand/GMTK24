using com.absence.timersystem;
using com.game.abilities;
using com.game.input;
using com.game.player;
using UnityEngine;

namespace com.game
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private Combo m_defaultCombo;

        [SerializeField] [Min(0f)] private float m_attackCooldown = 0.2f;

        Timer m_cooldownTimer;

        public bool CanPerform => (!IsAttacking) && 
            (!Player.Instance.IsEating) && 
            (!Player.Instance.InDialogue) && 
            (!InputManager.Instance.InUI) && 
            (!InputManager.Instance.InRebindProcess);

        bool m_isAttacking = false;
        public bool IsAttacking => m_isAttacking;

        private void Start()
        {
            InputEventChannel.Player.OnAttackInput += Attack;
        }

        public void Attack()
        {
            if (!CanPerform) return;
            if (m_cooldownTimer != null) return;

            bool usedCombo = Player.Instance.Hub.Abilities.UseCombo(m_defaultCombo, out Ability usedAbility);
            if (!usedCombo) return;

            m_isAttacking = true;

            if (usedAbility.IsEnded) m_isAttacking = false;
            else usedAbility.OnEnd += () => m_isAttacking = false;

            SetupCooldownTimer();
        }

        void SetupCooldownTimer()
        {
            m_cooldownTimer = Timer.Create(m_attackCooldown, null, s =>
            {
                m_cooldownTimer = null;
            });

            m_cooldownTimer.Start();
        }

        private void OnGUI()
        {
            if (!m_debugMode) return;

            if (GUILayout.Button("Attack"))
            {
                Attack();
            }
        }
    }
}

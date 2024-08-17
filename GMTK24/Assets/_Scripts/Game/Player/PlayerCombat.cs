using com.absence.timersystem;
using com.game.input;
using com.game.player;
using UnityEngine;

namespace com.game
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private Combo m_defaultCombo;

        [SerializeField] private float m_attackCooldown = 0.2f;

        Timer m_cooldownTimer;

        private void Start()
        {
            InputEventChannel.Player.OnAttackInput += Attack;
        }

        public void Attack()
        {
            if (m_cooldownTimer != null) return;

            bool usedCombo = Player.Instance.Hub.Abilities.User.UseCombo(m_defaultCombo);

            if (!usedCombo) return;

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

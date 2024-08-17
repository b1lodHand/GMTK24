using com.game.player;
using UnityEngine;

namespace com.game
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private Combo m_defaultCombo;

        int comboIndex;

        private void Start()
        {
            comboIndex = -1;
        }

        public void Attack()
        {
            if (Player.Instance.Hub.Abilities.User.ActiveAbility != null) return;

            comboIndex++;
            if (comboIndex == m_defaultCombo.AbilityCount) comboIndex = 0;

            Player.Instance.Hub.Abilities.User.UseAbility(m_defaultCombo.GetAbilityAt(comboIndex));
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

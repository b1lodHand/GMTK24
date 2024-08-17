using com.game.player;
using UnityEngine;

namespace com.game
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private Combo m_defaultCombo;

        public void Attack()
        {
            Player.Instance.Hub.Abilities.User.UseCombo(m_defaultCombo);
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

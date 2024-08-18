using com.game.abilities;
using UnityEngine;

namespace com.game.player
{
    public class PlayerAbilities : MonoBehaviour
    {
        [SerializeField] private AbilityUser m_abilityUser;
        public AbilityUser User => m_abilityUser;

        public bool UseAbility(Ability ability) => User.UseAbility(ability);
        public bool UseCombo(Combo combo) => User.UseCombo(combo);
    }
}

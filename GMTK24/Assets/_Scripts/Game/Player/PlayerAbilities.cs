using com.game.abilities;
using UnityEngine;

namespace com.game.player
{
    public class PlayerAbilities : MonoBehaviour
    {
        [SerializeField] private AbilityUser m_abilityUser;
        public AbilityUser User => m_abilityUser;

        public bool UseAbility(Ability ability) => User.UseAbility(ability, false, out _);
        public bool UseCombo(Combo combo) => User.UseCombo(combo, out _);

        public bool UseAbility(Ability ability, out Ability usedAbility) => User.UseAbility(ability, false, out usedAbility);
        public bool UseCombo(Combo combo, out Ability usedAbility) => User.UseCombo(combo, out usedAbility);
    }
}

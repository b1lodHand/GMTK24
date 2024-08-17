using com.game.abilities;
using UnityEngine;

namespace com.game
{
    public class PlayerAbilities : MonoBehaviour
    {
        [SerializeField] private AbilityUser m_abilityUser;
        public AbilityUser User => m_abilityUser;
    }
}

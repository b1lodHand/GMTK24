using com.game.abilities;
using com.game.player;
using UnityEngine;

namespace com.game
{
    public class PlayerAbilities : MonoBehaviour
    {
        [SerializeField] private AbilityUser m_abilityUser;
        public AbilityUser User => m_abilityUser;

        private void Start()
        {
            AbilityUserData data = new();
            data.Person = Player.Instance.Person;

            m_abilityUser.SetData(data);
        }
    }
}

using com.absence.personsystem;
using com.game.player;
using UnityEngine;

namespace com.game.abilities
{
    [System.Serializable]
    public class AbilityUserData
    {
        [field: SerializeField] public Person Person { get; set; }

        public AbilityUserData() 
        { 
            Person = null;
        }

        public bool IsPlayer()
        {
            if (Person == null) return false;
            return Person.Equals(Player.Instance.Person);
        }
    }
}

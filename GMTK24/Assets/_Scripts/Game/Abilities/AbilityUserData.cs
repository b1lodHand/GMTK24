using com.absence.personsystem;
using com.game.entities;
using com.game.player;

namespace com.game.abilities
{
    [System.Serializable]
    public class AbilityUserData
    {
        public Entity Entity;

        public bool IsPlayer()
        {
            if (Entity == null) return false;

            Person person = Entity.Person;

            if (person == null) return false;
            if (person != Player.Instance.Person) return false;

            return true;
        }
    }
}

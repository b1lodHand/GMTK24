using com.absence.personsystem;
using com.game.entities;

namespace com.game.interactables
{
    [System.Serializable]
    public class InteractorData
    {
        public Entity Entity;

        public bool IsPlayer()
        {
            if (Entity == null) return false;

            Person person = Entity.Person;

            if (person == null) return false;
            if (person != player.Player.Instance.Person) return false;

            return true;
        }
    }
}

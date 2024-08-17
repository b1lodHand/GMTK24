using com.absence.personsystem;

namespace com.game.interactables
{
    [System.Serializable]
    public class InteractorData
    {
        public Person Person;

        public bool IsPlayer()
        {
            if (Person == null) return false;
            if (Person != player.Player.Instance.Person) return false;

            return true;
        }
    }
}

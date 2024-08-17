using com.absence.personsystem;
using com.game.scaling.generics;

namespace com.game.interactables
{
    [System.Serializable]
    public class InteractorData
    {
        public Person Person;
        public EntityScaler Scaler; 

        public bool IsPlayer()
        {
            if (Person == null) return false;
            if (Person != player.Player.Instance.Person) return false;

            return true;
        }
    }
}

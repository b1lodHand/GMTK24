using com.absence.personsystem;
using UnityEngine;

namespace com.game.entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private Person m_person;
        [SerializeField] private EntityComponentHub m_componentHub;

        public Person Person => m_person;
        public EntityComponentHub Hub => m_componentHub;
    }
}

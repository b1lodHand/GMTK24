using com.absence.variablesystem;
using UnityEngine;

namespace com.game.entities
{
    public class EntityStats : MonoBehaviour
    {
        [SerializeField] private Variable_Float m_health = new("Health", 0f);
        public Variable_Float Health => m_health;
    }
}

using UnityEngine;

namespace com.game.entities
{
    public class EntityComponentHub : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D m_rigidbody;
        [SerializeField] private EntityStats m_stats;
        [SerializeField] private EntityEatingSystem m_eatingSystem;

        public Rigidbody2D Rigidbody => m_rigidbody;
        public EntityStats Stats => m_stats;
        public EntityEatingSystem EatingSystem => m_eatingSystem;
    }
}

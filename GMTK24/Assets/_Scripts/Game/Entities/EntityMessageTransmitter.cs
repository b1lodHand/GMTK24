using UnityEngine;

namespace com.game.entities
{
    public class EntityMessageTransmitter : MonoBehaviour
    {
        [SerializeField] private Entity m_entity;
        public Entity Entity => m_entity;
    }
}

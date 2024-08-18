using com.absence.attributes;
using com.game.entities;
using UnityEngine;

namespace com.game
{
    [RequireComponent(typeof(Entity))]
    public class SpawnObjectOnDeath : MonoBehaviour
    {
        [SerializeField, Readonly] private Entity m_entity;



        private void Reset()
        {
            m_entity = GetComponent<Entity>();
        }
    }
}

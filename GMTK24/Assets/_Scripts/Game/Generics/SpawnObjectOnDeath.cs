using com.absence.attributes;
using com.game.entities;
using System.Collections.Generic;
using UnityEngine;

namespace com.game
{
    [RequireComponent(typeof(Entity))]
    public class SpawnObjectOnDeath : MonoBehaviour
    {
        [SerializeField, Readonly] private Entity m_entity;
        [SerializeField] private List<GameObject> m_objectsToSpawn = new();

        private void Start()
        {
            m_entity.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            m_objectsToSpawn.ForEach(obj => Instantiate(obj));
        }

        private void Reset()
        {
            m_entity = GetComponent<Entity>();
        }
    }
}

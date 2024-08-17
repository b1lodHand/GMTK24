using com.absence.attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.generics
{
    public class CircleOverlapChecker : MonoBehaviour
    {
        [SerializeField] [Min(0f)] private float m_radius;
        [SerializeField] private LayerMask m_layerMask;
        [SerializeField, Readonly] private bool m_enabled = false;

        Collider2D[] m_foundColliders;
        bool m_initialized = false;
        int m_maxColliderCount = 0;

        private void FixedUpdate()
        {
            if (!m_initialized) return;
            if (!m_enabled) return;

            Physics2D.OverlapCircleNonAlloc(transform.position, m_radius, m_foundColliders, m_layerMask);
        }

        public void Initialize(int maxCollidersToDetect)
        {
            m_maxColliderCount = maxCollidersToDetect;
            m_initialized = true;

            m_foundColliders = new Collider2D[m_maxColliderCount];
        }

        public void Enable()
        {
            if (!m_initialized) return;

            m_enabled = true;
        }

        public void Disable()
        {
            if (!m_initialized) return;

            m_enabled = false;
            ClearResults();
        }

        public List<T> GetResults<T>() where T : UnityEngine.Object
        {
            return m_foundColliders.ToList().ConvertAll(collider2D => collider2D.attachedRigidbody.gameObject.GetComponent<T>());    
        }

        private void ClearResults()
        {
            if (!m_initialized) return;

            for (int i = 0; i < m_foundColliders.Length; i++)
            {
                m_foundColliders[i] = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_radius);
        }
    }
}

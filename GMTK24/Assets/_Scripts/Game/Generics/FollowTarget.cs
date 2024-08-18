using com.absence.attributes;
using UnityEngine;

namespace com.game
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private bool m_keepStartingOffset = true;
        [SerializeField, HideIf(nameof(m_keepStartingOffset))] private Vector3 m_defaultOffset;
        [SerializeField] private Transform m_target;

        Vector3 m_offset = Vector3.zero;

        private void Start()
        {
            if (m_keepStartingOffset) m_offset = transform.position - m_target.position;
            else m_offset = m_defaultOffset;
        }

        private void Update()
        {
            transform.position = m_target.position + m_offset;
        }
    }
}

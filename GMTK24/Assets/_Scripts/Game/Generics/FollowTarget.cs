using UnityEngine;

namespace com.game
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private bool m_keepStartingOffset = true;
        [SerializeField] private Transform m_target;

        Vector2 m_offset = Vector2.zero;

        private void Start()
        {
            if (m_keepStartingOffset) m_offset = transform.position - m_target.position;
        }

        private void Update()
        {
            transform.position = (Vector2)m_target.position + m_offset;
        }
    }
}

using UnityEngine;

namespace com.game.ai
{
    public class ContextSteering : MonoBehaviour
    {
        [Header("Initial Fields")]
        [SerializeField] private Rigidbody2D m_rb;
        [SerializeField, Min(8)] private int m_resolution = 8;
        [SerializeField] private float m_speedMultiplier = 1f;

        //[Header("Spread Settings")]

        //[SerializeField, Min(0.01f)] private float m_spreadCoefficient = 0.01f;
        //[SerializeField, Min(0.01f)] private float m_spreadBottomline = 0.01f;

        [Header("Interest Factors")]
        
        [SerializeField] private LayerMask m_interestMask;
        [SerializeField, Min(0.001f)] private float m_maxInterestDistance = 0.001f;

        [Header("Danger Factors")]

        [SerializeField] private LayerMask m_dangerMask;
        [SerializeField, Min(0f)] private float m_maxDangerDistance = 0f;

        [Header("Editor Settings")]

        [SerializeField] private bool m_showRawData = false;

        public Vector2 DesiredDirection => m_desiredDirection;

        private float[] m_interests;
        private float[] m_dangers;
        private float[] m_result;
        private Vector2[] m_cachedDirections;

        private Vector2 m_lastDesiredDirection;
        private Vector2 m_desiredDirection;

        private void Awake()
        {
            ResetAll();
        }

        private void FixedUpdate()
        {
            m_lastDesiredDirection = m_desiredDirection;

            FindAllDangers();
            FindAllInterests();
            CalculateResult();
            CalculateDesiredDirection();
        }

        void CalculateDesiredDirection()
        {
            Vector2 sum = Vector2.zero;
            for (int i = 0; i < m_resolution; i++)
            {
                float result = m_result[i];
                Vector2 resultDirection = m_cachedDirections[i];

                sum += resultDirection * result;
            }

            Vector2 avg = sum / m_resolution;
            float dotFactor = (Vector2.Dot(m_lastDesiredDirection, avg) / 2) + 0.5f;

            m_desiredDirection = avg * dotFactor * m_speedMultiplier;
        }

        void ResetAll()
        {
            m_interests = new float[m_resolution];
            m_dangers = new float[m_resolution];
            m_result = new float[m_resolution];
            m_cachedDirections = new Vector2[m_resolution];

            for (int i = 0; i < m_resolution; i++)
            {
                m_interests[i] = 0f;
            }
            for (int i = 0; i < m_resolution; i++)
            {
                m_dangers[i] = 0f;
            }
            for (int i = 0; i < m_resolution; i++)
            {
                m_result[i] = 0f;
            }

            CacheDirections();
        }
        void FindAllDangers()
        {
            FindAll(ref m_dangers, m_dangerMask, m_maxDangerDistance);
        }
        void FindAllInterests()
        {
            FindAll(ref m_interests, m_interestMask, m_maxInterestDistance);
        }
        void CalculateResult()
        {
            for (int i = 0; i < m_resolution; i++)
            {
                float interest = m_interests[i];
                float danger = m_dangers[i];

                float sum = interest - danger;
                m_result[i] = sum;
            }
        }
        void CacheDirections()
        {
            float stepAngle = 360 / m_resolution;

            for (int i = 0; i < m_resolution; i++)
            {
                float angle = stepAngle * i;
                m_cachedDirections[i] = GetDirectionFromAngle(angle);
            }
        }
        void FindAll(ref float[] array, LayerMask withMask, float maxDistance)
        {
            Physics2D.queriesHitTriggers = false;

            Vector2 position = transform.position;
            Vector2 velocity = m_rb.velocity;
            Vector2 moveDirection = velocity.normalized;

            for (int i = 0; i < m_resolution; i++)
            {
                Vector2 direction = m_cachedDirections[i];

                RaycastHit2D hit = Physics2D.Raycast(position, direction, maxDistance, withMask);

                array[i] = 0f;

                if (!hit)
                {
                    //slot.Weight += m_spreadBottomline;
                    continue;
                }

                float distanceFactor = 1f - (hit.distance / maxDistance);
                float dotFactor = (Vector2.Dot(moveDirection, direction) / 2) + 0.5f;
                float weight = (distanceFactor + dotFactor) / 2;

                //float spread = (m_spreadCoefficient * weight) + m_spreadBottomline;

                array[i] += weight;
            }
        }
        Vector2 GetDirectionFromAngle(float angleInDegrees)
        {
            return new Vector2(-Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        private void OnDrawGizmosSelected()
        {
            Vector2 position = transform.position;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, m_maxDangerDistance);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(position, m_maxInterestDistance);

            if (!Application.isPlaying) return;

            if (m_showRawData)
            {
                DrawRawData();
                return;
            }

            DrawBakedData();

            return;

            void DrawBakedData()
            {
                Gizmos.color = Color.yellow;
                for (int i = 0; i < m_resolution; i++)
                {
                    float result = m_result[i];

                    Vector2 direction = m_cachedDirections[i];
                    Vector2 endPoint = position + (direction * result);

                    Gizmos.DrawLine(position, endPoint);
                }

                Gizmos.color = Color.magenta;
                Vector2 realEndPoint = position + m_desiredDirection;
                Gizmos.DrawLine(position, realEndPoint);
            }

            void DrawRawData()
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < m_resolution; i++)
                {
                    float interest = m_interests[i];

                    Vector2 direction = m_cachedDirections[i];
                    Vector2 endPoint = position + (direction * interest);

                    Gizmos.DrawLine(position, endPoint);
                }

                Gizmos.color = Color.red;
                for (int i = 0; i < m_resolution; i++)
                {
                    float danger = m_dangers[i];

                    Vector2 direction = m_cachedDirections[i];
                    Vector2 endPoint = position + (direction * danger);

                    Gizmos.DrawLine(position, endPoint);
                }
            }
        }
    }
}

using com.absence.attributes;
using UnityEngine;

namespace com.game.scaling.generics
{
    public class EntityScaler : MonoBehaviour
    {
        [SerializeField] private bool m_debugMode = false;

        [SerializeField] private Rigidbody2D m_rigidbody;
        [SerializeField] private Transform m_body;
        [SerializeField] private Transform m_collision;

        [SerializeField] private float m_bodyScaleFactor = 1f;
        [SerializeField] private float m_massDeltaFactor = 1f;
        [SerializeField, Readonly] private float m_totalScaleDifference = 0f;
        [SerializeField] private Vector2 m_scaleBounds = new Vector2(-100f, 100f);

        Vector2 m_bodyCollisionRatio;

        private void Start()
        {
            CacheRatio();
        }

        public bool ScaleUp(float massGain)
        {
            float newScale = m_totalScaleDifference + massGain;
            if (newScale > m_scaleBounds.y) return false;

            Calculate(massGain, out float newMass, out Vector2 newBodySize, out Vector2 newCollisionSize);
            ApplyFields(newMass, newBodySize, newCollisionSize);
            m_totalScaleDifference = newScale;

            return true;
        }

        public bool ScaleDown(float massLoss)
        {
            float newScale = m_totalScaleDifference - massLoss;
            if (newScale < m_scaleBounds.x) return false;

            Calculate(-massLoss, out float newMass, out Vector2 newBodySize, out Vector2 newCollisionSize);
            ApplyFields(newMass, newBodySize, newCollisionSize);
            m_totalScaleDifference = newScale;

            return true;
        }

        void ApplyFields(float newMass, Vector2 newBodySize, Vector2 newCollisionSize)
        {
            m_rigidbody.mass = newMass;

            m_body.localScale = newBodySize;
            //m_body.ScaleAround(m_body.position, newBodySize);

            m_collision.localScale = newCollisionSize;
            //m_collision.ScaleAround(m_collision.position, newCollisionSize);
        }

        void Calculate(float massDifference, out float newMass, out Vector2 newBodySize, out Vector2 newCollisionSize)
        {
            float mass = m_rigidbody.mass;
            newMass = mass + (massDifference * m_massDeltaFactor);

            Vector2 bodySize = m_body.transform.localScale;
            float bodySizeDiff = massDifference * m_bodyScaleFactor;
            float bodySizeRatio = bodySize.y / bodySize.x;

            bodySize.x += bodySizeDiff;
            bodySize.y += bodySizeDiff * bodySizeRatio;

            newBodySize = bodySize;

            newCollisionSize = newBodySize  / m_bodyCollisionRatio;
        }

        void CacheRatio()
        {
            float x = m_body.localScale.x / m_collision.localScale.x;
            float y = m_body.localScale.y / m_collision.localScale.y;
            m_bodyCollisionRatio = new(x, y);
        }

        private void OnGUI()
        {
            if (!m_debugMode) return;

            if (GUILayout.Button("Gain Mass"))
            {
                ScaleUp(1f);
            }

            if (GUILayout.Button("Lose Mass"))
            {
                ScaleDown(1f);
            }
        }
    }
}

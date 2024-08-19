using com.absence.attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.scaling.generics
{
    public class EntityScaler : MonoBehaviour
    {
        [SerializeField] private bool m_debugMode = false;

        [Header("Initial Fields")]
        [SerializeField] private Vector2 m_scaleBounds = new Vector2(-100f, 100f);

        [Header("Real Mass")]

        [SerializeField] private bool m_useRealMass = true;
        [SerializeField, ShowIf(nameof(m_useRealMass))] private Rigidbody2D m_rb;
        [SerializeField, ShowIf(nameof(m_useRealMass)), Min(0.001f)] private float m_rigidbodyMassFactor = 1f;

        [Space(10)]

        [Header("Additional Settings")]

        [SerializeField] private List<TransformEntry> m_scaleableObjects = new();

        [Header("Runtime")]

        [SerializeField, Readonly] private float m_totalScaleDifference = 0f;

        public float TotalScaleDifference => m_totalScaleDifference;

        public event Action<float> OnScaleDifference = null;

        private void Start()
        {
            CacheVariables();
            m_scaleableObjects.ForEach(obj => obj.Initialize());
        }

        public bool ScaleUp(float massGain)
        {
            float newScale = m_totalScaleDifference + massGain;
            if (newScale > m_scaleBounds.y) return false;

            Apply(massGain);
            m_totalScaleDifference = newScale;

            OnScaleDifference?.Invoke(massGain);
            return true;
        }

        public bool ScaleDown(float massLoss)
        {
            float newScale = m_totalScaleDifference - massLoss;
            if (newScale < m_scaleBounds.x) return false;

            Apply(-massLoss);
            m_totalScaleDifference = newScale;

            OnScaleDifference?.Invoke(-massLoss);
            return true;
        }

        void CacheVariables()
        {
        }

        void Apply(float scaleDifference)
        {
            ApplyRealMass(scaleDifference);
            RefreshAll(scaleDifference);
        }

        void ApplyRealMass(float scaleDifference)
        {
            if (m_rb == null) return;

            float mass = m_rb.mass;
            mass += scaleDifference * m_rigidbodyMassFactor;

            m_rb.mass = mass;
        }
        void RefreshAll(float scaleDifference)
        {
            List<TransformEntry> temp = m_scaleableObjects.OrderBy(entry => (int)entry.ScaleType).ToList();
            temp.ForEach(entry => entry.Refresh(scaleDifference));
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

        [System.Serializable]
        public class TransformEntry
        {
            public enum ScalingType
            {
                Individual = 0,
                Ratio = 1,
            }

            [SerializeField] private Transform m_transform = null;
            [SerializeField] private ScalingType m_scaleType = ScalingType.Individual;
            
            [SerializeField, ShowIf(nameof(m_scaleType), ScalingType.Individual), Min(0.001f)] 
            private float m_scaleFactor = 1f;

            [SerializeField, ShowIf(nameof(m_scaleType), ScalingType.Ratio)]
            private Transform m_targetForRatio = null;

            Vector2 m_ratio = Vector2.one;

            public ScalingType ScaleType => m_scaleType;

            public void Initialize()
            {
                switch (m_scaleType)
                {
                    case ScalingType.Individual:
                        break;

                    case ScalingType.Ratio:
                        m_ratio = CalculateRatio();
                        break;

                    default:
                        throw new System.Exception("something went wrong on an entity scaler.");
                }
            }
            public void Refresh(float scaleDifference)
            {
                switch (m_scaleType)
                {
                    case ScalingType.Individual:
                        RefreshIndividually(scaleDifference);
                        break;

                    case ScalingType.Ratio:
                        RefreshByRatio();
                        break;

                    default:
                        throw new Exception("something went wrong on an entity scaler.");
                }
            }

            void RefreshIndividually(float scaleDifference)
            {
                Vector2 scale = m_transform.localScale;
                float initialRatio = scale.y / scale.x;
                float step = scaleDifference * m_scaleFactor;

                scale.x += step;
                scale.y += step * initialRatio;

                m_transform.localScale = scale;
            }
            void RefreshByRatio()
            {
                m_transform.localScale = m_targetForRatio.localScale * m_ratio;
            }
            Vector2 CalculateRatio()
            {
                float x = m_transform.localScale.x / m_targetForRatio.localScale.x;
                float y = m_transform.localScale.y / m_targetForRatio.localScale.y;
                return new Vector2(x, y);
            }
        }
    }
}

using com.absence.attributes;
using com.game.scaling.generics;
using UnityEngine;

namespace com.game.generics.physics
{
    public class ForceFieldResistance : MonoBehaviour
    {
        [SerializeField] private float m_resistance = 0f;
        public float Resistance => m_resistance;

        [SerializeField] private bool m_affectedByScaling = false;
        [SerializeField, ShowIf(nameof(m_affectedByScaling))] private EntityScaler m_scaler;
        [SerializeField, ShowIf(nameof(m_affectedByScaling))] private float m_scaleFactor = 1f;

        private void Start()
        {
            if (m_affectedByScaling) m_scaler.OnScaleDifference += OnScale;
        }

        private void OnScale(float difference)
        {
            m_resistance += difference * m_scaleFactor;
        }
    }
}

using com.absence.attributes;
using com.game.scaling.generics;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.entities
{
    public class EntityFormSystem : MonoBehaviour
    {
        [SerializeField] protected EntityScaler m_scaler;
        [SerializeField] protected List<FormEntry> m_forms = new();
        [SerializeField, Readonly] protected EntityForm m_currentForm;

        public event Action<EntityForm, EntityForm> OnFormChange;

        private void Start()
        {
            m_scaler.OnScaleDifference += OnScaleDifference;
            if (m_forms.Count > 0) ApplyForm(m_forms.FirstOrDefault().FormAsset);
        }

        private void OnScaleDifference(float difference)
        {
            float currentScale = m_scaler.TotalScaleDifference;
            float previousScale = currentScale - difference;

            EntityForm targetForm = GetFormByScalingEvent(previousScale, currentScale);
            if (targetForm == null) return;

            ApplyForm(targetForm);
        }

        protected virtual void ApplyForm(EntityForm targetForm)
        {
            OnFormChange?.Invoke(m_currentForm, targetForm);
            m_currentForm = targetForm;
        }

        EntityForm GetFormByScalingEvent(float previousScale, float currentScale)
        {
            EntityForm previousForm = GetFormByScale(previousScale);
            EntityForm currentForm = GetFormByScale(currentScale);

            if (previousForm == null) throw new System.Exception("something went wrong with the form system.");
            if (currentForm == null) return null;

            if (previousScale.Equals(currentForm)) return null;

            return currentForm;
        }

        EntityForm GetFormByScale(float scale)
        {
            return m_forms.Last(entry => entry.ScaleBottomline <= scale).FormAsset;
        }

        [System.Serializable]
        public class FormEntry
        {
            [SerializeField] private EntityForm m_formAsset;
            [SerializeField] private float m_scaleBottomline;

            public EntityForm FormAsset => m_formAsset;
            public float ScaleBottomline => m_scaleBottomline;
        }
    }
}

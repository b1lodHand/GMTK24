using com.absence.attributes;
using com.absence.timersystem;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace com.game.popups
{
    public class Popup : MonoBehaviour
    {
        [Header("Initial Fields")]

        [SerializeField] protected TMP_Text m_text;
        [SerializeField, Min(0f)] protected float m_lifeTime = 0.5f;

        [Header("End Animation")]

        [SerializeField, Min(0f)] protected float m_endAnimationDuration = 0.3f;

        [SerializeField] protected bool m_moveUpOnEnd = false;
        [SerializeField, ShowIf(nameof(m_moveUpOnEnd)), Min(0.001f)] protected float m_moveUpAmplitude = 1f;

        [SerializeField] protected bool m_fadeOutOnEnd = true;

        protected Timer m_lifeTimer;

        public virtual void Initialize(string text)
        {
            m_text.text = text;
            SetupTimer();
        }

        void SetupTimer()
        {
            if (m_lifeTimer != null) m_lifeTimer.Fail();
            if (m_lifeTime == 0f)
            {
                StartEndAnimation();
                return;
            }

            m_lifeTimer = Timer.Create(m_lifeTime, null, s =>
            {
                m_lifeTimer = null;
                StartEndAnimation();
            });

            m_lifeTimer.Start();
        }

        void StartEndAnimation()
        {
            if (m_endAnimationDuration == 0f) return;

            transform.DOMoveY(transform.position.y + m_moveUpAmplitude, m_endAnimationDuration);
            m_text.DOFade(0f, m_endAnimationDuration).OnComplete(() => Destroy(gameObject));
        }
    }
}

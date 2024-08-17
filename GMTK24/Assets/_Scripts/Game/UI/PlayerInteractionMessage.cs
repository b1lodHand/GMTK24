using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;

namespace com.game.player.ui
{
    public class PlayerInteractionMessage : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_text;
        [SerializeField] private CanvasGroup m_group;
        [SerializeField] private float m_fadeDuration = 0.1f;

        TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> m_tweener = null;

        public void SetMessage(string message)
        {
            m_text.text = message;
        }

        public void FadeTo(Vector2 targetPosition)
        {
            SnapTo(targetPosition);

            //if (m_tweener != null) m_tweener.Kill(false);

            //m_tweener = GenerateFadeOutTweener();
            //m_tweener.OnComplete(() =>
            //{
            //    SnapTo(targetPosition);
            //    m_tweener = GenerateFadeInTweener();
            //    m_tweener.OnComplete(() => m_tweener = null);
            //});
        }
        
        public void SnapTo(Vector2 targetPosition)
        {
            if (targetPosition == (Vector2)transform.position) return;

            transform.position = targetPosition;
        }

        TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> GenerateFadeOutTweener()
        {
            const float fadeOutLimit = 0f;
            return m_group.DOFade(fadeOutLimit, m_fadeDuration).OnKill(() => m_tweener = null);
        }

        TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> GenerateFadeInTweener()
        {
            const float fadeInLimit = 1f;
            return m_group.DOFade(fadeInLimit, m_fadeDuration).OnKill(() => m_tweener = null);
        }
    }
}

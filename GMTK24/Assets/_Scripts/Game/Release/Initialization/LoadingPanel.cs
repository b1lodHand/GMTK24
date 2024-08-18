using com.absence.utilities;
//using DG.Tweening;
//using DG.Tweening.Core;
//using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace com.game.release.initialization
{
    public class LoadingPanel : Singleton<LoadingPanel>
    {
        [SerializeField] private GameObject m_background;
        //[SerializeField] private CanvasGroup m_panelGroup;
        //[SerializeField] private float m_fadeDuration = 0.3f;

        //TweenerCore<float, float, FloatOptions> m_tweener;

        public void Activate()
        {
            //if (m_tweener != null) m_tweener.Kill();

            m_background.SetActive(true);
            //m_tweener = SetupFadeInTweener();
        }

        //TweenerCore<float, float, FloatOptions> SetupFadeInTweener()
        //{
        //    const float fadeInLimit = 1f;
        //    m_tweener = m_panelGroup.DOFade(fadeInLimit, m_fadeDuration);
        //    m_tweener.OnKill(() =>
        //    {
        //        m_tweener = null;
        //    });

        //    m_tweener.OnComplete(() =>
        //    {
        //        m_tweener = null;
        //    });

        //    return m_tweener;   
        //}

        //TweenerCore<float, float, FloatOptions> SetupFadeOutTweener()
        //{
        //    const float fateOutLimit = 0f;
        //    m_tweener = m_panelGroup.DOFade(fateOutLimit, m_fadeDuration);
        //    m_tweener.OnKill(() =>
        //    {
        //        m_tweener = null;
        //    });

        //    m_tweener.OnComplete(() =>
        //    {
        //        m_tweener = null;
        //        m_background.SetActive(false);
        //    });

        //    return m_tweener;
        //}

        public void Deactivate()
        {
            //if (m_tweener != null) m_tweener.Kill();

            m_background.SetActive(false);
            //m_tweener = SetupFadeOutTweener();
        }
    }
}

using com.absence.attributes;
using com.game.extensions.statics;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.game.generics.ui
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectElementSnapper : MonoBehaviour
    {
        [SerializeField, Readonly] private ScrollRect m_scrollRect;
        [SerializeField] [Min(0f)] private float m_snapDuration;
        [SerializeField] private float m_snapOffset;
        [SerializeField] private Ease m_snapEase = Ease.Linear;

        List<RectTransform> m_children = new();
        RectTransform m_selectedChild = null;

        public List<RectTransform> Children => m_children;

        private TweenerCore<Vector2, Vector2, VectorOptions> m_tweener = null;

        public void UpdateScrollPosition()
        {
            Vector2 targetPosition = m_scrollRect.GetSnapPosition(m_selectedChild);

            int index = m_children.IndexOf(m_selectedChild);
            if(index != 0 && index != m_children.Count - 1)
            {
                if (m_scrollRect.horizontal) targetPosition.x += m_snapOffset;
                if (m_scrollRect.vertical) targetPosition.y += m_snapOffset;
            }

            if (m_tweener != null) m_tweener.Kill();

            m_tweener = m_scrollRect.content.DOAnchorPos(targetPosition, m_snapDuration, false).
                    OnKill(() => m_tweener = null).
                    OnComplete(() => m_tweener = null).
                    SetEase(m_snapEase);
        }

        public void RefreshChildren()
        {
            m_children.Clear();
            foreach (RectTransform child in m_scrollRect.content)
            {
                m_children.Add(child);
            }
        }

        public void SetSelectedChild(RectTransform newChild)
        {
            m_selectedChild = newChild;
        }

        private void Reset()
        {
            m_scrollRect = GetComponent<ScrollRect>();
        }
    }
}

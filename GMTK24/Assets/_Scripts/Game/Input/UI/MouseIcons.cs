using System.Collections.Generic;
using UnityEngine;

namespace com.game.input.ui
{
    [CreateAssetMenu(menuName = "Game/Input Icons/MouseIcons Asset", fileName = "New MouseIcons Asset")]
    public class MouseIcons : InputIcons
    {
        [Header("Main")]
        [SerializeField] private Sprite m_move;
        [SerializeField] private Sprite m_vertical;
        [SerializeField] private Sprite m_horizontal;

        [Header("Buttons")]
        [SerializeField] private Sprite m_leftButton;
        [SerializeField] private Sprite m_rightButton;
        // macros?

        [Header("Scroll")]
        [SerializeField] private Sprite m_scrollForward;
        [SerializeField] private Sprite m_scrollBackwards;
        [SerializeField] private Sprite m_scrollMove;
        [SerializeField] private Sprite m_scrollPress;

        public override Dictionary<string, Sprite> GenerateInitialDictionary()
        {
            return new Dictionary<string, Sprite>()
            {
                { "delta", m_move },
                { "delta/y", m_vertical },
                { "delta/x", m_horizontal },

                { "leftButton", m_leftButton },
                { "rightButton", m_rightButton },

                { "scroll/up", m_scrollForward },
                { "scroll/down", m_scrollBackwards },
                { "scroll/y", m_scrollMove },
                { "middleButton", m_scrollPress },
            };
        }
    }
}

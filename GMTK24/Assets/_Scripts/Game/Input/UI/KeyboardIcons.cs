using System.Collections.Generic;
using UnityEngine;

namespace com.game.input.ui
{
    [CreateAssetMenu(menuName = "Game/Input Icons/KeyboardIcons Asset", fileName = "New KeyboardIcons Asset")]
    public class KeyboardIcons : InputIcons
    {
        [Header("Main")]
        [SerializeField] private Sprite m_space;
        [SerializeField] private Sprite m_escape;
        [SerializeField] private Sprite m_tab;
        [SerializeField] private Sprite m_capsLock;
        [SerializeField] private Sprite m_backspace;
        [SerializeField] private Sprite m_enter;
        // macros?

        [Header("Left Controls")]
        [SerializeField] private Sprite m_leftControl;
        [SerializeField] private Sprite m_leftShift;
        [SerializeField] private Sprite m_leftAlt;

        [Header("Right Controls")]
        [SerializeField] private Sprite m_rightControl;
        [SerializeField] private Sprite m_rightShift;
        [SerializeField] private Sprite m_altGr;

        [Header("Numerics")]
        [SerializeField] private Sprite m_1;
        [SerializeField] private Sprite m_2;
        [SerializeField] private Sprite m_3;
        [SerializeField] private Sprite m_4;
        [SerializeField] private Sprite m_5;
        [SerializeField] private Sprite m_6;
        [SerializeField] private Sprite m_7;
        [SerializeField] private Sprite m_8;
        [SerializeField] private Sprite m_9;
        [SerializeField] private Sprite m_0;

        [Header("Letters")]
        [SerializeField] private Sprite m_q;
        [SerializeField] private Sprite m_w;
        [SerializeField] private Sprite m_e;
        [SerializeField] private Sprite m_r;
        [SerializeField] private Sprite m_t;
        [SerializeField] private Sprite m_y;
        [SerializeField] private Sprite m_u;
        [SerializeField] private Sprite m_ı;
        [SerializeField] private Sprite m_o;
        [SerializeField] private Sprite m_p;
        [SerializeField] private Sprite m_a;
        [SerializeField] private Sprite m_s;
        [SerializeField] private Sprite m_d;
        [SerializeField] private Sprite m_f;
        [SerializeField] private Sprite m_g;
        [SerializeField] private Sprite m_h;
        [SerializeField] private Sprite m_j;
        [SerializeField] private Sprite m_k;
        [SerializeField] private Sprite m_l;
        [SerializeField] private Sprite m_z;
        [SerializeField] private Sprite m_x;
        [SerializeField] private Sprite m_c;
        [SerializeField] private Sprite m_v;
        [SerializeField] private Sprite m_b;
        [SerializeField] private Sprite m_n;
        [SerializeField] private Sprite m_m;

        [Header("Functions")]
        [SerializeField] private Sprite m_f1;
        [SerializeField] private Sprite m_f2;
        [SerializeField] private Sprite m_f3;
        [SerializeField] private Sprite m_f4;
        [SerializeField] private Sprite m_f5;
        [SerializeField] private Sprite m_f6;
        [SerializeField] private Sprite m_f7;
        [SerializeField] private Sprite m_f8;
        [SerializeField] private Sprite m_f9;
        [SerializeField] private Sprite m_f10;
        [SerializeField] private Sprite m_f11;
        [SerializeField] private Sprite m_f12;

        [Header("Directionals")]
        [SerializeField] private Sprite m_up;
        [SerializeField] private Sprite m_down;
        [SerializeField] private Sprite m_left;
        [SerializeField] private Sprite m_right;

        [Header("Utilities")]
        [SerializeField] private Sprite m_printScreen;
        [SerializeField] private Sprite m_scrollLock;
        [SerializeField] private Sprite m_numLock;
        [SerializeField] private Sprite m_pause;
        [SerializeField] private Sprite m_insert;
        [SerializeField] private Sprite m_delete;
        [SerializeField] private Sprite m_home;
        [SerializeField] private Sprite m_end;
        [SerializeField] private Sprite m_pageUp;
        [SerializeField] private Sprite m_pageDown;

        [Header("Numpad")]
        [SerializeField] private Sprite m_numpad1;
        [SerializeField] private Sprite m_numpad2;
        [SerializeField] private Sprite m_numpad3;
        [SerializeField] private Sprite m_numpad4;
        [SerializeField] private Sprite m_numpad5;
        [SerializeField] private Sprite m_numpad6;
        [SerializeField] private Sprite m_numpad7;
        [SerializeField] private Sprite m_numpad8;
        [SerializeField] private Sprite m_numpad9;
        [SerializeField] private Sprite m_numpad0;
        [SerializeField] private Sprite m_numpadEnter;

        [Header("Misc")]
        [SerializeField] private Sprite m_doubleQuote;
        [SerializeField] private Sprite m_quote;
        [SerializeField] private Sprite m_OS;
        [SerializeField] private Sprite m_comma;
        [SerializeField] private Sprite m_period;
        [SerializeField] private Sprite m_minus;
        [SerializeField] private Sprite m_plus;
        [SerializeField] private Sprite m_star;
        [SerializeField] private Sprite m_slash;
        [SerializeField] private Sprite m_backslash;

        public override Dictionary<string, Sprite> GenerateInitialDictionary()
        {
            return new Dictionary<string, Sprite>()
            {
                { "space", m_space },
                { "escape", m_escape },
                { "tab", m_tab },
                { "capsLock", m_capsLock },
                { "backspace", m_backspace },
                { "enter", m_enter },

                { "leftCtrl", m_leftControl },
                { "leftShift", m_leftShift },
                { "leftAlt", m_leftAlt },

                { "rightCtrl", m_rightControl },
                { "rightShift", m_rightShift },
                { "rightAlt", m_altGr },

                { "1", m_1 },
                { "2", m_2 },
                { "3", m_3 },
                { "4", m_4 },
                { "5", m_5 },
                { "6", m_6 },
                { "7", m_7 },
                { "8", m_8 },
                { "9", m_9 },
                { "0", m_0 },

                { "q", m_q },
                { "w", m_w },
                { "e", m_e },
                { "r", m_r },
                { "t", m_t },
                { "y", m_y },
                { "u", m_u },
                { "ı", m_ı },
                { "o", m_o },
                { "p", m_p },
                { "a", m_a },
                { "s", m_s },
                { "d", m_d },
                { "f", m_f },
                { "g", m_g },
                { "h", m_h },
                { "j", m_j },
                { "k", m_k },
                { "l", m_l },
                { "z", m_z },
                { "x", m_x },
                { "c", m_c },
                { "v", m_v },
                { "b", m_b },
                { "n", m_n },
                { "m", m_m },

                { "f1", m_f1 },
                { "f2", m_f2 },
                { "f3", m_f3 },
                { "f4", m_f4 },
                { "f5", m_f5 },
                { "f6", m_f6 },
                { "f7", m_f7 },
                { "f8", m_f8 },
                { "f9", m_f9 },
                { "f10", m_f10 },
                { "f11", m_f11 },
                { "f12", m_f12 },

                { "upArrow", m_up },
                { "downArrow", m_down },
                { "leftArrow", m_left },
                { "rightArrow", m_right },

                { "printScreen", m_printScreen },
                { "scrollLock", m_scrollLock },
                { "numLock", m_numLock },
                { "pause", m_pause },
                { "insert", m_insert },
                { "delete", m_delete },
                { "home", m_home },
                { "end", m_end },
                { "pageUp", m_pageUp },
                { "pageDown", m_pageDown },

                { "numpad1", m_numpad1 },
                { "numpad2", m_numpad2 },
                { "numpad3", m_numpad3 },
                { "numpad4", m_numpad4 },
                { "numpad5", m_numpad5 },
                { "numpad6", m_numpad6 },
                { "numpad7", m_numpad7 },
                { "numpad8", m_numpad8 },
                { "numpad9", m_numpad9 },
                { "numpad0", m_numpad0 },
                { "numpadEnter", m_numpadEnter },

                { "backQuote", m_doubleQuote },
                { "quote", m_quote },
                { "", m_OS },
                { "comma", m_comma },
                { "period", m_period },
                { "minus", m_minus },
                { "plus", m_plus },
                { "asterisk", m_star },
                { "slash", m_slash },
                { "backslash", m_backslash },
            };
        }
    }
}

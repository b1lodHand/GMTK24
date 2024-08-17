using System.Collections.Generic;
using UnityEngine;

namespace com.game.input.ui
{
    [CreateAssetMenu(menuName = "Game/Input Icons/GamepadIcons Asset", fileName = "New GamepadIcons Asset")]
    public class GamepadIcons : InputIcons
    {
        [Header("Main")]
        [SerializeField] private Sprite m_touchPad;
        [SerializeField] private Sprite m_options;
        [SerializeField] private Sprite m_share;

        [Header("Left Shoulder")]
        [SerializeField] private Sprite m_leftTrigger;
        [SerializeField] private Sprite m_leftButton;

        [Header("Right Shoulder")]
        [SerializeField] private Sprite m_rightTrigger;
        [SerializeField] private Sprite m_rightButton;

        [Header("D-Pad")]
        [SerializeField] private Sprite m_up;
        [SerializeField] private Sprite m_down;
        [SerializeField] private Sprite m_left;
        [SerializeField] private Sprite m_right;
        [SerializeField] private Sprite m_horizontal;
        [SerializeField] private Sprite m_vertical;
        [SerializeField] private Sprite m_control;

        [Header("Buttons")]
        [SerializeField] private Sprite m_buttonNorth;
        [SerializeField] private Sprite m_buttonSouth;
        [SerializeField] private Sprite m_buttonWest;
        [SerializeField] private Sprite m_buttonEast;

        [Header("Left Stick")]
        [SerializeField] private Sprite m_leftStickUp;
        [SerializeField] private Sprite m_leftStickDown;
        [SerializeField] private Sprite m_leftStickLeft;
        [SerializeField] private Sprite m_leftStickRight;
        [SerializeField] private Sprite m_leftStickHorizontal;
        [SerializeField] private Sprite m_leftStickVertical;
        [SerializeField] private Sprite m_leftStickMove;
        [SerializeField] private Sprite m_leftStickPress;

        [Header("Right Stick")]
        [SerializeField] private Sprite m_rightStickUp;
        [SerializeField] private Sprite m_rightStickDown;
        [SerializeField] private Sprite m_rightStickLeft;
        [SerializeField] private Sprite m_rightStickRight;
        [SerializeField] private Sprite m_rightStickHorizontal;
        [SerializeField] private Sprite m_rightStickVertical;
        [SerializeField] private Sprite m_rightStickMove;
        [SerializeField] private Sprite m_rightStickPress;

        [SerializeField] Dictionary<string, Sprite> m_fullActionPathDictionary;

        public override Dictionary<string, Sprite> GenerateInitialDictionary()
        {
            return new Dictionary<string, Sprite>()
            {
                { "", m_touchPad },
                { "start", m_options },
                { "select", m_share },

                { "leftTrigger", m_leftTrigger },
                { "leftShoulder", m_leftButton },

                { "rightTrigger", m_rightTrigger },
                { "rightShoulder", m_rightButton },

                { "dpad/up", m_up },
                { "dpad/down", m_down },
                { "dpad/left", m_left },
                { "dpad/right", m_right },
                { "dpad/x", m_horizontal },
                { "dpad/y", m_vertical },
                { "dpad", m_control },

                { "buttonNorth", m_buttonNorth },
                { "buttonSouth", m_buttonSouth },
                { "buttonWest", m_buttonWest },
                { "buttonEast", m_buttonEast },

                { "leftStick/up", m_leftStickUp },
                { "leftStick/down", m_leftStickDown },
                { "leftStick/left", m_leftStickLeft },
                { "leftStick/right", m_leftStickRight },
                { "leftStick/x", m_leftStickHorizontal },
                { "leftStick/y", m_leftStickVertical },
                { "leftStick", m_leftStickMove },
                { "leftStickPress", m_leftStickPress },

                { "rightStick/up", m_rightStickUp },
                { "rightStick/down", m_rightStickDown },
                { "rightStick/left", m_rightStickLeft },
                { "rightStick/right", m_rightStickRight },
                { "rightStick/x", m_rightStickHorizontal },
                { "rightStick/y", m_rightStickVertical },
                { "rightStick", m_rightStickMove },
                { "rightStickPress", m_rightStickPress },
            };
        }
    }
}

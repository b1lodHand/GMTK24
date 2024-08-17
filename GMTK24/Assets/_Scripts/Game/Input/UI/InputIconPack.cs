using UnityEngine;

namespace com.game.input.ui
{
    [CreateAssetMenu(menuName = "Game/Input Icons/Input Icon Pack", fileName = "New Input Icon Pack")]
    public class InputIconPack : ScriptableObject
    {
        [SerializeField] private MouseIcons m_mouseIcons;
        [SerializeField] private KeyboardIcons m_keyboardIcons;
        [SerializeField] private GamepadIcons m_PS4Icons;
        [SerializeField] private GamepadIcons m_PS5Icons;
        [SerializeField] private GamepadIcons m_XBOXIcons;

        public MouseIcons MouseIcons => m_mouseIcons;
        public KeyboardIcons KeyboardIcons => m_keyboardIcons;
        public GamepadIcons PS4Icons => m_PS4Icons;
        public GamepadIcons PS5Icons => m_PS5Icons;
        public GamepadIcons XBOXIcons => m_XBOXIcons;

        public void ForceRefresh()
        {
            m_mouseIcons.ForceRefresh();
            m_keyboardIcons.ForceRefresh();
            m_PS4Icons.ForceRefresh();
            m_PS5Icons.ForceRefresh();
            m_XBOXIcons.ForceRefresh();
        }

        public InputIcons GetInputIconsByDeviceName(string deviceName)
        {
            if (MouseIcons.CompareDeviceName(deviceName)) return MouseIcons;
            else if (KeyboardIcons.CompareDeviceName(deviceName)) return KeyboardIcons;
            else if (PS4Icons.CompareDeviceName(deviceName)) return PS4Icons;
            else if (PS5Icons.CompareDeviceName(deviceName)) return PS5Icons;
            else if (XBOXIcons.CompareDeviceName(deviceName)) return XBOXIcons;
            else return null;
        }

        public InputIcons GetInputIconsByFullActionPath(string fullActionPath)
        {
            if (MouseIcons.CheckForDeviceName(fullActionPath)) return MouseIcons;
            else if (KeyboardIcons.CheckForDeviceName(fullActionPath)) return KeyboardIcons;
            else if (PS4Icons.CheckForDeviceName(fullActionPath)) return PS4Icons;
            else if (PS5Icons.CheckForDeviceName(fullActionPath)) return PS5Icons;
            else if (XBOXIcons.CheckForDeviceName(fullActionPath)) return XBOXIcons;
            else return null;
        }
    }
}

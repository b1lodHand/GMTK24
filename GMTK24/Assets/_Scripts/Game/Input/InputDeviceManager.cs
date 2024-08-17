using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.input
{
    public static class InputDeviceManager
    {
        public enum SupportedDevices
        {
            [InspectorName("Keyboard")]     Keyboard = 0,
            [InspectorName("Mouse")]        Mouse = 1,
            [InspectorName("Gamepad/PS4")]  PS4 = 3,
            [InspectorName("Gamepad/PS5")]  PS5 = 4,
            [InspectorName("Gamepad/XBOX")] XBOX = 5,
            [InspectorName("Gamepad/Auto")] Gamepad = 6,
        }
        static readonly Dictionary<SupportedDevices, string> s_deviceNameList = new Dictionary<SupportedDevices, string>() 
        {
            { SupportedDevices.Keyboard, "Keyboard"                },
            { SupportedDevices.Mouse,    "Mouse"                   },
            { SupportedDevices.PS4,      "DualShock4GamepadHID"    },
            { SupportedDevices.PS5,      "DualSenseGamepadHID"     },
            { SupportedDevices.XBOX,     "XInputControllerWindows" },
            { SupportedDevices.Gamepad,  "Gamepad"                 },
        };
        static readonly Dictionary<string, SupportedDevices> s_invertedDeviceNameList = s_deviceNameList.ToDictionary((i) => i.Value, (i) => i.Key);

        public static SupportedDevices DefaultGamepad { get; } = SupportedDevices.XBOX;

        public static string GetDeviceName(SupportedDevices deviceType)
        {
            return s_deviceNameList[deviceType];
        }

        public static SupportedDevices GetDeviceByName(string deviceName)
        {
            return s_invertedDeviceNameList[deviceName];
        }

        public static bool IsValidDeviceName(string deviceName)
        {
            return s_invertedDeviceNameList.ContainsKey(deviceName);
        }

        public static bool IsValidGamepadName(string deviceName)
        {
            if (!IsValidDeviceName(deviceName)) return false;

            string keyboardName = GetDeviceName(SupportedDevices.Keyboard);
            string mouseName = GetDeviceName(SupportedDevices.Mouse);

            if (deviceName.Equals(keyboardName)) return false;
            if (deviceName.Equals(mouseName)) return false;

            return true;
        }
    }
}

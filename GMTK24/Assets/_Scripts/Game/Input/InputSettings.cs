using System.Collections.Generic;

namespace com.game.input
{
    public static class InputSettings
    {
        public static class Rebinding
        {
            static readonly List<string> s_rebindExcludedControls = new List<string>()
            {
                "<Dualshock4GamepadHID>/systemButton",
                "<Dualshock4GamepadHID>/touchpadButton",
                "<Dualshock4GamepadHID>/touchpadButton",
                "<Gamepad>/leftStick",
                "<Gamepad>/rightStick",
            };
            public static List<string> RebindExcludedControls => s_rebindExcludedControls;

            public static int RebindEndDelayInFrames { get; } = 120;
        }
    }
}

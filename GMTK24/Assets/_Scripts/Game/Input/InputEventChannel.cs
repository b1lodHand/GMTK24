
using System;
using UnityEngine;

namespace com.game.input
{
    public static class InputEventChannel
    {
        public static class Internal
        {
            public static event Action<string> OnDeviceChanged = null;
            public static void ReceiveDeviceChangeEvent(string newDevice) => OnDeviceChanged?.Invoke(newDevice);

            public static void Reset()
            {
                OnDeviceChanged = null;
            }
        }

        public static class Player
        {
            public static event Action<Vector2> OnMovementInput = null;
            public static void ReceiveMovementInput(Vector2 newInput) => OnMovementInput?.Invoke(newInput);

            public static event Action OnInteractionInput = null;
            public static void ReceiveInteractionInput() => OnInteractionInput?.Invoke();

            public static event Action OnSwitchInteractionInput = null;
            public static void ReceiveSwitchInteractionInput() => OnSwitchInteractionInput?.Invoke();

            public static void Reset()
            {
                OnMovementInput = null;
                OnInteractionInput = null;
                OnSwitchInteractionInput = null;
            }
        }

        public static class UI
        {
            public static event Action OnCancelInput = null;
            public static void ReceiveCancelInput() => OnCancelInput?.Invoke();

            public static event Action<Vector2> OnNavigateInput = null;
            public static void ReceiveNavigateInput(Vector2 direction) => OnNavigateInput?.Invoke(direction);

            public static event Action OnSubmitInput = null;
            public static void ReceiveSubmitInput() => OnSubmitInput?.Invoke();

            public static void Reset()
            {
                OnCancelInput = null;
                OnNavigateInput = null;
                OnSubmitInput = null;
            }
        }

        public static class Dialogue
        {
            public static event Action OnDialogueSkipInput = null;
            public static void ReceiveDialogueSkipInput() => OnDialogueSkipInput?.Invoke();

            //public static event Action OnDialogueTryToExitInput = null;
            //public static void ReceiveDialogueTryToExitInput() => OnDialogueTryToExitInput?.Invoke();

            public static event Action<float> OnScrollInput = null;
            public static void ReceiveScrollInput(float scrollDelta) => OnScrollInput?.Invoke(scrollDelta);

            public static void Reset()
            {
                OnDialogueSkipInput = null;
                //OnDialogueTryToExitInput = null;
                OnScrollInput = null;
            }
        }

        public static class Constant
        {
            public static event Action OnOpenGenericMenuInput = null;
            public static void ReceiveOpenGenericMenuInput() => OnOpenGenericMenuInput?.Invoke();

            public static event Action OnOpenConsoleWindowInput = null;
            public static void ReceiveOpenConsoleWindowInput() => OnOpenConsoleWindowInput?.Invoke();

            public static void Reset()
            {
                OnOpenGenericMenuInput = null;
                OnOpenConsoleWindowInput = null;
            }
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Reset()
        {
            Internal.Reset();
            Player.Reset();
            UI.Reset();
            Dialogue.Reset();
            Constant.Reset();
        }
    }
}

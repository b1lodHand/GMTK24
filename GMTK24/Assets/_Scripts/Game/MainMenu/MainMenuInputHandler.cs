using com.game.input;
using com.game.misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.mainmenu
{
    public class MainMenuInputHandler : MonoBehaviour
    {
        private void Start()
        {
            Apply();
        }

        void Apply(/* PlayerInputActions inputActions*/)
        {
            if (InputManager.Instance == null) return;
            PlayerInputActions inputActions = InputManager.Instance.InputActions;

            // ui
            inputActions.UI.Submit.performed += OnSubmit;
            inputActions.UI.Navigate.performed += OnNavigate;
            inputActions.UI.Cancel.performed += OnCancel;

            // constant
            inputActions.Constant.OpenConsoleWindow.performed += OnOpenConsoleWindow;
        }
        void Revert()
        {
            if (InputManager.Instance == null) return;
            PlayerInputActions inputActions = InputManager.Instance.InputActions;

            // ui
            inputActions.UI.Submit.performed -= OnSubmit;
            inputActions.UI.Navigate.performed -= OnNavigate;
            inputActions.UI.Cancel.performed -= OnCancel;

            // constant
            inputActions.Constant.OpenConsoleWindow.performed -= OnOpenConsoleWindow;
        }

        #region Callbacks
        private void OnOpenConsoleWindow(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            InputEventChannel.Constant.ReceiveOpenConsoleWindowInput();
        }
        private void OnCancel(InputAction.CallbackContext context)
        {
            InputEventChannel.UI.ReceiveCancelInput();
        }
        private void OnNavigate(InputAction.CallbackContext context)
        {
            InputEventChannel.UI.ReceiveNavigateInput(context.ReadValue<Vector2>());
        }
        private void OnSubmit(InputAction.CallbackContext context)
        {
            InputEventChannel.UI.ReceiveSubmitInput();
        }
        #endregion

        private void OnDestroy()
        {
            Revert();
        }
    }
}

using com.game.input;
using com.game.misc;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.player
{
    [DefaultExecutionOrder(-99)]
    public class PlayerInputHandler : MonoBehaviour
    {
        //[SerializeField] private bool m_debugMode = false;

        private void Awake()
        {
            AddCallbacks();
        }

        void Apply(PlayerInputActions inputActions)
        {
            // in-game
            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Move.canceled += OnStop;
            inputActions.Player.Interact.performed += OnInteract;
            inputActions.Player.SwitchInteractable.performed += OnInteractableSwitched;
            inputActions.Player.Attack.performed += OnAttack;

            // ui
            inputActions.UI.Cancel.performed += OnCancel;
            inputActions.UI.Navigate.performed += OnNavigate;
            inputActions.UI.Submit.performed += OnSubmit;

            // dialogue
            inputActions.Dialogue.Skip.performed += OnDialogueSkip;
            //inputActions.Dialogue.TryToExit.performed += OnDialogueTryExit;
            inputActions.Dialogue.Scroll.performed += OnDialogueScroll;

            // constant
            inputActions.Constant.OpenGenericMenu.performed += OnOpenGenericMenu;
            inputActions.Constant.OpenConsoleWindow.performed += OnOpenConsoleWindow;
        }
        void Revert()
        {
            if (InputManager.Instance == null) return;

            PlayerInputActions inputActions = InputManager.Instance.InputActions;

            // in-game
            inputActions.Player.Move.performed -= OnMove;
            inputActions.Player.Move.canceled -= OnStop;
            inputActions.Player.Interact.performed -= OnInteract;
            inputActions.Player.SwitchInteractable.performed -= OnInteractableSwitched;
            inputActions.Player.Attack.performed -= OnAttack;

            // ui
            inputActions.UI.Cancel.performed -= OnCancel;
            inputActions.UI.Navigate.performed -= OnNavigate;
            inputActions.UI.Submit.performed -= OnSubmit;

            // dialogue
            inputActions.Dialogue.Skip.performed -= OnDialogueSkip;
            //inputActions.Dialogue.TryToExit.performed -= OnDialogueTryExit;
            inputActions.Dialogue.Scroll.performed -= OnDialogueScroll;

            // constant
            inputActions.Constant.OpenGenericMenu.performed -= OnOpenGenericMenu;
            inputActions.Constant.OpenConsoleWindow.performed -= OnOpenConsoleWindow;
        }

        #region In-game Callbacks
        private void OnInteractableSwitched(InputAction.CallbackContext context)
        {
            InputEventChannel.Player.ReceiveSwitchInteractionInput();
        }
        private void OnInteract(InputAction.CallbackContext context)
        {
            InputEventChannel.Player.ReceiveInteractionInput();
        }
        private void OnStop(InputAction.CallbackContext context)
        {
            InputEventChannel.Player.ReceiveMovementInput(Vector2.zero);
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 movement = context.ReadValue<Vector2>();
            InputEventChannel.Player.ReceiveMovementInput(movement);
        }
        private void OnAttack(InputAction.CallbackContext context)
        {
            InputEventChannel.Player.ReceiveAttackInput();
        }
        #endregion

        #region UI Callbacks
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

        #region Dialogue Callbacks
        private void OnDialogueSkip(InputAction.CallbackContext context)
        {
            InputEventChannel.Dialogue.ReceiveDialogueSkipInput();
        }
        //private void OnDialogueTryExit(InputAction.CallbackContext context)
        //{
        //    InputEventChannel.Dialogue.ReceiveDialogueTryToExitInput();   
        //}
        private void OnDialogueScroll(InputAction.CallbackContext context)
        {
            InputEventChannel.Dialogue.ReceiveScrollInput(context.ReadValue<float>());
        }
        #endregion

        #region Constant Callbacks
        private void OnOpenGenericMenu(InputAction.CallbackContext context)
        {
            InputEventChannel.Constant.ReceiveOpenGenericMenuInput();
        }
        private void OnOpenConsoleWindow(InputAction.CallbackContext context)
        {
            InputEventChannel.Constant.ReceiveOpenConsoleWindowInput();
        }
        #endregion

        #region Internal
        void AddCallbacks()
        {
            StartCoroutine(C_AddCallbacks());
        }
        IEnumerator C_AddCallbacks()
        {
            if (InputManager.Instance == null) yield return new WaitUntil(() => InputManager.Instance != null);

            InputManager.Instance.AddAssetCallbacks(Apply);
        }

        private void OnDestroy()
        {
            Revert();
        }
        #endregion
    }
}

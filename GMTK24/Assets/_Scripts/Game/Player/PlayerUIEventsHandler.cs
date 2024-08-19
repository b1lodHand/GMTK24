using com.absence.consolesystem;
using com.absence.dialoguesystem;
using com.game.menus;
using com.game.internals;
using UnityEngine;
using UnityEngine.EventSystems;
using com.game.input;

namespace com.game.player
{
    public class PlayerUIEventsHandler : MonoBehaviour
    {
        private void Start()
        {
            InputEventChannel.Constant.OnOpenGenericMenuInput += OnGenericMenuInput;
            InputEventChannel.Constant.OnOpenConsoleWindowInput += OnConsoleWindowInput;

            InputEventChannel.UI.OnCancelInput += OnCancelInput;
            InputEventChannel.UI.OnNavigateInput += OnNavigateInput;

            MenuManager.Instance.OnAnyMenuOpens += OnAnyMenuOpens;
            MenuManager.Instance.OnAllMenusClosed += OnAllMenusClosed;
        }

        private void OnNavigateInput(Vector2 vector)
        {
            if (ConsoleWindow.Instance.IsOpen) return;

            bool anySelectedObject = EventSystem.current.currentSelectedGameObject != null;
            if (anySelectedObject)
            {
                if (MenuManager.Instance.AnyMenusOpen) MenuManager.Instance.Last.SetLastSelectedObject(EventSystem.current.currentSelectedGameObject);
                return;
            }

            if (MenuManager.Instance.AnyMenusOpen)
            {
                MenuManager.Instance.ReselectFirstElement();
                return;
            }

            if (Player.Instance.InDialogue) DialogueDisplayer.Instance.ReselectFirstOptionIfExists();
        }

        private void OnCancelInput()
        {
            if (ConsoleWindow.Instance.IsOpen) return;
            if (InputManager.Instance.InRebindProcess)
            {
                Debug.Log("a");
                return;
            }

            bool anySelectedObject = EventSystem.current.currentSelectedGameObject != null;
            if (InternalSettings.CANCEL_UNSELECTS_FIRST && anySelectedObject)
            {
                EventSystem.current.SetSelectedGameObject(null);
                return;
            }

            if (MenuManager.Instance.AnyMenusOpen)
            {
                MenuManager.Instance.CloseLast();
                return;
            }

            if (InternalSettings.ESC_EXITS_DIALOGUE && Player.Instance.InDialogue)
            {
                Player.Instance.ForceExitDialogue();
                return;
            }
        }

        private void OnAllMenusClosed()
        {
            HandleInputMapping();
        }

        private void OnAnyMenuOpens()
        {
            InputManager.Instance.SwitchToUIMap();
        }
            
        private void OnGenericMenuInput()
        {
            if (InternalSettings.ESC_EXITS_DIALOGUE && Player.Instance.InDialogue) return;

            if (ConsoleWindow.Instance.IsOpen) ConsoleWindow.Instance.CloseWindow(false);

            if (MenuManager.Instance.IsOpen(Constants.GENERIC_MENU_NAME) && !MenuManager.Instance.IsLast(Constants.GENERIC_MENU_NAME)) return;

            if (InputManager.Instance.IsGamepad)
            {
                MenuManager.Instance.Toggle(Constants.GENERIC_MENU_NAME, true);
                return;
            }

            if (!MenuManager.Instance.IsOpen(Constants.GENERIC_MENU_NAME)) MenuManager.Instance.Open(Constants.GENERIC_MENU_NAME, true);
        }

        private void OnConsoleWindowInput()
        {
            if (MenuManager.Instance.IsInGenericMenu()) return;

            ConsoleWindow.Instance.SwitchWindowVisibility();

            if (ConsoleWindow.Instance.IsOpen)
            {
                InputManager.Instance.SwitchToUIMap();
                return;
            }

            HandleInputMapping();
        }

        public static void HandleInputMapping()
        {
            if (Player.Instance == null)
            {
                InputManager.Instance.SwitchToUIMap();
                return;
            }

            if (Player.Instance.InDialogue)
            {
                InputManager.Instance.SwitchToDialogueMap();
                DialogueDisplayer.Instance.ReselectLastOptionIfExists();
            }

            else
            {
                InputManager.Instance.SwitchToPlayerMap();
            }
        }
    }
}

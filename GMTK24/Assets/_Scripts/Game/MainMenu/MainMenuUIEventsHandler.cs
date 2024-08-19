using com.absence.consolesystem;
using com.game.input;
using com.game.internals;
using com.game.menus;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.game.mainmenu
{
    public class MainMenuUIEventsHandler : MonoBehaviour
    {
        private void Start()
        {
            InputManager.Instance.SwitchToUIMap();

            InputEventChannel.UI.OnCancelInput += OnCancel;
            InputEventChannel.Constant.OnOpenConsoleWindowInput += OnConsoleWindowInput;
        }

        private void OnCancel()
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
        }

        private void OnConsoleWindowInput()
        {
            ConsoleWindow.Instance.SwitchWindowVisibility();

            if (ConsoleWindow.Instance.IsOpen)
            {
                InputManager.Instance.SwitchToUIMap();
                return;
            }
        }
    }
}

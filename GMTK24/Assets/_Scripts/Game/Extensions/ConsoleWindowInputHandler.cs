using com.absence.consolesystem;
using com.game.input;
using com.game.player;
using UnityEngine;

namespace com.game.extensions
{
    [DisallowMultipleComponent]
    [AddComponentMenu("absencee_/absent-console/Console Window Input Handler")]
    public class ConsoleWindowInputHandler : MonoBehaviour
    {
        private void Awake()
        {
            InputEventChannel.UI.OnNavigateInput += OnNavigate;
            InputEventChannel.UI.OnSubmitInput += OnSubmit;
            InputEventChannel.UI.OnCancelInput += OnCancel;
        }

        private void OnCancel()
        {
            if (!ConsoleWindow.Instance.IsOpen) return;

            ConsoleWindow.Instance.CloseWindow(false);

            PlayerUIEventsHandler.HandleInputMapping();
        }

        private void OnSubmit()
        {
            if (!ConsoleWindow.Instance.IsOpen) return;

            ConsoleWindow.Instance.RetrieveEnterInput();
        }

        private void OnNavigate(Vector2 direction)
        {
            if (!ConsoleWindow.Instance.IsOpen) return;
            if (!(direction.Equals(Vector2.up))) return;

            ConsoleWindow.Instance.LoadLastCommand();
        }
    }
}

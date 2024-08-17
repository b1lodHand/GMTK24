using com.absence.consolesystem;
using com.absence.dialoguesystem;
using com.game.input;
using com.game.menus;
using com.game.player;
using UnityEngine;

namespace com.game.extensions
{
    [RequireComponent(typeof(DialogueInstance))]
    [AddComponentMenu("absencee_/absent-dialogues/Dialogue Instance Extensions/Dialogue Input Handler")]
    [DisallowMultipleComponent]
    public class DialogueInputHandler : DialogueExtensionBase
    {
        public override void OnAfterCloning()
        {
            base.OnAfterCloning();
            InputEventChannel.Dialogue.OnDialogueSkipInput += OnDialogueSkip;
            InputEventChannel.Dialogue.OnScrollInput += OnDialogueScroll;
            InputEventChannel.UI.OnSubmitInput += OnDialogueSkip;
        }

        private void OnDialogueScroll(float scrollDelta)
        {
            if (scrollDelta > 0f) DialogueDisplayer.Instance.TrySelectUpperOption();
            else if (scrollDelta < 0f) DialogueDisplayer.Instance.TrySelectLowerOption();
        }

        private void OnDialogueSkip()
        {
            if (ConsoleWindow.Instance.IsOpen) return;
            if (MenuManager.Instance.IsInGenericMenu()) return;
            if (!m_instance.InDialogue) return;

            if (m_instance.Player.State == DialoguePlayer.PlayerState.WaitingForInput)
            {
                m_instance.Player.Continue();
            }

            else if (m_instance.Player.State == DialoguePlayer.PlayerState.WaitingForOption)
            {
                if (InputManager.Instance.IsGamepad) return;

                int index = DialogueDisplayer.Instance.TryGetCurrentSelectedOptionIndex();
                if (index == -1) return;

                m_instance.Player.Context.OptionIndex = index;
                m_instance.ForceContinue();
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("CONTEXT/DialogueInstance/Add Extension/Input Handler")]
        static void AddExtensionMenuItem(UnityEditor.MenuCommand command)
        {
            DialogueInstance instance = (DialogueInstance)command.context;
            instance.AddExtension<DialogueInputHandler>();
        }
#endif
    }
}

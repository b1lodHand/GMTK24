using com.absence.utilities;
using com.absence.attributes;
using com.game.misc;
using com.game.internals;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Text;
using System.Collections;
using System;

namespace com.game.input
{
    [RequireComponent(typeof(PlayerInput))]
    [DefaultExecutionOrder(-100)]
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField, Readonly] private PlayerInput m_playerInput;
        [SerializeField] private bool m_debugMode;
        [SerializeField, Readonly] private bool m_inUI = false;
        [SerializeField, Readonly] private bool m_inRebindProcess = false;
        [SerializeField, Readonly] private string m_currentDeviceName;

        PlayerInputActions m_inputActions;
        string m_keyboardAndMouseSchemeName;
        string m_gamepadSchemeName;
        string m_currentSchemeName;
        int m_lastRebindDelay = 0;
        bool m_initialized = false;

        Action<PlayerInputActions> m_onInitialization;

        public bool InUI => m_inUI;
        public bool InRebindProcess => m_inRebindProcess;
        public bool IsGamepad => CurrentSchemeName.Equals(GamepadSchemeName);
        public bool IsKeyboardAndMouse => CurrentSchemeName.Equals(KeyboardAndMouseSchemeName);
        public string CurrentDeviceName => m_currentDeviceName;
        public string KeyboardAndMouseSchemeName => m_keyboardAndMouseSchemeName;
        public string GamepadSchemeName => m_gamepadSchemeName;
        public string CurrentSchemeName => m_currentSchemeName;
        public PlayerInput PlayerInput => m_playerInput;
        public PlayerInputActions InputActions => m_inputActions;

        #region API
        public void SwitchToUIMap()
        {
            m_inUI = true;

            m_inputActions.Player.Disable();
            m_inputActions.UI.Enable();
            m_inputActions.Dialogue.Disable();
            m_inputActions.Constant.Enable();
        }
        public void SwitchToPlayerMap()
        {
            m_inUI = false;

            m_inputActions.Player.Enable();
            m_inputActions.UI.Disable();
            m_inputActions.Dialogue.Disable();
            m_inputActions.Constant.Enable();
        }
        public void SwitchToDialogueMap()
        {
            m_inUI = false;

            if (InternalSettings.CAN_MOVE_IN_DIALOGUE) m_inputActions.Player.Enable();
            else m_inputActions.Player.Disable();

            m_inputActions.UI.Enable();
            m_inputActions.UI.Cancel.Disable();

            m_inputActions.Dialogue.Enable();
            m_inputActions.Constant.Enable();
        }

        public void StartRebindProcess()
        {
            m_inRebindProcess = true;
            m_inputActions.UI.Disable();

            DisableUINavigation();
        }
        public void EndRebindProcess()
        {
            m_lastRebindDelay = 0;
            StartCoroutine(C_EndRebind());
        }

        public void AddAssetCallbacks(Action<PlayerInputActions> onInitialize)
        {
            if (m_initialized) onInitialize?.Invoke(m_inputActions);
            else m_onInitialization += onInitialize;
        }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        #region Internal
        private void OnEnable()
        {
            m_inputActions.Enable();
            SwitchToPlayerMap();
        }
        private void OnDisable()
        {
            m_inputActions.Disable();
        }
        private void OnDestroy()
        {
            m_inputActions.Disable();
        }
        private void Reset()
        {
            m_playerInput = GetComponent<PlayerInput>();
        }

        void DisableUINavigation()
        {
            if (EventSystem.current != null) EventSystem.current.sendNavigationEvents = false;
        }
        void EnableUINavigation()
        {
            if (EventSystem.current != null) EventSystem.current.sendNavigationEvents = true;
        }
        IEnumerator C_EndRebind()
        {
            while (m_lastRebindDelay < input.InputSettings.Rebinding.RebindEndDelayInFrames)
            {
                m_lastRebindDelay++;
                yield return null;
            }

            m_inputActions.UI.Enable();
            m_inRebindProcess = false;

            EnableUINavigation();

            SwitchToUIMap();
        }
        void Initialize()
        {
            m_initialized = false;
            m_onInitialization = null;

            m_inputActions = new();
            m_initialized = true;

            m_onInitialization?.Invoke(m_inputActions);

            m_keyboardAndMouseSchemeName = m_inputActions.KeyboardMouseScheme.name;
            m_gamepadSchemeName = m_inputActions.GamepadScheme.name;
            m_currentSchemeName = m_playerInput.defaultControlScheme;

            m_playerInput.onControlsChanged += OnControlsChanged;
            //OnControlsChanged(m_playerInput);
        }
        private void OnControlsChanged(PlayerInput input)
        {
            string newSchemeName = input.currentControlScheme;
            bool deviceChanged = false;

            if (!m_currentDeviceName.Equals(input.devices[0].name))
            {
                m_currentDeviceName = input.devices[0].name;
                deviceChanged = true;
            }

            if (m_debugMode)
            {
                StringBuilder deviceNameList = new();
                foreach (InputDevice device in input.devices)
                {
                    deviceNameList.Append(device.name);
                    deviceNameList.Append(" ||| ");
                }

                Debug.Log($"control scheme changed: {newSchemeName}, with devices: {deviceNameList.ToString()}");
            }

            m_currentSchemeName = newSchemeName;
            if (deviceChanged) InputEventChannel.Internal.ReceiveDeviceChangeEvent(CurrentDeviceName);
        }
        #endregion
    }
}

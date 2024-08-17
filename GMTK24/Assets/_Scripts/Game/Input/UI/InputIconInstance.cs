using com.absence.attributes;
using com.game.extensions;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace com.game.input.ui
{
    public class InputIconInstance : MonoBehaviour
    {
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private bool m_handleActionActivation = false;
        [SerializeField] private InputDeviceManager.SupportedDevices m_targetDevice;
        [SerializeField] private InputActionReference m_actionReference;

        [Space(10)]

        [SerializeField] private bool m_useImage = false;
        [SerializeField, ShowIf(nameof(m_useImage))] private Image m_image;

        [Space(10)]

        [SerializeField] private bool m_useText = false;
        [SerializeField, ShowIf(nameof(m_useText))] private InputBinding.DisplayStringOptions m_textDisplayOptions;
        [SerializeField, ShowIf(nameof(m_useText))] private TMP_Text m_text;

        [Space(10)]

        [SerializeField] private bool m_useAutoLabel = false;
        [SerializeField, ShowIf(nameof(m_useAutoLabel))] private TMP_Text m_label;

        [Space(10)]

        [SerializeField] private bool m_canPerformRebind = false;
        [SerializeField, ShowIf(nameof(m_canPerformRebind))] private GameObject m_rebindPanel;

        [SerializeField, Readonly] private bool m_rebindInProgress = false;
        
        string m_targetDeviceName;
        InputAction m_targetAction;

        private void Awake()
        {
            Setup();
        }
        void Setup()
        {
            m_targetDeviceName = InputDeviceManager.GetDeviceName(m_targetDevice);
            m_targetAction = InputManager.Instance.InputActions.FindActionByRef(m_actionReference);

            if (m_targetDevice == InputDeviceManager.SupportedDevices.Gamepad)
            {
                InputEventChannel.Internal.OnDeviceChanged -= OnDeviceChanged;
                InputEventChannel.Internal.OnDeviceChanged += OnDeviceChanged;
            }

            ForceInitialize();
        }

        #region API
        public void Refresh()
        {
            Enable();

            if (m_useImage && InputIconManager.Instance.TryGetValidIcon(GenerateFullActionPath(), out Sprite iconFound))
                m_image.sprite = iconFound;

            if (m_useText) m_text.text = m_targetAction.bindings[FindBindingIndex()].ToDisplayString(m_textDisplayOptions);

            if (m_useAutoLabel) m_label.text = m_targetAction.name;
        }
        public void Enable()
        {
            if (m_useImage) m_image.gameObject.SetActive(true);
            if (m_useText) m_text.gameObject.SetActive(true);

            HandleRebindPanel();
        }
        public void Disable()
        {
            if (m_useImage) m_image.gameObject.SetActive(false);
            if (m_useText) m_text.gameObject.SetActive(false);

            HandleRebindPanel();
        }
        public void ForceInitialize()
        {
            if (InputIconManager.Instance == null) StartCoroutine(C_DelayedInitialize());
            else Initialize();
        }
        public void StartRebinding()
        {
            if (!m_canPerformRebind)
            {
                Debug.LogWarning("This icon displayer cannot perform rebind.");
                return;
            }

            if (InputManager.Instance.InRebindProcess)
            {
                Debug.LogWarning("There is an active rebind process already.");
                return;
            }

            int rebindingIndex = FindBindingIndex();
            if (rebindingIndex == -1)
            {
                Debug.LogWarning("Something went wrong while trying to rebind an action.");
                return;
            }

            if (m_handleActionActivation)  m_targetAction.Disable();

            InputActionRebindingExtensions.RebindingOperation rebindOperation =
                m_targetAction.PerformInteractiveRebinding(rebindingIndex)
                .WithDefaultControlsExcluded()
                .WithCancelingThrough("*/{Cancel}")
                .OnComplete(OnRebindComplete)
                .OnCancel(OnRebindCancel);

            if (m_targetDevice == InputDeviceManager.SupportedDevices.Keyboard) rebindOperation = rebindOperation.WithControlsExcluding("Mouse");
            else if (m_targetDevice == InputDeviceManager.SupportedDevices.Mouse) rebindOperation = rebindOperation.WithControlsExcluding("Keyboard");

            m_rebindInProgress = true;
            InputManager.Instance.StartRebindProcess();
            rebindOperation.Start();

            HandleRebindPanel();
        }
        #endregion

        #region Callbacks
        private void OnDeviceChanged(string newDevice)
        {
            InputDeviceManager.SupportedDevices device;
            try
            {
                device = InputDeviceManager.GetDeviceByName(newDevice);
            }

            catch
            {
                Debug.LogWarning($"There are no input devices in InputDeviceManager.SupportedDevices named '{newDevice}'.");
                return;
            }

            if (device == InputDeviceManager.SupportedDevices.Keyboard)
            {
                return;
            }

            if (device == InputDeviceManager.SupportedDevices.Mouse)
            {
                return;
            }

            if (device == InputDeviceManager.SupportedDevices.Gamepad)
            {
                m_targetDeviceName = InputDeviceManager.GetDeviceName(InputDeviceManager.DefaultGamepad);
            }

            m_targetDeviceName = newDevice;
            Refresh();
        }
        private void OnRebindComplete(InputActionRebindingExtensions.RebindingOperation operation)
        {
            if (m_debugMode) Debug.Log("Rebinding operation ended successfully.");

            if (m_handleActionActivation) m_targetAction.Enable();
            m_rebindInProgress = false;
            InputManager.Instance.EndRebindProcess();

            HandleRebindPanel();
            Refresh();
        }
        private void OnRebindCancel(InputActionRebindingExtensions.RebindingOperation operation)
        {
            if (m_debugMode) Debug.Log("Rebinding operation cancelled.");

            if (m_handleActionActivation) m_targetAction.Enable();
            m_rebindInProgress = false;
            InputManager.Instance.EndRebindProcess();

            HandleRebindPanel();
            Refresh();
        }
        #endregion

        #region Internal
        void HandleRebindPanel()
        {
            if (m_rebindPanel != null) m_rebindPanel.SetActive(m_rebindInProgress);
            if (m_useImage) m_image.gameObject.SetActive(!m_rebindInProgress);
        }
        string GenerateFullActionPath()
        {
            if (m_targetDevice == InputDeviceManager.SupportedDevices.Gamepad)
                return $"{m_targetDeviceName}/{GenerateBindingName(InputDeviceManager.GetDeviceName(InputDeviceManager.SupportedDevices.Gamepad))}";

            return $"{m_targetDeviceName}/{GenerateBindingName(m_targetDeviceName)}";
        }
        string GenerateBindingName(string deviceNameToCheck)
        {
            InputBinding binding = m_targetAction.bindings.Where(b =>
            {
                if (b.overridePath != null) return b.overridePath.Contains(deviceNameToCheck);
                else return b.path.Contains(deviceNameToCheck);

            }).FirstOrDefault();

            if (binding.overridePath != null) return binding.overridePath.Replace($"<{deviceNameToCheck}>/", "");
            else return binding.path.Replace($"<{deviceNameToCheck}>/", "");
        }
        int FindBindingIndex()
        {
            if (m_targetDevice == InputDeviceManager.SupportedDevices.Gamepad)
                return m_targetAction.bindings.IndexOf(b => b.path.Contains(InputDeviceManager.GetDeviceName(InputDeviceManager.SupportedDevices.Gamepad)));

            return m_targetAction.bindings.IndexOf(b => b.path.Contains(m_targetDeviceName));
        }
        void Initialize()
        {
            if (m_targetDevice == InputDeviceManager.SupportedDevices.Gamepad) InitializeForGamepad();
            else Refresh();
        }
        void InitializeForGamepad()
        {
            string currentDevice = InputManager.Instance.CurrentDeviceName;

            if (InputDeviceManager.IsValidGamepadName(currentDevice)) OnDeviceChanged(currentDevice);
            else OnDeviceChanged(InputDeviceManager.GetDeviceName(InputDeviceManager.SupportedDevices.XBOX));
        }
        IEnumerator C_DelayedInitialize()
        {
            yield return new WaitUntil(() => InputIconManager.Instance != null);
            Initialize();
        }
        #endregion

    }
}

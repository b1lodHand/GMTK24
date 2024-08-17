using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.input.ui
{
    public abstract class InputIcons : ScriptableObject
    {
        [Header("Device")]
        [SerializeField] protected bool m_debugMode = false;
        [SerializeField] protected InputDeviceManager.SupportedDevices m_deviceType;
        [SerializeField] protected List<string> m_otherAcceptedDeviceNames;

        [SerializeField] protected Dictionary<string, Sprite> m_converter;

        protected string m_deviceName => InputDeviceManager.GetDeviceName(m_deviceType);

        public abstract Dictionary<string, Sprite> GenerateInitialDictionary();

        public virtual bool TryGetValidIcon(string targetKey, out Sprite iconFound)
        {
            iconFound = null;
            string binding = targetKey.Replace($"{m_deviceName}/", string.Empty);

            if (m_debugMode) Debug.Log(targetKey);  

            if (!m_converter.ContainsKey(binding)) return false;

            iconFound = m_converter[binding];
            return true;
        }

        public virtual bool CompareDeviceName(string deviceName)
        {
            bool result = false;
            if (m_deviceName.Equals(deviceName)) result = true;
            if (m_otherAcceptedDeviceNames.Any(dvn => dvn.Equals(deviceName))) result = true;

            return result;
        }

        public virtual bool CheckForDeviceName(string fullActionPath)
        {
            bool result = false;
            if (fullActionPath.Contains(m_deviceName)) result = true;
            if (m_otherAcceptedDeviceNames.Any(dvn => fullActionPath.Contains(dvn))) result = true;

            return result;
        }

        public virtual void ForceRefresh()
        {
            m_converter = GenerateInitialDictionary();
        }

        public virtual void SoftRefresh()
        {
            if (m_converter == null) m_converter = GenerateInitialDictionary();
        }
    }
}

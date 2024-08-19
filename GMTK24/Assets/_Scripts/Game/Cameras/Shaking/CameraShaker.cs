using Cinemachine;
using com.absence.timersystem;
using System;
using System.Linq;
using UnityEngine;

namespace com.game.cameras.shaking
{
    [RequireComponent(typeof(Camera))]
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private NoiseSettings m_mildProfile;
        [SerializeField] private NoiseSettings m_strongProfile;
        [SerializeField] private NoiseSettings m_extremeProfile;

        [SerializeField] private ShakeProperties m_defaultShakeProperties = ShakeProperties.Default;

        CinemachineBasicMultiChannelPerlin m_perlin;
        NoiseSettings m_settings;
        Timer m_timer;
        float m_amplitudeGain;
        float m_frequencyGain;
        float m_amplitudeModifier;
        float m_timeSpent;
        float m_duration;

        public void Shake() => Shake(m_defaultShakeProperties);
        public void Shake(ShakeProperties properties)
        {
            if (m_timer != null) m_timer.Fail();
            if (properties.Duration <= 0f) return;

            m_settings = GetSettings(properties.Profile);
            m_amplitudeGain = properties.Amplitude;
            m_frequencyGain = properties.Frequency;
            m_duration = properties.Duration;
            m_amplitudeModifier = 1f;
            m_timeSpent = 0f;
            bool fadeOut = properties.FadeOut;

            SetupPerlin();

            if (fadeOut) m_timer = Timer.Create(m_duration, FadeOut, OnTimerComplete);
            else m_timer = Timer.Create(m_duration, null, OnTimerComplete);

            m_timer.Start();
        }

        public void FetchPerlin(CinemachineVirtualCamera vm)
        {
            m_perlin = vm.GetComponentPipeline().ToList().Where(component => component is CinemachineBasicMultiChannelPerlin).FirstOrDefault() as CinemachineBasicMultiChannelPerlin;
        }

        void SetupPerlin()
        {
            if (m_settings != null) m_perlin.m_NoiseProfile = m_settings;
            m_perlin.m_AmplitudeGain = m_amplitudeGain;
            m_perlin.m_FrequencyGain = m_frequencyGain;
        }

        void CleanPerlin()
        {
            m_perlin.m_AmplitudeGain = 0f;
            m_perlin.m_FrequencyGain = 0f;
        }

        private void FadeOut()
        {
            m_timeSpent += Time.deltaTime;
            m_amplitudeModifier = 1f - (m_timeSpent / m_duration);

            m_perlin.m_AmplitudeGain = m_amplitudeGain * m_amplitudeModifier;
        }

        private void OnTimerComplete(Timer.TimerState state)
        {
            m_timer = null;
            CleanPerlin();
        }

        NoiseSettings GetSettings(ShakeProperties.ShakeProfile profile)
        {
            switch (profile)
            {
                case ShakeProperties.ShakeProfile.Mild:
                    return m_mildProfile;
                case ShakeProperties.ShakeProfile.Strong:
                    return m_strongProfile;
                case ShakeProperties.ShakeProfile.Extreme:
                    return m_extremeProfile;
                default:
                    throw new Exception("cannot shake camera with given properties.");
            }
        }
    }
}

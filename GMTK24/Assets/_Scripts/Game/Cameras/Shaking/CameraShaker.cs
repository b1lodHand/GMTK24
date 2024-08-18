using com.absence.attributes;
using DG.Tweening;
using UnityEngine;

namespace com.game.cameras.shaking
{
    [RequireComponent(typeof(Camera))]
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField, Readonly] private Camera m_camera;
        [SerializeField] private ShakeProperties m_defaultShakeProperties = ShakeProperties.Default;

        Tweener m_shaker;
        Vector3 m_savedPosition;

        public void Shake() => Shake(m_defaultShakeProperties);
        public void Shake(ShakeProperties properties)
        {
            if (m_shaker != null) m_shaker.Kill(true);

            m_savedPosition = m_camera.transform.position;
            m_savedPosition.z = -10;

            m_shaker = m_camera.DOShakePosition(properties.Duration,
                properties.Amplitude,
                properties.Vibrato,
                properties.Randomness,
                properties.FadeOut,
                ShakeRandomnessMode.Full);

            m_shaker.OnComplete(() =>
            {
                m_shaker = null;
                m_camera.transform.position = m_savedPosition;
            });
        }

        private void Reset()
        {
            m_camera = GetComponent<Camera>();
        }
    }
}

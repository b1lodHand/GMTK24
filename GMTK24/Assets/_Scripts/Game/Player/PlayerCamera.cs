using com.absence.attributes;
using com.game.cameras.shaking;
using UnityEngine;

namespace com.game
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField, Readonly] private Camera m_camera;
        [SerializeField] private CameraShaker m_shaker;

        public void Shake(ShakeProperties properties) => m_shaker.Shake(properties);
        public void Shake() => m_shaker.Shake();

        private void Reset()
        {
            m_camera = GetComponent<Camera>();
        }
    }
}

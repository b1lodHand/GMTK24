using Cinemachine;
using com.game.cameras.shaking;
using UnityEngine;

namespace com.game.player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CameraShaker m_shaker;

        public void Shake(ShakeProperties properties) => m_shaker.Shake(properties);
        public void Shake() => m_shaker.Shake();

        public void SetupWith(CinemachineVirtualCamera vm)
        {
            vm.Follow = Player.Instance.Body;
            m_shaker.FetchPerlin(vm);
        }
    }
}

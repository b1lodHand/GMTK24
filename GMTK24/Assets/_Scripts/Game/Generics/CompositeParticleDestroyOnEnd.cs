using UnityEngine;

namespace com.game.generics
{
    [RequireComponent(typeof(ParticleSystem))]
    public class CompositeParticleDestroyOnEnd : MonoBehaviour
    {
        [SerializeField] private Transform m_target;

        private void OnParticleSystemStopped()
        {
            Destroy(m_target.gameObject);
        }
    }
}

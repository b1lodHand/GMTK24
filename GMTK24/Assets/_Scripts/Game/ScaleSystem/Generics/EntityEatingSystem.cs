using com.game.scaling.generics;
using UnityEngine;

namespace com.game.entities
{
    public class EntityEatingSystem : MonoBehaviour
    {
        [SerializeField] private GameObject m_eatingParticles;
        [SerializeField] private EntityScaler m_scaler;

        public virtual bool Eat(Edible target)
        {
            if (target.ScaleMode == Edible.ScalingMode.ScaleUp) m_scaler.ScaleUp(target.Mass);
            else if (target.ScaleMode == Edible.ScalingMode.ScaleDown) m_scaler.ScaleDown(target.Mass);

            if (m_eatingParticles != null) Instantiate(m_eatingParticles, target.transform.position, Quaternion.identity);
            return true;
        }
    }
}

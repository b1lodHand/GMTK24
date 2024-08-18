using com.game.scaling.generics;
using UnityEngine;

namespace com.game.entities
{
    public class EntityEatingSystem : MonoBehaviour
    {
        [SerializeField] private EntityScaler m_scaler;

        public virtual bool Eat(Edible target)
        {
            if (target.ScaleMode == Edible.ScalingMode.ScaleUp) m_scaler.ScaleUp(target.Mass);
            else if (target.ScaleMode == Edible.ScalingMode.ScaleDown) m_scaler.ScaleDown(target.Mass);

            return true;
        }
    }
}

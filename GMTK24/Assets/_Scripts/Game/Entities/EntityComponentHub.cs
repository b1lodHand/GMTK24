using com.game.scaling.generics;
using UnityEngine;

namespace com.game.entities
{
    public class EntityComponentHub : MonoBehaviour
    {
        [SerializeField] private EntityScaler m_scaler;
        public EntityScaler Scaler => m_scaler;
    }
}

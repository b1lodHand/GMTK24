using UnityEngine;

namespace com.game.entities
{
    [CreateAssetMenu(menuName = "Game/Entity System/Entity Form", fileName = "New Entity Form")]
    public class EntityForm : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController m_controller;
        public AnimatorOverrideController OverrideController => m_controller;
    }
}

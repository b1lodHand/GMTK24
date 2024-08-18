using com.game.interactables;
using UnityEngine;

namespace com.game.scaling.generics
{
    public class Edible : Interactable
    {
        public enum ScalingMode
        {
            ScaleUp = 0,
            ScaleDown = 1,
        }

        [SerializeField] private ScalingMode m_scaleMode = ScalingMode.ScaleUp;
        [SerializeField] [Min(0f)] private float m_mass = 1f;

        public ScalingMode ScaleMode => m_scaleMode;
        public float Mass => m_mass;

        public override string CustomInteractionMessageForPlayer => "Eat";

        protected override void Interact_Internal(InteractorData sender)
        {    
            bool success = sender.Entity.Hub.EatingSystem.Eat(this);
            if (success) Destroy(gameObject);
        }
    }
}

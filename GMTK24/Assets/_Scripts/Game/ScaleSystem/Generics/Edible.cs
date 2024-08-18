using com.game.interactables;
using UnityEngine;

namespace com.game.scaling.generics
{
    public class Edible : Interactable
    {
        public enum ScaleMode
        {
            ScaleUp = 0,
            ScaleDown = 1,
        }

        [SerializeField] private ScaleMode m_scaleMode = ScaleMode.ScaleUp;
        [SerializeField] private float m_mass = 1f;

        public override string CustomInteractionMessageForPlayer => "Eat";

        protected override void Interact_Internal(InteractorData sender)
        {
            if (m_scaleMode == ScaleMode.ScaleUp) sender.Entity.Hub.Scaler.ScaleUp(m_mass);
            else if (m_scaleMode == ScaleMode.ScaleDown) sender.Entity.Hub.Scaler.ScaleDown(m_mass);

            Destroy(gameObject);
        }
    }
}

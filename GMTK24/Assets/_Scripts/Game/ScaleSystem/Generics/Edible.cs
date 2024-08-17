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
            ScaleInteractorData data = sender as ScaleInteractorData;

            if (m_scaleMode == ScaleMode.ScaleUp) data.Scaler.ScaleUp(m_mass);
            else if (m_scaleMode == ScaleMode.ScaleDown) data.Scaler.ScaleDown(m_mass);
        }
    }

    public class ScaleInteractorData : InteractorData
    {
        public EntityScaler Scaler;
    }
}

using UnityEngine;

namespace com.game.abilities.subtypes.player
{
    [CreateAssetMenu(menuName = "Game/Abilities/Player/Claw Ability", fileName = "New Claw Ability")]
    public class ClawAbility : Ability
    {
        public enum ClawSide
        {
            Left = 0,
            Right = 1,
        }

        [SerializeField] private ClawSide m_clawSide = ClawSide.Left;

        public override bool CanUse(AbilityUserData user)
        {
            if (!user.IsPlayer()) return false;

            return true;
        }

        public override void Use(AbilityUserData user)
        {
            StartAbility();
            Debug.Log($"{m_clawSide} claw!");
            EndAbility();
        }
    }
}

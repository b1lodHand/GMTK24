using com.game.damage;
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
            DamageContext context = new()
            {
                Sender = user.Entity,
                Receiver = null,
                Value = 10f,
                Tier = DamageContext.DamageTier.Normal,
            };

            DamageSystem.Instance.DealDamage(context);
            EndAbility();
        }
    }
}

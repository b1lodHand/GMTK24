using UnityEngine;

namespace com.game.abilities.subtypes
{
    [CreateAssetMenu(menuName = "Game/Abilities/Player/Bite Ability", fileName = "New Bite Ability")]
    public class BiteAbility : Ability
    {
        public override bool CanUse(AbilityUserData user)
        {
            if(!user.IsPlayer()) return false;

            return true;
        }

        public override void Use(AbilityUserData user)
        {
            StartAbility();
            Debug.Log("Bite!");
            EndAbility();
        }
    }
}

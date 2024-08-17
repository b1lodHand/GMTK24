using com.game.abilities;
using System.Collections.Generic;
using UnityEngine;

namespace com.game
{
    [CreateAssetMenu(menuName = "Game/Abilities/Combo", fileName = "New Combo")]
    public class Combo : ScriptableObject
    {
        [SerializeField] private List<Ability> m_abilities = new();
        [SerializeField] private float m_maxTimeBetween = 1f;

        public Ability GetAbilityAt(int index) => m_abilities[index];
        public float TimeThreshold => m_maxTimeBetween;
        public int AbilityCount => m_abilities.Count;
    }
}

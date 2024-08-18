using com.absence.attributes;
using com.absence.timersystem;
using com.game.entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.abilities
{
    public class AbilityUser : MonoBehaviour
    {
        [Header("Initial Fields")]

        [SerializeField] private Entity m_entityScript;

        [Header("Database")]

        [SerializeField] private List<AbilityEntry> m_abilities = new();
        [SerializeField] private List<ComboEntry> m_combos = new();

        [Header("Runtime")]

        [SerializeField, Readonly] private Ability m_activeAbility;
        [SerializeField, Readonly] private Combo m_activeCombo;

        List<ComboEntry> m_possibleCombos;
        Timer m_comboTimer;
        int m_comboIndex;

        public Ability ActiveAbility => m_activeAbility;
        public Combo PredictedCombo => m_activeCombo; 

        private void Start()
        {
            m_abilities.ForEach(entry => entry.Initialize());
            m_combos.ForEach(entry => entry.Initialize());

            ResetCombos();
        }

        public bool UseAbility(Ability ability, bool ignoreCombos, out Ability usedAbility)
        {
            usedAbility = null;

            if (m_activeAbility != null) return false;
            if (ability == null) return false;

            AbilityEntry targetEntry = m_abilities.Where(entry => entry.Ability.Equals(ability)).FirstOrDefault();
            if (targetEntry == null) return false;
            if (!targetEntry.IsUnlocked) return false;

            AbilityUserData data = GenerateUserData();

            Ability targetAbility = targetEntry.GetUsableAbility();
            if (!targetAbility.CanUse(data)) return false;

            if(ignoreCombos)
            {
                ResetCombos();
                Cast();

                usedAbility = targetAbility;
                return true;
            }

            m_possibleCombos.RemoveAll(entry =>
            {
                Combo combo = entry.Combo;
                if (combo.AbilityCount <= m_comboIndex) return true;

                Ability ability = combo.GetAbilityAt(m_comboIndex);
                if (ability == null) return true;
                if (!ability.Equals(ability)) return true;

                return false;
            });

            if (m_comboTimer != null) ForceEndComboTimer();

            if(m_possibleCombos.Count > 0)
            {
                m_activeCombo = m_possibleCombos.FirstOrDefault().Combo;

                m_comboIndex++;
                if (m_comboIndex == m_activeCombo.AbilityCount) m_comboIndex = 0;

                targetAbility.OnEnd += () =>
                {
                    StartComboTimer();
                };
            }

            Cast();

            usedAbility = targetAbility;
            return true;

            void Cast()
            {
                targetAbility.OnEnd += () =>
                {
                    m_activeAbility = null;
                };

                m_activeAbility = targetAbility;
                targetAbility.Use(data);
            }
        }
        public bool UseCombo(Combo combo, out Ability usedAbility)
        {
            usedAbility = null;

            if (combo == null) return false;
            if (!m_combos.Any(entry => entry.Combo.Equals(combo))) return false;

            if (m_activeCombo != combo)
            {
                ResetCombos();
                m_activeCombo = combo;
            }

            return UseAbility(m_activeCombo.GetAbilityAt(m_comboIndex), false, out usedAbility);
        }
        public void ForceEndActiveAbility()
        {
            if (m_activeAbility == null) return;

            m_activeAbility.EndAbility();
        }

        AbilityUserData GenerateUserData()
        {
            AbilityUserData data = new();
            if (m_entityScript != null) data.Entity = m_entityScript;

            return data;
        }

        void StartComboTimer()
        {
            ForceEndComboTimer();
            m_comboTimer = Timer.Create(m_activeCombo.TimeThreshold, null, s =>
            {
                m_comboTimer = null;
                if (s == Timer.TimerState.Failed) return;

                ResetCombos();
            });

            m_comboTimer.Start();
        }

        void ForceEndComboTimer()
        {
            if (m_comboTimer == null) return;
            m_comboTimer.Fail();
        }

        void ResetCombos()
        {
            m_comboIndex = 0;
            m_possibleCombos = new(m_combos.Where(entry => entry.IsUnlocked));
        }

        [System.Serializable]
        public class AbilityEntry
        {
            [SerializeField] private bool m_unlockedByDefault = true;
            [SerializeField] private Ability m_ability = null;
            [SerializeField, Readonly] private Ability m_instantiatedAbility = null;

            bool m_isUnlocked = false;

            public bool IsUnlocked => m_isUnlocked;
            public Ability Ability => m_ability;

            public void Initialize()
            {
                m_isUnlocked = m_unlockedByDefault;
                if (m_ability != null) m_instantiatedAbility = m_ability.Clone();
            }

            public void Lock()
            {
                m_isUnlocked = false;
            }

            public void Unlock()
            {
                m_isUnlocked = true;
            }

            public Ability GetUsableAbility()
            {
                m_instantiatedAbility.ClearEvents();
                return m_instantiatedAbility;
            }
        }

        [System.Serializable]
        public class ComboEntry
        {
            [SerializeField] private bool m_unlockedByDefault = true;
            [SerializeField] private Combo m_combo = null;

            bool m_isUnlocked = false;

            public bool IsUnlocked => m_isUnlocked;
            public Combo Combo => m_combo;

            public void Initialize()
            {
                m_isUnlocked = m_unlockedByDefault;
            }
        }
    }
}

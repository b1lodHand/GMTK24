using com.absence.attributes;
using com.absence.timersystem;
using com.game.player;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Pipeline;
using UnityEngine;

namespace com.game.abilities
{
    public class AbilityUser : MonoBehaviour
    {
        [SerializeField] private bool m_usedByPlayer = false;
        [SerializeField] private List<AbilityEntry> m_abilities = new();
        [SerializeField] private List<ComboEntry> m_combos = new();
        [SerializeField] private Ability m_activeAbility;
        [SerializeField] private Combo m_activeCombo;

        List<ComboEntry> m_possibleCombos;
        AbilityUserData m_data;
        Timer m_comboTimer;
        int m_comboIndex;

        public Ability ActiveAbility => m_activeAbility;
        public Combo PredictedCombo => m_activeCombo; 

        private void Start()
        {
            m_abilities.ForEach(entry => entry.Initialize());
            m_data = new();

            ResetCombos();

            if (m_usedByPlayer) m_data.Person = Player.Instance.Person;
        }

        public bool UseAbility(Ability ability)
        {
            if (m_activeAbility != null) return false;

            AbilityEntry targetEntry = m_abilities.Where(entry => entry.Ability.Equals(ability)).FirstOrDefault();
            if (targetEntry == null) return false;

            Ability targetAbility = targetEntry.GetUsableAbility();
            if (!targetAbility.CanUse(m_data)) return false;

            m_possibleCombos.RemoveAll(entry =>
            {
                Combo combo = entry.Combo;
                if (combo.AbilityCount <= m_comboIndex) return true;

                Ability ability = combo.GetAbilityAt(m_comboIndex);
                if (ability == null) return true;
                if (!ability.Equals(ability)) return true;

                return false;
            });

            if (m_comboTimer != null) m_comboTimer.Fail();

            if(m_possibleCombos.Count > 0)
            {
                m_activeCombo = m_possibleCombos.FirstOrDefault().Combo;
                m_comboIndex++;

                targetAbility.OnEnd += () =>
                {
                    StartComboTimer();
                };
            }

            Cast();
            return true;

            void Cast()
            {
                targetAbility.OnEnd += () =>
                {
                    m_activeAbility = null;
                };

                m_activeAbility = targetAbility;
                targetAbility.Use(m_data);
            }
        }
        
        void StartComboTimer()
        {
            m_comboTimer = Timer.Create(m_activeCombo.TimeThreshold, null, s =>
            {
                m_comboTimer = null;
                if (s == Timer.TimerState.Failed) return;

                ResetCombos();
            });
        }

        void ForceEndComboTimer()
        {
            if (m_comboTimer == null) return;
            m_comboTimer.Fail();
        }

        void ResetCombos()
        {
            m_comboIndex = 0;
            m_possibleCombos = new(m_combos);
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

using System;
using UnityEngine;

namespace com.game.abilities
{
    //[CreateAssetMenu(menuName = "Game/Ability System/Ability", fileName = "New Ability")]
    public abstract class Ability : ScriptableObject
    {
        public virtual bool IsAsync => false;

        bool m_isEnded = false;
        public bool IsEnded => m_isEnded;

        public event Action OnStart;
        public event Action OnEnd;

        public bool IsClone { get; private set; } = false;
        public Ability ClonedFrom { get; private set; } = null;

        public abstract void Use(AbilityUserData user);
        public abstract bool CanUse(AbilityUserData user);

        public virtual void StartAbility()
        {
            if (!IsClone) throw new Exception("non-clone ability used runtime!");

            m_isEnded = false;
            OnStart?.Invoke();
        }
        public virtual void EndAbility()
        {
            if (!IsClone) throw new Exception("non-clone ability used runtime!");

            m_isEnded = true;
            OnEnd?.Invoke();
        }

        public void ClearEvents()
        {
            OnStart = null;
            OnEnd = null;
        }
        public Ability Clone()
        {
            Ability clone = Instantiate(this);
            clone.IsClone = true;
            clone.ClonedFrom = this;
            clone.ClearEvents();

            return clone;
        }
    }
}

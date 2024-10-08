using com.absence.timersystem;
using com.game.entities;
using com.game.scaling.generics;
using UnityEngine;

namespace com.game.player
{
    public class PlayerEatingSystem : EntityEatingSystem
    {
        [SerializeField] private float m_chewDuration = 1.2f;

        Timer m_timer;

        private void Start()
        {
            Player.Instance.Hub.Entity.Hub.FormSystem.OnFormChange += OnFormChange;
        }

        private void OnFormChange(EntityForm form1, EntityForm form2)
        {
            PlayerEventChannel.Eating.CommitFormChange(form1, form2);
        }

        public override bool Eat(Edible target)
        {
            bool success = base.Eat(target);

            if (!success) return false;

            if (m_timer != null) m_timer.Fail();

            Player.Instance.StartEating();
            m_timer = Timer.Create(m_chewDuration, null, s =>
            {
                m_timer = null;
                if (s == Timer.TimerState.Succeeded) Player.Instance.StopChewing();
            });

            m_timer.Start();
            return success;
        }
    }
}

using com.game.entities;
using System;
using UnityEngine;

namespace com.game.player
{
    public static class PlayerEventChannel
    {
        public static class Eating 
        {
            public static event Action OnStartEating = null;
            public static void CommitEatStart() => OnStartEating?.Invoke();

            public static event Action OnStopEating = null;
            public static void CommitEatStop() => OnStopEating?.Invoke();

            public static event Action OnStopChewing = null;
            public static void CommitChewStop() => OnStopChewing?.Invoke();

            public static event Action<EntityForm, EntityForm> OnFormChange = null;
            public static void CommitFormChange(EntityForm previousForm, EntityForm newForm) => OnFormChange?.Invoke(previousForm, newForm);

            public static void Reset()
            {
                OnStartEating = null;
                OnStopEating = null;
                OnStopChewing = null;
                OnFormChange = null;
            }
        }

        public static class Timeline
        {
            public static event Action OnCutsceneStart = null;
            public static void CommitCutseneStart() => OnCutsceneStart?.Invoke();

            public static event Action OnCutseneEnd = null;
            public static void CommitCutsceneEnd() => OnCutseneEnd?.Invoke();

            public static void Reset()
            {
                OnCutsceneStart = null;
                OnCutseneEnd = null;
            }
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Reset()
        {
            Eating.Reset();
            Timeline.Reset();
        }
    }
}

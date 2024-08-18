using System;
using UnityEngine;

namespace com.game.player
{
    public static class PlayerEventChannel
    {
        public static event Action OnStartChewing = null;
        public static void CommitChewStart() => OnStartChewing?.Invoke();

        public static event Action OnStopChewing = null;
        public static void CommitChewStop() => OnStopChewing?.Invoke();

        public static event Action OnEatAnimationEnd = null;
        public static void CommitEatAnimationEnd() => OnEatAnimationEnd?.Invoke();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Reset()
        {
            OnStartChewing = null;
            OnStopChewing = null;
            OnEatAnimationEnd = null;
        }
    }
}

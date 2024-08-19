using UnityEngine;

namespace com.game
{
    public static class Game
    {
        public static bool Initialized { get; set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Reset()
        {
            Initialized = false;
        }
    }
}

using UnityEngine;

namespace com.game.cameras.shaking
{
    [System.Serializable]
    public class ShakeProperties
    {
        [field: SerializeField] public float Duration { get; set; }
        [field: SerializeField] public float Amplitude { get; set; }
        [field: SerializeField] public int Vibrato { get; set; }
        [field: SerializeField] public float Randomness { get; set; }
        [field: SerializeField] public bool FadeOut { get; set; }

        public static ShakeProperties Default
        {
            get
            {
                ShakeProperties prop = new();
                prop.Duration = 0.2f;
                prop.Amplitude = 0.3f;
                prop.Vibrato = 100;
                prop.Randomness = 90f;
                prop.FadeOut = true;

                return prop;
            }
        }
    }
}

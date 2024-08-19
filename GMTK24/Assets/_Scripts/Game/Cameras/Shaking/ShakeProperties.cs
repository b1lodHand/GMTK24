using UnityEngine;

namespace com.game.cameras.shaking
{
    [System.Serializable]
    public class ShakeProperties
    {
        public enum ShakeProfile
        {
            Mild = 0,
            Strong = 1,
            Extreme = 2,
        }

        [field: SerializeField] public ShakeProfile Profile { get; set; }
        [field: SerializeField] public float Duration { get; set; }
        [field: SerializeField] public float Amplitude { get; set; }
        [field: SerializeField] public float Frequency { get; set; }
        [field: SerializeField] public bool FadeOut { get; set; }

        public static ShakeProperties Default
        {
            get
            {
                ShakeProperties prop = new();
                prop.Profile = ShakeProfile.Mild;
                prop.Duration = 0.2f;
                prop.Amplitude = 0.05f;
                prop.Frequency = 200f;
                prop.FadeOut = true;

                return prop;
            }
        }
    }
}

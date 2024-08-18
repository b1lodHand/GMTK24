using UnityEngine;
using com.absence.utilities;
using com.game.cameras.shaking;
using com.game.damage;
using com.game.popups;
using com.game.player;

namespace com.game.complementary
{
    public class DamageFeedbacks : StaticInstance<DamageFeedbacks>
    {
        [SerializeField] private ShakeProperties m_normalDamageShake = ShakeProperties.Default;
        [SerializeField] private ShakeProperties m_highDamageShake = ShakeProperties.Default;
        [SerializeField] private ShakeProperties m_higherDamageShake = ShakeProperties.Default;
        [SerializeField] private ShakeProperties m_superDamageShake = ShakeProperties.Default;
        [SerializeField] private ShakeProperties m_criticalDamageShake = ShakeProperties.Default;

        private void Start()
        {
            DamageSystem.Instance.OnDamageDeal += OnDamage;
        }

        private void OnDamage(DamageContext context)
        {
            HandleCameraShake(context);
            HandlePopup(context);
        }

        void HandlePopup(DamageContext context)
        {
            PopupManager.Instance.CreateDamagePopup(context);
        }
        void HandleCameraShake(DamageContext context)
        {
            ShakeProperties properties;
            switch (context.Tier)
            {
                case DamageContext.DamageTier.Normal:
                    properties = m_normalDamageShake;
                    break;
                case DamageContext.DamageTier.High:
                    properties = m_highDamageShake;
                    break;
                case DamageContext.DamageTier.Higher:
                    properties = m_higherDamageShake;
                    break;
                case DamageContext.DamageTier.Super:
                    properties = m_superDamageShake;
                    break;
                case DamageContext.DamageTier.Critical:
                    properties = m_criticalDamageShake;
                    break;
                default:
                    properties = m_normalDamageShake;
                    break;
            }

            Player.Instance.Camera.Shake(properties);
        }
    }
}

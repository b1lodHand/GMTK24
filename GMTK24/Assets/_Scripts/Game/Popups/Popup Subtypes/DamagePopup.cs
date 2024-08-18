using com.game.damage;
using UnityEngine;

namespace com.game.popups.subtypes
{
    public class DamagePopup : Popup
    {
        [Header("Damage Properties")]
        [SerializeField] [Min(0f)] private float m_damageSizeFactor = 0.05f;

        [Header("Damage Colors")]

        [SerializeField] private Color m_normalColor = Color.white;
        [SerializeField] private Color m_highColor = Color.white;
        [SerializeField] private Color m_higherColor = Color.white;
        [SerializeField] private Color m_superColor = Color.white;
        [SerializeField] private Color m_criticalColor = Color.white;

        public void SetDamage(DamageContext context)
        {
            Color textColor;
            switch (context.Tier)
            {
                case DamageContext.DamageTier.Normal:
                    textColor = m_normalColor;
                    break;
                case DamageContext.DamageTier.High:
                    textColor = m_highColor;
                    break;
                case DamageContext.DamageTier.Higher:
                    textColor = m_higherColor;
                    break;
                case DamageContext.DamageTier.Super:
                    textColor = m_superColor;
                    break;
                case DamageContext.DamageTier.Critical:
                    textColor = m_criticalColor;
                    break;
                default:
                    textColor = m_normalColor;
                    break;
            }

            m_text.color = textColor;

            Vector2 scale = m_text.transform.localScale;
            float ratio = scale.y / scale.x;
            float step = ((int)context.Tier) * m_damageSizeFactor;

            scale.x += step;
            scale.y += step * ratio;

            m_text.transform.localScale = scale;
        }
    }
}

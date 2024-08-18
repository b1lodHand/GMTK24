using com.absence.utilities;
using com.game.damage;
using com.game.popups.subtypes;
using UnityEngine;

namespace com.game.popups
{
    public class PopupManager : Singleton<PopupManager>
    {
        [Header("Initial Fields")]

        [SerializeField] private Canvas m_canvas;

        [Header("Popups")]

        [SerializeField] private DamagePopup m_damagePopupPrefab;

        public void CreateDamagePopup(DamageContext context)
        {
            DamagePopup popup = CreateInitialPopup(m_damagePopupPrefab, context.Value.ToString("F0"), context.ContactPoint);
            popup.SetDamage(context);
        }

        T CreateInitialPopup<T>(T targetPrefab, string text, Vector2 position) where T : Popup
        {
            T popup = Instantiate(targetPrefab, position, Quaternion.identity, m_canvas.transform);
            popup.Initialize(text);

            return popup;
        }
    }
}

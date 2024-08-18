using com.absence.utilities;
using System;

namespace com.game.damage
{
    public class DamageSystem : Singleton<DamageSystem>
    {
        public event Action<DamageContext> OnDamageDeal = null;

        public void DealDamage(DamageContext context)
        {
            DealDamage_Internal(context);
            OnDamageDeal?.Invoke(context);
        }

        void DealDamage_Internal(DamageContext context)
        {
            if(context.CausedDeath)
            {
                context.Receiver.Die();
                return;
            }

            if (context.Receiver == null) return;

            context.Receiver.Stats.Health.Value -= context.Value;
        }
    }
}

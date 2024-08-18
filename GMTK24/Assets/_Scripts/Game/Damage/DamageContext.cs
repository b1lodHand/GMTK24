using com.game.entities;
using UnityEngine;

namespace com.game.damage
{
    [System.Serializable]
    public class DamageContext
    {
        public enum DamageTier
        {
            Normal = 0,
            High = 1,
            Higher = 2,
            Super = 3,
            Critical = 4,
        }

        public DamageTier Tier { get; set; }
        public float Value { get; set; }

        public Entity Sender { get; set; }
        public Entity Receiver { get; set; }

        public bool CausedDeath { get; set; }
        public Vector2 ContactPoint { get; set; }

        public DamageContext() 
        {
            Tier = DamageTier.Normal;
            Value = 0f;
            Sender = null;
            Receiver = null;
            CausedDeath = false;
            ContactPoint = Vector2.zero;
        }
    }
}

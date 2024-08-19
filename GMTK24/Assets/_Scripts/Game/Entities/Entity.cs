using com.absence.personsystem;
using com.game.damage;
using System;
using UnityEngine;

namespace com.game.entities
{
    public class Entity : MonoBehaviour
    {
        public enum EntityType
        {
            Prop = 0,
            Creature = 1,
        }

        [SerializeField] protected EntityType m_type = EntityType.Creature;
        [SerializeField] protected Person m_person;
        [SerializeField] protected Transform m_body;
        [SerializeField] protected EntityComponentHub m_componentHub;

        public event Action OnDeath;
        public event Action OnTakeDamage;

        public EntityType Type => m_type;
        public Person Person => m_person;
        public Transform Body => m_body;
        public EntityComponentHub Hub => m_componentHub;
        public EntityStats Stats => Hub.Stats;

        public virtual bool TakeDamage(Entity sender, float damageTaken, Vector2 contactPoint, bool critical = false)
        {
            float health = Stats.Health.Value;
            bool death = damageTaken > health;

            float percentage = damageTaken / health;
            DamageContext.DamageTier tier;

            const float superBottomline = 0.85f;
            const float higherBottomline  = 0.7f;
            const float highBottomline = 0.45f;

            if (critical) tier = DamageContext.DamageTier.Critical;
            else if (percentage > superBottomline) tier = DamageContext.DamageTier.Super;
            else if (percentage > higherBottomline) tier = DamageContext.DamageTier.Higher;
            else if (percentage > highBottomline) tier = DamageContext.DamageTier.High;
            else tier = DamageContext.DamageTier.Normal;

            DamageContext context = new()
            {
                Sender = sender,
                Receiver = this,
                CausedDeath = death,
                ContactPoint = contactPoint,
                Tier = tier,
                Value = damageTaken,
            };

            DamageSystem.Instance.DealDamage(context);
            OnTakeDamage?.Invoke();

            return death;
        }

        public virtual void Die()
        {
            Destroy(gameObject);
            OnDeath?.Invoke();
        }
    }
}

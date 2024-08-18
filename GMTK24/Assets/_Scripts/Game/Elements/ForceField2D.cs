using com.game.generics.physics;
using System.Collections.Generic;
using UnityEngine;

namespace com.game.elements
{
    public class ForceField2D : MonoBehaviour
    {
        const float k_lineLength = 2f;
        const float k_knobRadius = 0.2f;

        [SerializeField] [Min(0.001f)] private float m_strength = 8000f;
        [SerializeField] [Min(0.001f)] private bool m_ignoreAnyResistance = false;

        List<(Rigidbody2D rb, ForceFieldResistance resistance)> m_affectedOnes = new();

        private void FixedUpdate()
        {
            m_affectedOnes.ForEach(entry =>
            {
                if(entry.resistance == null)
                {
                    entry.rb.AddForce(m_strength * -transform.up, ForceMode2D.Force);
                    return;
                }

                float resistance = entry.resistance.Resistance;
                if (resistance >= m_strength) return;

                float force = m_strength - resistance;
                entry.rb.AddForce(force * -transform.up, ForceMode2D.Force);
            });
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Rigidbody2D rb = collision.attachedRigidbody;
            if ((!m_ignoreAnyResistance) && rb.TryGetComponent(out ForceFieldResistance resistance)) m_affectedOnes.Add((rb, resistance));
            else m_affectedOnes.Add((rb, null));
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            m_affectedOnes.RemoveAll(entry => entry.rb.Equals(collision.attachedRigidbody));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector3 startPos = transform.position;

            Vector3 endPos = startPos;
            endPos.y -= k_lineLength;

            Gizmos.DrawLine(startPos, endPos);
            Gizmos.DrawSphere(endPos, k_knobRadius);
        }
    }
}

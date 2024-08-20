using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.game.ai
{
    public class ContextSteeringMove : MonoBehaviour
    {
        [SerializeField] private ContextSteering m_steerer;
        [SerializeField] private Rigidbody2D m_rb;
        [SerializeField] private float m_speed;

        private void FixedUpdate()
        {
            Vector2 moveDirection = m_steerer.DesiredDirection;
            m_rb.velocity  = moveDirection * m_speed ;
        }
    }
}

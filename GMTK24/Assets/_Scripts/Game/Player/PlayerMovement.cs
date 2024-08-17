using com.game.input;
using UnityEngine;

namespace com.game.player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D m_rigidbody;
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private float m_walkSpeed = 5f;

        Vector2 m_movement;

        private void Awake()
        {
            InputEventChannel.Player.OnMovementInput += OnMoveInput;
        }

        private void FixedUpdate()
        {
            m_rigidbody.AddForce(m_movement);
        }

        private void OnMoveInput(Vector2 vector)
        {
            if(m_debugMode) Debug.Log(vector);
            m_movement = vector.normalized * m_walkSpeed;
        }
    }
}

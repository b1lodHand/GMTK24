using UnityEngine;

namespace com.game.generics
{
    [DefaultExecutionOrder(-200)]
    public class ActivateOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(true);
        }
    }
}

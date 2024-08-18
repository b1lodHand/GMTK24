using UnityEngine;

namespace com.game.generics
{
    [DefaultExecutionOrder(-100)]
    public class DestroyOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            DestroyImmediate(gameObject);
        }
    }
}

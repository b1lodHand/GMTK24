using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.game.testing
{
    public class DebugScene : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_EDITOR
            if (Game.Initialized)
            {
                Kill();
                return;
            }

            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
#else
            Kill();
#endif
        }

        void Kill() 
        {
            DestroyImmediate(gameObject);
            return;
        }
    }
}

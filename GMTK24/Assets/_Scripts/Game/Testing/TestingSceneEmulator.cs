using UnityEngine;
using UnityEngine.AddressableAssets;

namespace com.game.testing
{
    public class TestingSceneEmulator : MonoBehaviour
    {
        [SerializeField] private AssetReference m_gameplayScene;

        private void Awake()
        {
            Addressables.LoadSceneAsync(m_gameplayScene, UnityEngine.SceneManagement.LoadSceneMode.Additive, true);
        }
    }
}

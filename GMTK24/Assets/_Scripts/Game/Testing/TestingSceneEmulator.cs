using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace com.game.testing
{
    public class TestingSceneEmulator : MonoBehaviour
    {
        [SerializeField] private List<AssetReference> m_scenesToLoad;

        int m_index;

        private void Awake()
        {
            if (Game.Initialized)
            {
                Kill();
                return;
            }

            m_index = -1;
            m_scenesToLoad.RemoveAll(scene => scene == null);
            if (m_scenesToLoad.Count == 0)
            {
                Kill();
                return;
            }

            LoadNext();
        }
        
        void LoadNext()
        {
            m_index++;

            if(m_index >= m_scenesToLoad.Count)
            {
                Kill();
                return;
            }

            AsyncOperationHandle handle =
                Addressables.LoadSceneAsync(m_scenesToLoad[m_index], UnityEngine.SceneManagement.LoadSceneMode.Additive, true);

            handle.Completed += operation =>
            {
                if (operation.Status == AsyncOperationStatus.Failed) throw new System.Exception("Something went wrong emulating the game.");

                LoadNext();
            };
        }

        void Kill()
        {
            DestroyImmediate(gameObject);
        }
    }
}

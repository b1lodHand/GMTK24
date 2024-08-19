using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace com.game.release.initialization
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private bool m_debugMode = false;

        [SerializeField] private AssetReference m_utilitiesScene;
        [SerializeField] private AssetReference m_startingSceneRelease;
        [SerializeField] private AssetReference m_startingSceneDebug;

        private void Awake()
        {
            SetupLoadingScene();
        }

        void SetupLoadingScene()
        {
            AsyncOperationHandle<SceneInstance> operation =
                Addressables.LoadSceneAsync(m_utilitiesScene, LoadSceneMode.Additive, true);

            operation.Completed += handle =>
            {
                if (operation.Status == AsyncOperationStatus.Failed) throw new Exception("Couldn't load the game.");

                LoadingPanel.Instance.Activate();
                SetupStartingScene();
            };
        }

        void SetupStartingScene()
        {
#if UNITY_EDITOR
            AsyncOperationHandle<SceneInstance> operation =
                Addressables.LoadSceneAsync(m_debugMode ? m_startingSceneDebug : m_startingSceneRelease, LoadSceneMode.Additive, false);
#else
            AsyncOperationHandle<SceneInstance> operation =
                Addressables.LoadSceneAsync(m_startingSceneRelease, LoadSceneMode.Additive, false);
#endif

            operation.Completed += handle =>
            {
                if(handle.Status == AsyncOperationStatus.Failed) throw new Exception("Couldn't load the game.");
                Complete(handle.Result);
            };
        }

        void Cleanup()
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(0, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            operation.completed += handle =>
            {
                Debug.Log("Game initialized successfully.");
            };
        }

        void Complete(SceneInstance startSceneInstance)
        {
            Game.Initialized = true;

            AsyncOperation operation = startSceneInstance.ActivateAsync();
            operation.completed += handle =>
            {
                SceneManager.SetActiveScene(startSceneInstance.Scene);
                LoadingPanel.Instance.Deactivate();

                Cleanup();
            };
        }
    }
}

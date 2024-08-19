using Cinemachine;
using com.absence.utilities;
using com.game.player;
using System.Collections;
using UnityEngine;

namespace com.game.rooms
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private CinemachineVirtualCamera m_levelVM;

        private void Start()
        {
            OnEnter();
        }

        public void OnEnter()
        {
            if (m_levelVM == null) return;
            if (Game.Initialized)
            {
                m_levelVM.Follow = Player.Instance.Body;
                return;
            }

            SetupLevelCamera();
        }

        public void OnExit()
        {

        }

        void SetupLevelCamera()
        {
            if (Player.Instance != null)
            {
                m_levelVM.Follow = Player.Instance.Body;
                return;
            }

            StartCoroutine(C_DelayedSetupLevelCamera());
        }

        private IEnumerator C_DelayedSetupLevelCamera()
        {
            yield return new WaitUntil(() => Player.Instance != null);

            Player.Instance.Camera.SetupWith(m_levelVM);
        }
    }
}

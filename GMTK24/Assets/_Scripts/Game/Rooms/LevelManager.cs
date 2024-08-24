using Cinemachine;
using com.absence.utilities;
using com.game.player;
using Polarith.AI.Move;
using System.Collections;
using UnityEngine;

namespace com.game.rooms
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private CinemachineVirtualCamera m_levelVM;
        [SerializeField] private AIMEnvironment m_playerEnvironment;

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

            StartCoroutine(C_DelayedSetup());
        }

        private IEnumerator C_DelayedSetup()
        {
            yield return new WaitUntil(() => Player.Instance != null);

            Player.Instance.Camera.SetupWith(m_levelVM);

            if (m_playerEnvironment != null)
            {
                m_playerEnvironment.GameObjects.Add(Player.Instance.gameObject);
            }
        }
    }
}

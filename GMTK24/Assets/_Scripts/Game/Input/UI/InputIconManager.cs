using com.absence.utilities;
using UnityEngine;

namespace com.game.input.ui
{
    public class InputIconManager : Singleton<InputIconManager>
    {
        [SerializeField] private InputIconPack m_iconPackInUse;
        public InputIconPack CurrentIconPack => m_iconPackInUse;

        private void Start()
        {
            m_iconPackInUse.ForceRefresh();
        }

        public bool TryGetValidIcon(string fullActionPath, out Sprite iconFound)
        {
            return m_iconPackInUse.GetInputIconsByFullActionPath(fullActionPath).TryGetValidIcon(fullActionPath, out iconFound);
        }
    }
}

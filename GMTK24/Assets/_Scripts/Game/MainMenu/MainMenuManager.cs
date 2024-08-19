using com.absence.utilities;
using UnityEngine;

namespace com.game.mainmenu
{
    public class MainMenuManager : Singleton<MainMenuManager>
    {
        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadEntryScene()
        {

        }
    }
}

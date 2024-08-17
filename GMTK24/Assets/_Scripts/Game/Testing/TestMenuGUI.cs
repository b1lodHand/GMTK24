using com.game.menus;
using UnityEngine;

namespace com.game.testing
{
    public class TestMenuGUI : MonoBehaviour
    {
        private void OnGUI()
        {
            GUILayout.BeginHorizontal();

            MenuManager.Instance.AllMenus.ForEach(menu =>
            {
                if (!GUILayout.Button(menu.name)) return;

                MenuManager.Instance.Open(menu.name, true);
            });

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            MenuManager.Instance.AllMenus.ForEach(menu =>
            {
                if (!GUILayout.Button(menu.name)) return;

                MenuManager.Instance.Close(menu.name);
            });

            GUILayout.EndHorizontal();
        }
    }
}

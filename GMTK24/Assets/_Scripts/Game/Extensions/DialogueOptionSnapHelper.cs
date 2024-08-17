using com.absence.dialoguesystem;
using com.game.generics.ui;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.extensions
{
    public class DialogueOptionSnapHelper : MonoBehaviour
    {
        [SerializeField] private ScrollRectElementSnapper m_snapper;

        private void Start()
        {
            DialogueDisplayer.Instance.OnDisplay += OnDisplay;
        }

        private void OnDisplay()
        {
            StartCoroutine(C_Perform());
        }

        IEnumerator C_Perform()
        {
            yield return null;

            m_snapper.RefreshChildren();

            List<OptionText> options = m_snapper.Children.ConvertAll(rect => rect.GetComponent<OptionText>()).ToList();

            options.ForEach(option =>
            {
                option.OnSelectAction += () =>
                {
                    m_snapper.SetSelectedChild(option.GetComponent<RectTransform>());
                    m_snapper.UpdateScrollPosition();
                };
            });
        }
    }
}

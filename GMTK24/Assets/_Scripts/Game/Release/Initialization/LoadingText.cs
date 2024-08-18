using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace com.game.release.initialization
{
    public class LoadingText : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_text;

        [SerializeField] private List<string> m_sequence = new();
        [SerializeField] private float m_eachDuration = 0.75f;

        int m_currentIndex = -1;
        float m_counter;

        private void Start()
        {
            m_currentIndex = -1;
            MoveToNext();
        }

        private void Update()
        {
            if (!enabled) return;

            if (m_counter < m_eachDuration) m_counter += Time.deltaTime;
            else MoveToNext();
        }

        void MoveToNext()
        {
            m_currentIndex++;
            ClampIndex();

            m_text.text = m_sequence[m_currentIndex];
            ResetCounter();
        }

        void ClampIndex()
        {
            if (m_currentIndex >= m_sequence.Count) m_currentIndex = 0;
        }
        
        void ResetCounter()
        {
            m_counter = 0f;
        }
    }
}

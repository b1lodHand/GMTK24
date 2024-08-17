using com.absence.attributes;
using com.game.interactables;
using UnityEngine;

namespace com.game.generics
{
    public class InteractableOutliner : MonoBehaviour
    {
        public enum OutlineType
        {
            GameObject = 0,
            Material = 1,
            Pass = 2,
        }

        [SerializeField, Readonly] private Interactable m_interactable;
        [SerializeField] private OutlineType m_outlineType;

        [SerializeField, ShowIf(nameof(m_outlineType), OutlineType.GameObject)] 
        private GameObject m_outlineObject;

        [SerializeField, ShowIf(nameof(m_outlineObject), OutlineType.Material)]
        private SpriteRenderer m_targetRenderer;

        [SerializeField, ShowIf(nameof(m_outlineObject), OutlineType.Material)]
        private Material m_material;

        private void Awake()
        {
            if (m_interactable == null)
            {
                Debug.LogWarning("There are no interactables associated to this outliner. Disabling self...");
                enabled = false;
                return;
            }

            m_interactable.OnSelectedByPlayer += EnableOutline;
            m_interactable.OnUnselectedByPlayer += DisableOutline;
        }

        void EnableOutline() => SetOutlineVisibility(true);
        void DisableOutline() => SetOutlineVisibility(false);
        void SetOutlineVisibility(bool visibility)
        {
            if (!enabled) return;

            switch (m_outlineType)
            {
                case OutlineType.GameObject:
                    m_outlineObject.SetActive(visibility);
                    break;

                case OutlineType.Material:
                    m_targetRenderer.sharedMaterial = m_material;
                    break;

                default:
                    Debug.Log("There is no logic for this outliner type yet.");
                    break;
            }
        }

        private void Reset()
        {
            FindInteractable();
        }

        [ContextMenu("Find Interactable Script Associated")]
        void FindInteractable()
        {
            m_interactable = GetComponent<Interactable>();
        }
    }
}

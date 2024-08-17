using UnityEngine;
using UnityEngine.UI;

namespace com.game.extensions.statics
{
    public static class ScrollRectExtensions
    {
        public static Vector2 GetSnapPosition(this ScrollRect instance, RectTransform element)
        {
            Canvas.ForceUpdateCanvases();

            var contentPos = (Vector2)instance.transform.InverseTransformPoint(instance.content.position);
            var childPos = (Vector2)instance.transform.InverseTransformPoint(element.position);
            var endPos = contentPos - childPos;

            if (!instance.horizontal) endPos.x = instance.content.anchoredPosition.x;

            if (!instance.vertical) endPos.y = instance.content.anchoredPosition.y;

            return endPos;
        }
    }
}

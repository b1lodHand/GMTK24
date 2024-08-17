using UnityEngine;

namespace com.game.extensions.statics
{
    public static class TransformExtensions
    {
        public static void ScaleAround(this Transform target, Vector3 pivot, Vector3 newScale)
        {
            Vector3 targetPosition = target.localPosition;
            Vector3 pivotPosition = pivot;

            Vector3 positionDifference = targetPosition - pivotPosition;

            float scaleRatio = newScale.x / target.localScale.x;
            
            Vector3 newPosition = pivotPosition + positionDifference * scaleRatio;

            target.localScale = newScale;
            target.localPosition = newPosition;
        }
    }
}

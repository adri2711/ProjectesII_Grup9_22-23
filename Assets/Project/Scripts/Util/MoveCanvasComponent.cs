using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCanvasComponent : MonoBehaviour
{
    public static void Drag(Transform target, Vector2 pointerPosition, Canvas canvas, string mainCameraName = "Main Camera")
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerPosition,
            GameObject.Find(mainCameraName).GetComponent<Camera>(),
            out position
            );
        target.position = canvas.transform.TransformPoint(position);
    }
}

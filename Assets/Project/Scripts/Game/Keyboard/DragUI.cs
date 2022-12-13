using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragUI : MonoBehaviour
{
    private Canvas canvas;
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void DragHandler(BaseEventData data)
    {
        Vector2 position;
        PointerEventData pointerData = (PointerEventData)data;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            GameObject.Find("Main Camera").GetComponent<Camera>(),
            out position
            );
        transform.position = canvas.transform.TransformPoint(position);
    }
}

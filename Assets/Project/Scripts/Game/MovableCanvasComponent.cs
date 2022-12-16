using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MovableCanvasComponent : MonoBehaviour
{
    public bool draggable = false;
    public bool bouncing = true;
    [NonSerialized] public bool held = false;
    public float baseSpeed = 0f;
    public float bounce = 1f;
    protected float addSpeed;
    protected Vector2 dir;

    protected void SetPosition(Transform targetTransform, Vector3 newPos)
    {
        targetTransform.localPosition += newPos;
    }
    protected virtual void MovementUpdate(Transform targetTransform, Canvas targetCanvas)
    {
        RectTransform targetRect = (RectTransform)targetTransform;
        RectTransform canvasRect = targetCanvas.GetComponent<RectTransform>();
        Vector3 movement = dir * (baseSpeed + addSpeed);

        addSpeed -= 0.1f;
        if (addSpeed < 0)
        {
            addSpeed = 0;
        }

        if (bouncing)
        {
            Vector2 edges = new Vector2(canvasRect.rect.width / 2, canvasRect.rect.height / 2);
            Vector2 popupPos = targetTransform.localPosition;
            Vector2 popupSize = new Vector2(targetRect.rect.width, targetRect.rect.height);
            if (Mathf.Abs(popupPos.x) + (popupSize.x / 2) >= edges.x && Mathf.Ceil(dir.normalized.x) == Mathf.Ceil(popupPos.normalized.x))
            {
                dir.x = -dir.x;
                addSpeed += bounce;
                GameEvents.instance.PopupBounce();
            }
            else if (Mathf.Abs(popupPos.y) + (popupSize.y / 2) >= edges.y && Mathf.Ceil(dir.normalized.y) == Mathf.Ceil(popupPos.normalized.y))
            {
                dir.y = -dir.y;
                addSpeed += bounce;
                GameEvents.instance.PopupBounce();
            }
        }

        targetTransform.localPosition += movement;
    }
    protected virtual void SetRandomDirection()
    {
        dir = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
    public virtual void DragMove(BaseEventData data, Transform targetTransform, Canvas targetCanvas, string mainCameraName = "Main Camera")
    {
        if (held && draggable)
        {
            PointerEventData pointerData = (PointerEventData)data;
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)targetCanvas.transform,
                pointerData.position,
                GameObject.Find(mainCameraName).GetComponent<Camera>(),
                out position
                );
            targetTransform.position = targetCanvas.transform.TransformPoint(position);
        }
    }
    public virtual void DragStart(BaseEventData data)
    {
        GameEvents.instance.PopupGrab();
        held = true;
    }
    public virtual void DragEnd(BaseEventData data)
    {
        held = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MovableCanvasComponent : MonoBehaviour
{
    public bool draggable = false;
    public bool bouncing = true;
    [NonSerialized] public bool held = false;
    public float baseSpeed = 0f;
    public float bounce = 1f;
    protected float addSpeed;
    protected Vector2 dir;
    private bool speedTimerLock;

    public void SetPosition(Transform targetTransform, Vector3 newPos)
    {
        targetTransform.localPosition += newPos;
    }
    public abstract Vector2 GetPosition();

    protected void MovementUpdate(Transform targetTransform, Canvas targetCanvas)
    {
        RectTransform canvasRect = targetCanvas.GetComponent<RectTransform>();
        Vector2 topRight = new Vector2(canvasRect.rect.width / 2, canvasRect.rect.height / 2);
        Vector2 bottomLeft = new Vector2(-canvasRect.rect.width / 2, -canvasRect.rect.height / 2);
        MovementUpdate(targetTransform, targetCanvas, bottomLeft, topRight);
    }
    protected virtual void MovementUpdate(Transform targetTransform, Canvas targetCanvas, Vector2 bottomLeftLimit, Vector2 topRightLimit)
    {
        RectTransform targetRect = (RectTransform)targetTransform;
        Vector3 movement = dir * (baseSpeed + addSpeed);

        addSpeed -= 0.1f;
        if (addSpeed < 0)
        {
            addSpeed = 0;
        }

        if (bouncing)
        {
            Vector2 position = targetTransform.localPosition;
            Vector2 compSize = new Vector2(targetRect.rect.width, targetRect.rect.height);
            if ((position.x + compSize.x / 2 > topRightLimit.x || position.x - compSize.x / 2 < bottomLeftLimit.x) && Mathf.Ceil(dir.normalized.x) == Mathf.Ceil(position.normalized.x))
            {
                dir.x = -dir.x;
                addSpeed += bounce;
                GameEvents.instance.PopupBounce();
            }
            else if ((position.y + compSize.y / 2 > topRightLimit.y || position.y - compSize.y / 2 < bottomLeftLimit.y) && Mathf.Ceil(dir.normalized.y) == Mathf.Ceil(position.normalized.y))
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
    protected void SetSpeedForDuration(float speed, float dur)
    {
        if (speedTimerLock) return;
        StartCoroutine(SetSpeedTimer(speed, dur));
    }
    private IEnumerator SetSpeedTimer(float speed, float dur)
    {
        speedTimerLock = true;
        float prevSpeed = baseSpeed;
        baseSpeed = speed;
        yield return new WaitForSeconds(dur);
        baseSpeed = prevSpeed;
        speedTimerLock = false;
    }
    public virtual void DragMove(BaseEventData data, Transform targetTransform, Canvas targetCanvas, string mainCameraName = "Main Camera")
    {
        RectTransform canvasRect = targetCanvas.GetComponent<RectTransform>();
        Vector2 topRight = new Vector2(canvasRect.rect.width / 2, canvasRect.rect.height / 2);
        Vector2 bottomLeft = new Vector2(-canvasRect.rect.width / 2, -canvasRect.rect.height / 2);
        DragMove(data, targetTransform, targetCanvas, bottomLeft, topRight, mainCameraName);
    }
    public virtual void DragMove(BaseEventData data, Transform targetTransform, Canvas targetCanvas, Vector2 bottomLeftLimit, Vector2 topRightLimit, string mainCameraName = "Main Camera")
    {
        if (!held || !draggable) return;

        RectTransform targetRect = (RectTransform)targetTransform;
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)targetCanvas.transform,
            pointerData.position,
            GameObject.Find(mainCameraName).GetComponent<Camera>(),
            out position
            );
        
        Vector2 compSize = new Vector2(targetRect.rect.width, targetRect.rect.height);
        if (position.x > topRightLimit.x)
        {
            Vector2 edgePos = new Vector2(topRightLimit.x, position.y);
            position = edgePos;
        }
        else if (position.x < bottomLeftLimit.x)
        {
            Vector2 edgePos = new Vector2(bottomLeftLimit.x, position.y);
            position = edgePos;
        }
        if (position.y > topRightLimit.y)
        {
            Vector2 edgePos = new Vector2(position.x, topRightLimit.y);
            position = edgePos;
        }
        else if (position.y < bottomLeftLimit.y)
        {
            Vector2 edgePos = new Vector2(position.x, bottomLeftLimit.y);
            position = edgePos;
        }

        targetTransform.position = targetCanvas.transform.TransformPoint(position);
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

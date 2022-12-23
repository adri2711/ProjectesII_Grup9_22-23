using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public abstract class Interruption : MovableCanvasComponent
{
    protected Transform targetTransform;
    protected Canvas targetCanvas;
    [NonSerialized] public Animator animator;
    protected string id = "invalid";
    public float closeTime = 0f;
    public virtual void Spawn()
    {
        targetTransform = GetComponentInChildren<Image>().transform;
        targetCanvas = GetComponentInChildren<Canvas>();
        targetCanvas.sortingOrder = IntManager.instance.GetIntCount();
        animator = GetComponentInChildren<Animator>();
        animator.Play("open");
    }
    public virtual void Close() {
        StartCoroutine(CloseThread(closeTime));
    }
    protected IEnumerator CloseThread(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
    private void FixedUpdate()
    {
        if (baseSpeed > 0f && !held)
        {
            MovementUpdate(targetTransform, targetCanvas);
        }
    }
    public void SetPosition(Vector3 newPos)
    {
        SetPosition(GetComponentInChildren<Image>().transform, newPos);
    }
    public void Drag(BaseEventData data)
    {
        DragMove(data, targetTransform, targetCanvas);
    }
    public override void DragStart(BaseEventData data)
    {
        base.DragStart(data);
    }
    public override void DragEnd(BaseEventData data)
    {
        base.DragEnd(data);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyPlacement : MovableCanvasComponent
{
    [NonSerialized] public int index;
    [NonSerialized] public int rootIndex;
    [NonSerialized] public Vector2 rootPos;
    public bool detachable = true;
    [NonSerialized] public bool inRoot = true;
    [NonSerialized] public int detachCounter = 0;
    private int clicksToDetach = 3;

    private KeyDisplay key;
    private Canvas canvas;
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        key = GetComponent<KeyDisplay>();
        inRoot = true;
        rootPos = transform.parent.localPosition;
        key.UpdateKey();
    }
    public void AttachToRoot(int newRoot)
    {
        rootIndex = newRoot;
        transform.parent.localPosition = rootPos;
        inRoot = true;
    }
    public void DetachFromRoot()
    {
        inRoot = false;
        KeyboardRoots.keyRoots[rootIndex].DetachKey();
    }
    public void Drag(BaseEventData data)
    {
        if (!inRoot)
        {
            DragMove(data, transform.parent, canvas);
        }
    }
    public override void DragStart(BaseEventData data)
    {
        base.DragStart(data);
        key.UpdateKey();
    }
    public override void DragEnd(BaseEventData data)
    {
        base.DragEnd(data);
        key.UpdateKey();
    }
    public void RightClick(BaseEventData data)
    {
        if (inRoot && detachable)
        {
            detachCounter++;
            if (detachCounter > clicksToDetach)
            {
                DetachFromRoot();
                detachCounter = 0;
                key.UpdateKey();
            }
        }
    }
}

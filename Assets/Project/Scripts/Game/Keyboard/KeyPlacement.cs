using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyPlacement : MovableCanvasComponent
{
    [NonSerialized] public int index;
    [NonSerialized] public int rootIndex;
    [NonSerialized] public Vector2 rootPos;
    public bool detachable = true;
    [NonSerialized] public bool inRoot = true;
    [NonSerialized] public int detachCounter = 0;
    private int clicksToDetach = 3;

    [SerializeField] private AnimationCurve launchSpeedCurve;
    [SerializeField] private float minLaunchDuration;
    [SerializeField] private float maxLaunchDuration;
    [NonSerialized] public float movementTime = 0f;

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
                Launch();
            }
        }
    }
    public void Launch()
    {
        if (detachable)
        {
            SetRandomDirection();
            movementTime = UnityEngine.Random.Range(minLaunchDuration, maxLaunchDuration);
        }
    }
    private void FixedUpdate()
    {
        if (movementTime > 0)
        {
            baseSpeed = launchSpeedCurve.Evaluate(movementTime / maxLaunchDuration);
            MovementUpdate(transform.parent, canvas);
            movementTime -= Time.deltaTime;
        }
    }
}

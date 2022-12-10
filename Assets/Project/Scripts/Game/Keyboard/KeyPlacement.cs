using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyPlacement : MonoBehaviour
{
    public int index;
    public int rootIndex;
    public Vector2 rootPos;
    public bool detachable = true;
    public bool inRoot = true;
    public bool held = false;
    public int detachCounter = 0;
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
        held = true;
        inRoot = false;
        KeyboardRoots.keyRoots[rootIndex].DetachKey();
    }
    public void Drag(BaseEventData data)
    {
        if (!inRoot && held)
        {
            Vector2 position;
            PointerEventData pointerData = (PointerEventData)data;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pointerData.position,
                GameObject.Find("Main Camera").GetComponent<Camera>(),
                out position
                );
            transform.parent.position = canvas.transform.TransformPoint(position);
        }
    }
    public void DragStart(BaseEventData data)
    {
        held = true;
        key.UpdateKey();
    }
    public void DragEnd(BaseEventData data)
    {
        held = false;
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
